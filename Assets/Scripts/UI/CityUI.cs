using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityUI : MonoBehaviour
{
    MapDrawer mapDrawer;
    Toggle toggle;
    ToggleGroup tg;
    // Start is called before the first frame update
    void Start()
    {
        mapDrawer = FindObjectOfType<MapDrawer>();
        toggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mapDrawer.selectedColor != Color.black) ShowCityInfo();
    }

    public void ShowCityInfo()
    {
        toggle.isOn = true;
        CityDefiniton cd = CityDefiniton.cityDefs.Find(
            x => x.r == mapDrawer.selectedColor.r 
            && x.g == mapDrawer.selectedColor.g 
            && x.b == mapDrawer.selectedColor.b
        );
        City.cityList.Find(x => x.id == cd.cityId);
    }
}
