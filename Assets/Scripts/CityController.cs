using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{
    public City citySO;
    public List<CitizenGroup> citizens;

    public int Population => citizens.Count * CitizenGroup.POP_PER_GROUP;

}
