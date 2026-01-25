using UnityEngine;

public class PersonController : MonoBehaviour
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
