using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityDefiniton
{
    public int cityId;
    public int r, g, b;
    public static List<CityDefiniton> cityDefs = new List<CityDefiniton>();

    public CityDefiniton()
    {

    }

    public CityDefiniton(int cityId, int r, int g, int b)
    {
        this.cityId = cityId;
        this.r = r;
        this.g = g;
        this.b = b;
        cityDefs.Add(this);
    }
}
