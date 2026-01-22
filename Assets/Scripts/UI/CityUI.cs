using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CityUI : ToggleUIBehaviour
{
    public MapDrawer mapDrawer;
    public TMP_Text cityNameText;
    public TMP_Text populationText;
    public TMP_Text descriptionText;

    protected override string UpdateEventString { get => GameConstants.GameEvents.SELECTED_CITY; }

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        if (mapDrawer.SelectedColor.r != 0 || mapDrawer.SelectedColor.g != 0 || mapDrawer.SelectedColor.b != 0)
            DrawUI();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void DrawUI()
    {
        toggle.isOn = true;
        City city = mapDrawer.GetSelectedCityFromColor(mapDrawer.SelectedColor);
        if(city)
        {
            cityNameText.text = city.cityName;
            populationText.text = "Population: " + city.Population.ToString("N0");
            descriptionText.text = city.description;
        }
    }

}
