using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;

public class CityNameText : MonoBehaviour
{
    public Color cityColor;
    public TMP_Text cityText;

    // Draw the city name on the map based on the city ID
    public void DrawCityName(Color cityColor)
    {
        // Get the city name from the city ID
        City city = City.GetCityByColor(cityColor);
        string cityName = "";
        if (city)
        {
            cityName = city.cityName;
        }
        cityText.text = cityName;

        // Get reverse of the color
        Color reverseColor = new Color(1 - cityColor.r, 1 - cityColor.g, 1 - cityColor.b);
        cityText.color = reverseColor;

    }

    private void Start()
    {
        DrawCityName(cityColor);
    }
}
