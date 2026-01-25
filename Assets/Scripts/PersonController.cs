using UnityEngine;

public class PersonController : MonoBehaviour
{
    public Person personSO;
    public float fame;
    public float personAge;
    public float corruption;
    public Ideology ideology;

    public int Management => personSO.baseManagement;
    public int Diplomacy => personSO.baseDiplomacy;
    public int Wisdom => personSO.baseWisdom;
    public int Speech => personSO.baseSpeech;
    public int Intrigue => personSO.baseIntrigue;

}
