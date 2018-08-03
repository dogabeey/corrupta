using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class News<Category>
{
    public string headLine;
    public string description;

    public DateTime newsDate = new DateTime(GameObject.FindGameObjectWithTag("simulation").GetComponent<Simulator>().Year + DateTime.Now.Year, GameObject.FindGameObjectWithTag("simulation").GetComponent<Simulator>().Month,0);
}
