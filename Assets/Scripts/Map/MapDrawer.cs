using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Linq;

public class MapDrawer : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public string provinceSelectString = "_ProvinceSelect";
    public string provinceColorString = "_ProvinceColor";
    [ReadOnly] private Color selectedColor;

    private List<City> _drawnCities = new List<City>();
    private Texture2D _texture;
    private float _mapHeight, _mapWidth;
    private Vector3 _worldPoint1 = Vector3.zero, _worldPoint2 = Vector3.zero;


    public Color SelectedColor { 
        get => selectedColor; 
        set => selectedColor = value;
    }

    private void Start()
    {
        _texture = (Texture2D) meshRenderer.material.mainTexture;
        _mapHeight = _texture.height;
        _mapWidth = _texture.width;
        Vector2 point1 = Vector2.zero;
        Vector2 point2 = Vector2.zero;

        var uniqueColors = FindAllUniqueColorsInTexture();
        foreach ( var color in uniqueColors)
        {
            var pixels = GetProvincePixels(color);
            City city = GetSelectedCityFromColor(color);
            if(city)
            {
                city.surfaceSizeByPixel = pixels.Count;
                city.color = color;

                _drawnCities.Add(city);
                ( point1, point2 ) = pixels.GetFarthestPoints();
                _worldPoint1 = GetWorldPointFromMeshTexture(meshRenderer, point1);
                _worldPoint2 = GetWorldPointFromMeshTexture(meshRenderer, point2);

                city.farthestPoint1 = _worldPoint1;
                city.farthestPoint2 = _worldPoint2;
                city.farthestPlanePoint1 = point1;
                city.farthestPlanePoint2 = point2;
            }
        }
    }

    private void Update()
    {
    }

    private void OnDrawGizmos()
    {
        foreach (var city in _drawnCities)
        {
            Gizmos.color = city.color;
            DrawThickLine(city.farthestPoint1, city.farthestPoint2, 0.1f);
        }
    }
    private void DrawThickLine(Vector3 a, Vector3 b, float thickness)
    {
        // thickness world units cinsinden
        Vector3 dir = (b - a).normalized;
        Vector3 side = Vector3.Cross(dir, Camera.current.transform.forward).normalized * (thickness / 2f);

        // 3 paralel çizgi → kalın görünüm
        Gizmos.DrawLine(a - side, b - side);
        Gizmos.DrawLine(a, b);
        Gizmos.DrawLine(a + side, b + side);
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
        meshRenderer.material.SetInt(provinceSelectString, srgb == Color.black ? 0 : 1);
        meshRenderer.material.SetColor(provinceColorString, srgb);

        SelectedColor = color;
    }

    public City GetSelectedCityFromColor(Color selectedColor)
    {
        float searchedRed = Mathf.FloorToInt(selectedColor.r * 255);
        float searchedGreen = Mathf.FloorToInt(selectedColor.g * 255);
        float searchedBlue = Mathf.FloorToInt(selectedColor.b * 255);

        var cityDefs = CityDefiniton.GetInstances();
        // Find the city definition that is closest to the selected color
        CityDefiniton cd = cityDefs.OrderBy(c =>
        {
            float distance = c.Color.Distance(selectedColor);
            return distance;
        }
        ).First();

        List<City> instances = City.GetInstances();
        City city = null;
        if (instances.Exists(c => c == cd.city))
        {
            city = instances.Find(c => c == cd.city);
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

}