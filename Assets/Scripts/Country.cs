using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

public class Country
{
    public static Country Instance;
    public string name;

    public enum ChangeableStats
    {
        baseMonetaryValue,
        baseGNP,
        baseExportRate,
        baseTourism,
        baseCulture,
        baseJobs,
        baseDiplomacy,
        baseTechnology,
        baseMilitary,
        baseSafety,
        baseSocialPolitics,
        basePublicService,
        baseJustice,
        baseEducation,
        baseTaxRates,
        corruption
    }

    public void UpdateAll()
    {
        SetCulture();
        SetJobs();
        SetDiplomacy();
        SetTaxRates();
        SetJustice();
        SetEducation();
        SetTechnology();
        SetExportRate();
        SetTourism();
        SetGNP();
        SetMonetaryValue();
        SetTechnology();
        SetMilitary();
        SetSafety();
        SetPublicService();
        SetSocialPolitics();

        SetWealth();
        SetPrestige();
        SetStability();
        SetHappiness();
    }

    public float GetGNP()
    {
        return gNP;
    }

    public void SetGNP()
    {
        gNP = (education + technology + jobs) / 3 + baseGNP;
    }

    public float GetMonetaryValue()
    {
        return monetaryValue;
    }

    private void SetMonetaryValue()
    {
        monetaryValue = (3 * exportRate + tourism) / 4 + baseMonetaryValue;
    }

    public float GetExportRate()
    {
        return exportRate;
    }

    private void SetExportRate()
    {
        exportRate = baseExportRate;
    }

    public float GetTourism()
    {
        return tourism;
    }

    private void SetTourism()
    {
        tourism = culture + baseTourism;
    }

    public float GetCulture()
    {
        return culture;
    }

    private void SetCulture()
    {
        culture = baseCulture;
    }

    public float GetJobs()
    {
        return jobs;
    }

    private void SetJobs()
    {
        jobs = baseJobs;
    }

    public float GetDiplomacy()
    {
        return diplomacy;
    }

    private void SetDiplomacy()
    {
        diplomacy = baseDiplomacy;
    }

    public float GetTechnology()
    {
        return technology;
    }

    private void SetTechnology()
    {
        technology = (socialPolitics + 4 * education + gNP) / 6 + baseTechnology;
    }

    public float GetMilitary()
    {
        return military;
    }

    private void SetMilitary()
    {
        military = baseMilitary + technology;
    }

    public float GetSafety()
    {
        return safety;
    }

    private void SetSafety()
    {
        safety = (taxRates + military * 2 + publicService * 4) / 7 + baseSafety;
    }

    public float GetSocialPolitics()
    {
        return socialPolitics;
    }

    private void SetSocialPolitics()
    {
        socialPolitics = taxRates / 5 + baseSocialPolitics;
    }

    public float GetPublicService()
    {
        return publicService;
    }

    private void SetPublicService()
    {
        publicService = taxRates / 2 + basePublicService;
    }

    public float GetJustice()
    {
        return justice;
    }

    private void SetJustice()
    {
        justice = baseJustice;
    }

    public float GetEducation()
    {
        return education;
    }

    private void SetEducation()
    {
        education = baseEducation;
    }

    public float GetTaxRates()
    {
        return taxRates;
    }

    private void SetTaxRates()
    {
        taxRates = baseTaxRates;
    }

    public float GetCorruption()
    {
        return corruption;
    }

    //public void SetCorruption(float value)
    //{
    //    corruption = value;
    //}
    public float GetWealth()
    {
        return wealth;
    }

    private void SetWealth()
    {
        wealth = (2 * gNP + monetaryValue + 3 * taxRates) / 6;
    }

    public float GetPrestige()
    {
        return prestige;
    }

    private void SetPrestige()
    {
        prestige = (4 * diplomacy + 2 * culture + 3 * technology + 3 * military) / 12;
    }

    public float GetStability()
    {
        return stability;
    }

    private void SetStability()
    {
        stability = (3 * justice + 4 * safety + monetaryValue + 2 * military) / 10;
    }

    public float GetHappiness()
    {
        return happiness;
    }

    private void SetHappiness()
    {
        happiness = (jobs + socialPolitics + education * (stability - prestige) + publicService + (justice / corruption) + safety) / (stability - prestige + (corruption<1 ? 1 : 1/corruption) + 4);
    }

    private float education;
    private float gNP;
    private float monetaryValue;
    private float exportRate;
    private float tourism;
    private float culture;
    private float jobs;
    private float diplomacy;
    private float technology;
    private float military;
    private float safety;
    private float socialPolitics;
    private float publicService;
    private float justice;
    private float taxRates;

    private float wealth;
    private float prestige;
    private float stability;
    private float happiness;

    public float baseMonetaryValue = 0;
    public float baseGNP = 0;
    public float baseExportRate = 0;
    public float baseTourism = 0;
    public float baseCulture = 0;
    public float baseJobs = 0;
    public float baseDiplomacy = 0;
    public float baseTechnology = 0;
    public float baseMilitary = 0;
    public float baseSafety = 0;
    public float baseSocialPolitics = 0;
    public float basePublicService = 0;
    public float baseJustice = 0;
    public float baseEducation = 0;
    public float baseTaxRates = 0;
    public float corruption = 0;


    private Country(string name)
    {
        this.name = name;
    }

    public static void InitCountry(string name)
    {
        Instance = new Country(name);

    }

    public void ChangeValue(ChangeableStats stat, float rate)
    {
        FieldInfo field = typeof(Country).GetField(stat.ToString());
        float value = (float)field.GetValue(this);

        field.SetValue(this, value + rate);
    }

    void ChangeValue(string stat, float rate)
    {
        FieldInfo field = typeof(Country).GetField(stat);
        Debug.Log(field.ToString());
        float value = (float)field.GetValue(this);

        field.SetValue(this, value + rate);
    }

    public static void RandomizeAll()
    {
        foreach (string stat in Enum.GetNames(typeof(ChangeableStats)))
        {
            float val = UnityEngine.Random.value * 50;
            Debug.Log("Attemping to change " + stat + " to " + val);
            Instance.ChangeValue(stat, val);
        }
    }
}
