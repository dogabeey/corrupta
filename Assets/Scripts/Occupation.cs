using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Occupation", menuName = "Corrupta/Social Classes/New Occupation...")]
public class Occupation : ListedScriptableObject<Occupation>
{
    public int id;
    public string className;
    [MinMaxSlider(0, 100)]
    public Vector2 wealthRange;
    [MinMaxSlider(0, 100)]
    public Vector2 educationRange;
    [MinMaxSlider(0, 100)]
    public Vector2 partizanshipRange;
}