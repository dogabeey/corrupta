using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CityUI : ToggleUIBehaviour
{
    public MapDrawer mapDrawer;

    protected override string UpdateEventString { get => GameConstants.GameEvents.SELECTED_CITY; }

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mapDrawer.selectedColor != Color.black)
            DrawUI();
    }

    public override void DrawUI()
    {
        toggle.isOn = true;

        float searchedRed = Mathf.FloorToInt(mapDrawer.selectedColor.r * 255);
        float searchedGreen = Mathf.FloorToInt(mapDrawer.selectedColor.g * 255);
        float searchedBlue = Mathf.FloorToInt(mapDrawer.selectedColor.b * 255);

        var cityDefs = CityDefiniton.GetInstances();
        // Find the city definition that is closest to the selected color
        CityDefiniton cd = cityDefs.OrderBy(c =>
        {
            float distance = c.Color.Distance(mapDrawer.selectedColor);
            return distance;
        }
        ).First();

        List<City> instances = City.GetInstances();
        if (instances.Exists(c => c == cd.city))
        {
            City city = instances.Find(c => c == cd.city);
            Debug.Log("Selected " + city.cityName);
        }
    }
}
