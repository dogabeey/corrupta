using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "New Occupation", menuName = "Corrupta/Social Classes/New Occupation...")]
public class Occupation
{
    public int id;
    public string className;
    public float avgWealth;
    public float avgEducation;
    public float avgPartizanship;
}