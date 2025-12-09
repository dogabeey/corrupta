using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Linq;
using TMPro;

public class MapDrawer : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Transform cityTextParent;
    public TMP_Text cityTextPrefab;
    [ReadOnly] private Color selectedColor;

    private Texture2D _texture;
    private float _mapHeight, _mapWidth;


    public Color SelectedColor { 
        get => selectedColor; 
        set => selectedColor = value;
    }

    private void Start()
    {
        _texture = (Texture2D) meshRenderer.material.mainTexture;
        _mapHeight = _texture.height;
        _mapWidth = _texture.width;

        var uniqueColors = FindAllUniqueColorsInTexture();
        foreach ( var color in uniqueColors)
        {
            var pixels = GetProvincePixels(color);
            City city = GetSelectedCityFromColor(color);
            if(city)
            {
                city.surfaceSizeByPixel = pixels.Count;

                Vector2 point1 = pixels.GetPointWithSmallestY();
                Vector2 point2 = pixels.GetPointWithLargestY();
                Vector3 worldPoint1 = GetWorldPointFromMeshTexture(meshRenderer, point1);
                Vector3 worldPoint2 = GetWorldPointFromMeshTexture(meshRenderer, point2);

                Vector3 angle1 = pixels.GetPointWithSmallestX();
                Vector3 angle2 = pixels.GetPointWithLargestX();
                Vector3 worldAngle1 = GetWorldPointFromMeshTexture(meshRenderer, angle1);
                Vector3 worldAngle2 = GetWorldPointFromMeshTexture(meshRenderer, angle2);

                CreateCityNameText(city, worldPoint1, worldPoint2, worldAngle1, worldAngle2);
            }
        }
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,256))
        {
            Debug.Log("Hit point: " + hit.textureCoord.x * _mapWidth + " " + hit.textureCoord.y * _mapHeight);
            SelectColor(_texture.GetPixel((int)(hit.textureCoord.x * _mapWidth), (int)(hit.textureCoord.y * _mapHeight)));
        }

    }

    private void SelectColor(Color color)
    {
        Color srgb = color.linear == color ? color.gamma : color;  // Convert back to gamma
        meshRenderer.material.SetInt(GameConstants.Instance.mapShaderProvinceSelectString, srgb == Color.black ? 0 : 1);
        meshRenderer.material.SetColor(GameConstants.Instance.mapShaderProvinceColorString, srgb);

        SelectedColor = color;
        EventManager.TriggerEvent(GameConstants.GameEvents.SELECTED_CITY, new EventParam());
    }

    public City GetSelectedCityFromColor(Color selectedColor)
    {
        var cityDefs = CityDefiniton.GetInstances();
        // Find the city definition that is closest to the selected color
        CityDefiniton cd = cityDefs.OrderBy(c =>
        {
            float distance = c.Color.Distance(selectedColor);

            return distance;
        }
        ).Where(c => c.Color.Distance(selectedColor) < GameConstants.Instance.mapMaxColorDistanceDetection).FirstOrDefault();

        List<City> instances = City.GetInstances();
        City city = null;
        if(cd)
        {
            if (instances.Exists(c => c == cd.city))
            {
                city = instances.Find(c => c == cd.city);
            }
        }
        return city;
    }
    private HashSet<Color> FindAllUniqueColorsInTexture()
    {
        HashSet<Color> uniqueColors = new HashSet<Color>();
        for (int x = 0; x < _mapWidth; x++)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                Color pixelColor = _texture.GetPixel(x, y);
                uniqueColors.Add(pixelColor);
            }
        }
        return uniqueColors;
    }
    private Vector3 GetWorldPointFromMeshTexture(MeshRenderer meshRenderer, Vector2 pixelPoint)
    {
        // Normalize pixel coords to UV
        float u = (pixelPoint.x + 0.5f) / _texture.width;
        float v = (pixelPoint.y + 0.5f) / _texture.height;

        // Invert UVs because plane is mirrored
        u = 1f - u;
        v = 1f - v;

        // Convert UV → local plane coords (-5..5)
        float localX = Mathf.Lerp(-5f, 5f, u);
        float localZ = Mathf.Lerp(-5f, 5f, v);

        Vector3 localPos = new Vector3(localX, 0f, localZ);

        return meshRenderer.transform.TransformPoint(localPos);
    }

    private List<Vector2> GetProvincePixels(Color provinceColor)
    {
        List<Vector2> pixels = new List<Vector2>();
        for (int x = 0; x < _mapWidth; x++)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                Color pixelColor = _texture.GetPixel(x, y);
                if (pixelColor == provinceColor)
                {
                    pixels.Add(new Vector2(x, y));
                }
            }
        }
        return pixels;
    }

    // Create a tmp text for cit name and place it between pos1 and pos2
    private void CreateCityNameText(City city, Vector3 pos1, Vector3 pos2, Vector3 angle1, Vector3 angle2)
    {
        TMP_Text cityText = Instantiate(cityTextPrefab, cityTextParent);
        cityText.text = city.cityName;
        Vector3 midPoint = (pos1 + pos2 + angle1 + angle2) / 4f;
        midPoint.y = transform.position.y;
        cityText.transform.position = midPoint + Vector3.up * 0.1f; // Slightly above the map
        // Set x bounds of the text box to fit between angle1 and angle2
        cityText.rectTransform.sizeDelta = new Vector2(Vector3.Distance(angle1, angle2) * 10f, cityText.rectTransform.sizeDelta.y);
        // Set its rotation to angle between pos1 and pos2
        Vector3 direction = (angle2 - angle1).normalized;
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        cityText.transform.rotation = Quaternion.Euler(90f, -angle, 0f);
        cityText.transform.localScale *= 0.85f;
        cityText.transform.localScale *= GameConstants.Instance.mapTextScaleFactor;
    }
}