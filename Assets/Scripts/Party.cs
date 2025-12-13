using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Party", menuName = "Corrupta/New Party...")]
public class Party : ListedScriptableObject<Party>
{
    public static List<Party> parties = new List<Party>();

    public string partyName;
    public string logo;
    public string color;

    public Person founder;
    public Person chairPerson;
    public Person viceChairPerson;
    public Ideology ideology;

    public List<Person> deputyList = new List<Person>();
    public override void Start()
    {
    }
    public override void Update()
    {
    }
    public override void OnManagerDestroy()
    {
    }
}

