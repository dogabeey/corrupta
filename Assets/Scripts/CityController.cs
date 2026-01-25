using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{
    public City citySO;
    public Person mayor;
    public List<CitizenGroup> citizens;

    public int Population => citizens.Count * CitizenGroup.POP_PER_GROUP;

    public void Init(City citySO)
    {
        this.citySO = citySO;
        mayor = citySO.mayor;
        citizens = citySO.citizens;
    }
}
