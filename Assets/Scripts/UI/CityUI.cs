using System.Collections;
using System.Collections.Generic;
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


        CityDefiniton cd = CityDefiniton.GetInstances().Find(
            x => x.r == Mathf.FloorToInt(mapDrawer.selectedColor.r * 255)
            && x.g == Mathf.FloorToInt(mapDrawer.selectedColor.g * 255)
            && x.b == Mathf.FloorToInt(mapDrawer.selectedColor.b * 255)
        );

        List<City> instances = City.GetInstances();
        if (instances.Exists(c => c == cd.city))
        {
            City city = instances.Find(c => c == cd.city);
            Debug.Log("Selected " + city.cityName);
        }
    }
}
