using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New City", menuName = "Corrupta/New City...")]
public class City : ListedScriptableObject<City>
{
    public int id;
    public string cityName;
    public string description;
    public Person mayor;
    [Header("Generation Settings")]
    List<IdeologyRate> ideologyRates;
    List<PartyRate> partyRates;

    public List<CitizenGroup> citizens;

    public int Population => citizens.Count * CitizenGroup.POP_PER_GROUP;


    [Button("Add Random Citizens")]
    public void GenerateCitizens(int count)
    {
        //Separating
        List<int> ideologyWeights = new List<int>();
        List<int> partyWeights = new List<int>();

        List<Ideology> ideologies = new List<Ideology>();
        List<Party> parties = new List<Party>();

        ideologyRates.ForEach(i => ideologyWeights.Add(i.rate));
        partyRates.ForEach(p => partyWeights.Add(p.rate));

        ideologyRates.ForEach(i => ideologies.Add(i.ideology));
        partyRates.ForEach(p => parties.Add(p.party));

        for (int i = 0; i < count; i++)
        {
            Ideology ideology = ideologies.GetWeightedRandomElement(ideologyWeights);
            Party party = parties.GetWeightedRandomElement(ideologyWeights);

            Occupation occupation = Occupation.GetInstances().GetRandomElement();

            float wealth = UnityEngine.Random.Range(occupation.wealthRange.x, occupation.wealthRange.y);
            float education = UnityEngine.Random.Range(occupation.educationRange.x, occupation.educationRange.y);
            float partizanship = UnityEngine.Random.Range(occupation.partizanshipRange.x, occupation.partizanshipRange.y);
            citizens.Add(new CitizenGroup(party, ideology, Occupation.GetInstances().GetRandomElement(), wealth, education, partizanship));
        }
    }

    [System.Serializable]
    public class IdeologyRate
    {
        public Ideology ideology;
        [Range(1,10)] public int rate;
    }
    [System.Serializable]
    public class PartyRate
    {
        public Party party;
        [Range(1,10)] public int rate;
    }
}