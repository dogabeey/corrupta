using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Sirenix.OdinInspector;
using System.Linq;

[CreateAssetMenu(fileName = "New City", menuName = "Corrupta/New City...")]
public class City : ListedScriptableObject<City>
{
    public string cityName;
    public string description;
    public Person mayor;
    public List<CitizenGroup> citizens;
    [Header("Generation Settings")]
    public List<PartyRate> partyRates;
    public List<IdeologyRate> ideologyRates;
    public List<OccupationRate> occuptionRates;

    public int Population => citizens.Count * CitizenGroup.POP_PER_GROUP;
    public float SurfaceAreaInKm2 => surfaceSizeByPixel;
    public string SurfaceAreaInKm2String => (SurfaceAreaInKm2 / 1000).ToString("N1") + " km²";

    internal int surfaceSizeByPixel;
    public override void Start()
    {

    }
    public override void Update()
    {

    }
    public override void OnManagerDestroy()
    {

    }

    [Button("Add Random Citizens")]
    public void GenerateCitizens(int count)
    {
        List<int> ideologyWeights = new List<int>();
        List<int> partyWeights = new List<int>();
        List<int> occupationWeights = new List<int>();

        List<Ideology> ideologies = GameManager.Instance.ideologies;
        List<Party> parties = GameManager.Instance.parties;
        List<Occupation> occupations = GameManager.Instance.occupations;

        ideologyRates.ForEach(p => ideologyWeights.Add(p.rate));
        partyRates.ForEach(p => partyWeights.Add(p.rate));
        occuptionRates.ForEach(p => occupationWeights.Add(p.rate));

        for (int i = 0; i < count; i++)
        {
            Ideology ideology = ideologies.GetWeightedRandomElement(ideologyWeights);
            Party party = parties.GetWeightedRandomElement(partyWeights);
            Occupation occupation = occupations.GetWeightedRandomElement(occupationWeights);

            float wealth = UnityEngine.Random.Range(occupation.wealthRange.x, occupation.wealthRange.y);
            float education = UnityEngine.Random.Range(occupation.educationRange.x, occupation.educationRange.y);
            float partizanship = UnityEngine.Random.Range(occupation.partizanshipRange.x, occupation.partizanshipRange.y);
            citizens.Add(new CitizenGroup(party, ideology, occupation, wealth, education, partizanship));
        }
    }

    private void OnValidate()
    {
        // Add all ideologies that are missing in the ideology rates list
        var allIdeologies = Ideology.GetInstances();
        foreach (var ideology in allIdeologies)
        {
            if (!ideologyRates.Any(ir => ir.ideology == ideology))
            {
                ideologyRates.Add(new IdeologyRate { ideology = ideology, rate = 0 });
            }
        }
        // Add all parties that are missing in the party rates list
        var allParties = Party.GetInstances();
        foreach (var party in allParties)
        {
            if (!partyRates.Any(pr => pr.party == party))
            {
                partyRates.Add(new PartyRate { party = party, rate = 0 });
            }
        }
        // Add all occupations that are missing in the occupation rates list
        var allOccupations = Occupation.GetInstances();
        foreach (var occupation in allOccupations)
        {
            if (!occuptionRates.Any(or => or.occupation == occupation))
            {
                occuptionRates.Add(new OccupationRate { occupation = occupation, rate = 0 });
            }
        }
    }

    [System.Serializable]
    public class IdeologyRate
    {
        public Ideology ideology;
        [Range(0,10)] public int rate;
    }
    [System.Serializable]
    public class PartyRate
    {
        public Party party;
        [Range(0,10)] public int rate;
    }
    public class OccupationRate
    {
        public Occupation occupation;
        [Range(0, 10)] public int rate;
    }
    public static CityDefiniton GetCityDefinition(int id)
    {
        return GameManager.Instance.cityDefinitions.Find(c => c.city.id == id);
    }
}