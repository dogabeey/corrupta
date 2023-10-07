using System.Collections;
using UnityEngine;
using System.Xml;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "New Ideology", menuName = "Corrupta/New Ideology...")]
public class Ideology  : ListedScriptableObject<Ideology>
{
    public int id;
    public string ideologyName;
    public string description;
}
