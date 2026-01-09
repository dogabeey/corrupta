using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class CitizenGroup
{
    public const int POP_PER_GROUP = 100000;

    public Party party;
    public Ideology ideology;
    public Occupation occupation;

    public float wealth;
    public float happiness;
    public float education;
    public float partizanship;

    public CitizenGroup()
    {
        
    }

    public CitizenGroup(Party party, Ideology ideology, Occupation occupation, float wealth, float happiness, float education, float partizanship)
    {
        this.party = party;
        this.ideology = ideology;
        this.occupation = occupation;
        this.wealth = wealth;
        this.happiness = happiness;
        this.education = education;
        this.partizanship = partizanship;
    }
}
