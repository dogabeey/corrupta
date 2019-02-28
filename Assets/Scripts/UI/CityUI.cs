using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityUI : MonoBehaviour
{
    [Tooltip("The image gameobject that contains game map, parented by a scrollview's content")] public GameObject bgImage;
    public GameObject cityMarkerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started popping " + City.cityList.Count + " city.");
        foreach (City city in City.cityList)
        {
            Debug.Log("Adding " + city.cityName + " to map.");
            GameObject inst = Instantiate(cityMarkerPrefab,bgImage.transform);
            inst.transform.localPosition = city.coordinates;
            inst.GetComponentInChildren<Text>().text = city.cityName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
