using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

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

    private void OnValidate()
    {
        if(id == 0)
        {
            var allOccupations = GetInstances();
            int maxId = 0;
            foreach (var occupation in allOccupations)
            {
                if (occupation.id > maxId)
                {
                    maxId = occupation.id;
                }
            }
            id = maxId + 1;
        }
        if(className.IsNullOrWhitespace())
        {
            className = name;
        }
        if(wealthRange.x == wealthRange.y)
        {
            wealthRange.y = Mathf.Min(wealthRange.x + 10, 100);
        }
        if(educationRange.x == educationRange.y)
        {
            educationRange.y = Mathf.Min(educationRange.x + 10, 100);
        }
        if(partizanshipRange.x == partizanshipRange.y)
        {
            partizanshipRange.y = Mathf.Min(partizanshipRange.x + 10, 100);
        }
    }
}