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


        CityDefiniton cd = CityDefiniton.cityDefs.Find(
            x => x.r == Mathf.FloorToInt(mapDrawer.selectedColor.r * 255)
            && x.g == Mathf.FloorToInt(mapDrawer.selectedColor.g * 255)
            && x.b == Mathf.FloorToInt(mapDrawer.selectedColor.b * 255)
        );

        if(City.cityList.Exists(x => x.id == cd.cityId))
        {
            City city = City.cityList.Find(x => x.id == cd.cityId);
            Debug.Log("Selected " + city.cityName);
        }
    }
}
