using System;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    void Start();
    void Update();
    void OnDestroy();
    
}
public class ObjectController : IController
{
    public virtual void Start()
    {

    }
    public virtual void Update()
    {

    }
    public virtual void OnDestroy()
    {

    }
}

[Serializable]
public class CityController : ObjectController
{
    public City citySO;
    public Person mayor;
    public List<CitizenGroup> citizens;

    public CityController(City citySO, Person mayor, List<CitizenGroup> citizens)
    {
        this.citySO = citySO;
        this.mayor = mayor;
        this.citizens = citizens;
    }

    public int Population => citizens.Count * CitizenGroup.POP_PER_GROUP;

    public void Init(City citySO)
    {
        this.citySO = citySO;
        mayor = citySO.mayor;
        citizens = citySO.citizens;
    }
}
