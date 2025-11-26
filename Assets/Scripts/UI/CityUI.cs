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
        if (mapDrawer.SelectedColor != Color.black)
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
             // Blah blah blah
        }
    }

}
