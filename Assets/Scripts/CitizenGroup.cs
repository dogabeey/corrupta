using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Lionsfall.SimpleJSON;

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

    public CitizenGroup()
    {
        
    }

    public CitizenGroup(Party party, Ideology ideology, Occupation occupation, float wealth, float happiness, float education)
    {
        this.party = party;
        this.ideology = ideology;
        this.occupation = occupation;
        this.wealth = wealth;
        this.happiness = happiness;
        this.education = education;
    }
}
