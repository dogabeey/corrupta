using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


/// <summary>
/// Citizen groups represent a segment of the population in a city, defined by their political affiliation (party), 
/// ideological beliefs (ideology), and economic role (occupation). Each group has attributes such as wealth, 
/// happiness, education level, and partisanship that influence their behavior and interactions within the city. 
/// These groups are essential for simulating the social dynamics and political landscape of the city, as they can 
/// affect voting patterns, public opinion, and overall city development. You can think them as a single voter, 
/// but instead of representing a single person, they represent a group of people with same approximate characteristics 
/// and preferences.
/// </summary>
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
