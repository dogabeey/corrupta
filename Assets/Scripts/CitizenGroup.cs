using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenGroup : MonoBehaviour
{
    public const int POP_PER_GROUP = 100000;

    public int partyID;
    public int ideologyID;
    public int occupationID;

    public float wealth;
    public float happiness;
    public float education;

    public Party Party
    {
        get => Party.parties.Find(p => partyID == p.id);
    }
    public Ideology Ideology
    {
        get => Ideology.ideologyList.Find(i => ideologyID == i.id);
    }
    public Occupation Occupation
    {
        get => Occupation.occupations.Find(o => occupationID == o.id);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
