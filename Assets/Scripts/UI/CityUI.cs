using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityUI : MonoBehaviour
{
    public MapDrawer mapDrawer;

    Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mapDrawer.selectedColor != Color.black)
            ShowCityInfo();
    }

    public void ShowCityInfo()
    {
        toggle.isOn = true;


        CityDefiniton cd = CityDefiniton.GetInstances().Find(
            x => x.r == Mathf.FloorToInt(mapDrawer.selectedColor.r * 255)
            && x.g == Mathf.FloorToInt(mapDrawer.selectedColor.g * 255)
            && x.b == Mathf.FloorToInt(mapDrawer.selectedColor.b * 255)
        );


        if(City.cityList.Exists(c => c == cd.city))
        {
            City city = City.cityList.Find(c => c == cd.city);
            Debug.Log("Selected " + city.cityName);
        }
    }
}
