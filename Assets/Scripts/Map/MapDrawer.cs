using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class MapDrawer : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Transform cityTextParent;
    public TMP_Text cityTextPrefab;
    public float textHeightOffset = 1f;
    [Header("Debug")]
    public bool editMode = false;
    [ShowIf(nameof(IsEditMode))] public TMP_InputField cityNameInput;
    [ShowIf(nameof(IsEditMode))] public TMP_InputField cityDescriptionInput;
    [ShowIf(nameof(IsEditMode))] public TMP_InputField cityPopulationInput;

    public bool doNotGenerateCityLabels = false;

    private Texture2D _texture;
    private float _mapHeight, _mapWidth;
    private Color _selectedColor;


    #region Odin Inspector Hooks
    private bool IsEditMode() => editMode;
    #endregion

    public Color SelectedColor { 
        get => _selectedColor; 
        set
        {
            _selectedColor = value;
            if(_selectedColor.r == 0 && _selectedColor.g == 0 && _selectedColor.b == 0)
            {
                meshRenderer.material.SetInt("_ProvinceSelected", 0); // Deselect province.
            }
            else
            {
                meshRenderer.material.SetInt("_ProvinceSelected", 1); // Select province.
                meshRenderer.material.SetColor("_ProvinceColor", _selectedColor);
            }
        }
    }

    private void Start()
    {
        _texture = (Texture2D)meshRenderer.material.mainTexture;
        _mapHeight = _texture.height;
        _mapWidth = _texture.width;

        if(!doNotGenerateCityLabels) GenerateCityLabels();


    }

    private void OnEnable()
    {
        if (editMode)
        {
            cityNameInput.onValueChanged.AddListener(OnCityNameChanged);
            cityDescriptionInput.onValueChanged.AddListener(OnCityDescriptionChanged);
            cityPopulationInput.onValueChanged.AddListener(OnCityPopulationChanged);
        }
    }
    private void OnDisable()
    {
        if (editMode)
        {
            cityNameInput.onValueChanged.RemoveListener(OnCityNameChanged);
            cityDescriptionInput.onValueChanged.RemoveListener(OnCityDescriptionChanged);
            cityPopulationInput.onValueChanged.RemoveListener(OnCityPopulationChanged);
        }
    }

    private void OnCityNameChanged(string newName)
    {
        City city = GetSelectedCityFromColor(SelectedColor);
        if (city != null)
        {
            city.cityName = newName;
            GenerateCityLabel(SelectedColor);
        }
    }
    private void OnCityDescriptionChanged(string newDescription)
    {
        City city = GetSelectedCityFromColor(SelectedColor);
        if (city != null)
        {
            city.description = newDescription;
        }
    }
    private void OnCityPopulationChanged(string newPopulation)
    {
        City city = GetSelectedCityFromColor(SelectedColor);
        city.citizens.Clear();
        if (city != null)
        {
            if (int.TryParse(newPopulation, out int population))
            {
                city.GenerateCitizens(population);
            }
        }
    }

    private void GenerateCityLabels()
    {
        var uniqueColors = FindAllUniqueColorsInTexture();
        foreach (var color in uniqueColors)
        {
            GenerateCityLabel(color);
        }
    }

    private void GenerateCityLabel(Color color)
    {
        var pixels = GetProvincePixels(color);
        City city = GetSelectedCityFromColor(color);
        if (city)
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

            CreateCityNameText(city, worldPoint1, worldPoint2, worldAngle1, worldAngle2, textHeightOffset);
        }
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,256))
        {
            SelectColor(_texture.GetPixel((int)(hit.textureCoord.x * _mapWidth), (int)(hit.textureCoord.y * _mapHeight)));
        }

    }

    private void SelectColor(Color color)
    {
        Debug.Log("Selected color: <color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + ColorUtility.ToHtmlStringRGB(color) + "</color>");

        SelectedColor = color;
        EventManager.TriggerEvent(GameConstants.GameEvents.SELECTED_CITY, new EventParam());

        if (editMode)
        {
            City selectedCity = GetSelectedCityFromColor(color);
            if (selectedCity != null)
            {
                cityNameInput.text = selectedCity.cityName;
                cityDescriptionInput.text = selectedCity.description;
                cityPopulationInput.text = selectedCity.Population.ToString();
            }
        }
    }

    public City GetSelectedCityFromColor(Color selectedColor)
    {
        var cityDefs = GameManager.Instance.cityDefinitions;
        // Find the city definition that is closest to the selected color
        CityDefiniton cd = cityDefs.OrderBy(c =>
        {
            float distance = c.Color.Distance(selectedColor);

            return distance;
        }
        ).Where(c => c.Color.Distance(selectedColor) < GameConstants.Instance.mapMaxColorDistanceDetection).FirstOrDefault();

        List<City> instances = GameManager.Instance.cities;
        City city = null;
        if(cd)
        {
            if (instances.Exists(c => c.id == cd.city.id))
            {
                city = instances.Find(c => c.id == cd.city.id);
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
    private void CreateCityNameText(City city, Vector3 pos1, Vector3 pos2, Vector3 angle1, Vector3 angle2, float heightOffset)
    {
        TMP_Text cityText = Pool.CityText.Spawn<TMP_Text>(Vector3.zero, Quaternion.identity);
        cityText.transform.SetParent(cityTextParent);
        cityText.name = "CityLabel_" + city.id;
        cityText.text = city.cityName;
        Vector3 midPoint = (pos1 + pos2 + angle1 + angle2) / 4f;
        midPoint.y = cityTextParent.position.y + heightOffset;
        cityText.transform.position = midPoint;
        // Set x bounds of the text box to fit between angle1 and angle2
        cityText.rectTransform.sizeDelta = new Vector2(Vector3.Distance(angle1, angle2) * 10f, cityText.rectTransform.sizeDelta.y);
        // Set its rotation to angle between pos1 and pos2
        Vector3 direction = (angle2 - angle1).normalized;
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        cityText.transform.rotation = Quaternion.Euler(90f, -angle, 0f);
        cityText.transform.localScale = Vector3.one;
        cityText.transform.localScale *= GameConstants.Instance.mapTextScaleFactor;
    }
}