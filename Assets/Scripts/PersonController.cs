using System;
using UnityEngine;

[Serializable]
public class PersonController : ObjectController
{
    public Person personSO;
    public float fame;
    public float personAge;
    public float corruption;
    public Ideology ideology;

    public int baseManagement;
    public int baseDiplomacy;
    public int baseWisdom;
    public int baseSpeech;
    public int baseIntrigue;

    public PersonController(Person personSO, float fame, float personAge, float corruption, Ideology ideology, int baseManagement, int baseDiplomacy, int baseWisdom, int baseSpeech, int baseIntrigue)
    {
        this.personSO = personSO;
        this.fame = fame;
        this.personAge = personAge;
        this.corruption = corruption;
        this.ideology = ideology;
        this.baseManagement = baseManagement;
        this.baseDiplomacy = baseDiplomacy;
        this.baseWisdom = baseWisdom;
        this.baseSpeech = baseSpeech;
        this.baseIntrigue = baseIntrigue;
    }

    public int Management => baseManagement;
    public int Diplomacy => baseDiplomacy;
    public int Wisdom => baseWisdom;
    public int Speech => baseSpeech;
    public int Intrigue => baseIntrigue;

    public void Init(Person personSO)
    {
        this.personSO = personSO;
        fame = personSO.fame;
        personAge = personSO.personAge;
        corruption = personSO.corruption;
        ideology = personSO.ideology;
        baseManagement = personSO.baseManagement;
        baseDiplomacy = personSO.baseDiplomacy;
        baseWisdom = personSO.baseWisdom;
        baseSpeech = personSO.baseSpeech;
        baseIntrigue = personSO.baseIntrigue;
    }

}
