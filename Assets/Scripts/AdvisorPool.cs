using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;

public class AdvisorPool : SerializedMonoBehaviour
{
    // Holds multiple advisor types at once
    public List<AdvisorBase> advisors = new List<AdvisorBase>();
    public TextAsset maleNameList, femaleNameList, lastNameList;

    private void Start()
    {
        CreateRandomAdvisorsOfType<PRDirector>(5);
        CreateRandomAdvisorsOfType<FinanceChairman>(5);
        CreateRandomAdvisorsOfType<ChiefOfStaff>(5);
        CreateRandomAdvisorsOfType<OppositionResearcher>(5);

    }

    // Convenience API
    public IEnumerable<AdvisorEntry<T>> GetAdvisorsOfType<T>() where T : AdvisorType
    {
        foreach (var a in advisors)
            if (a is AdvisorEntry<T> typed)
                yield return typed;
    }
    public AdvisorEntry<T> GetAdvisorByName<T>(string name) where T : AdvisorType
    {
        foreach (var a in advisors)
            if (a is AdvisorEntry<T> typed && typed.AdvisorName == name)
                return typed;
        return null;
    }

    public void AddAdvisor<T>(Advisor<T> advisor) where T : AdvisorType
    {
        var entry = new AdvisorEntry<T> { advisor = advisor };
        advisors.Add(entry);
    }
    public Advisor<T> CreateRandomAdvisor<T>(T advisorType) where T : AdvisorType
    {
        var advisor = new Advisor<T>
        {
            type = advisorType,
            advisorName = GenerateRandomName(),
            costMultiplier = Random.Range(0.8f, 1.2f),
            effectBonusMultiplier = Random.Range(0.8f, 1.2f),
            apCostMultiplier = Random.Range(0.8f, 1.2f)
        };
        AddAdvisor(advisor);
        return advisor;
    }
    public void CreateRandomAdvisorsOfType<T>(int number) where T : AdvisorType
    {
        for (int i = 0; i < number; i++)
        {
            T randomType = GetRandomAdvisorType<T>();
            CreateRandomAdvisor(randomType);
        }
    }

    private T GetRandomAdvisorType<T>() where T : AdvisorType
    {
        var allTypes = System.Enum.GetValues(typeof(T)).Cast<T>();
        return allTypes.ElementAt(UnityEngine.Random.Range(0, allTypes.Count()));
    }

    private string GenerateRandomName()
    {
        string firstName = Random.value < 0.5f ? GetRandomNameFromList(maleNameList) : GetRandomNameFromList(femaleNameList);
        string lastName = GetRandomNameFromList(lastNameList);
        return $"{firstName} {lastName}";
    }

    private string GetRandomNameFromList(TextAsset maleNameList)
    {
        string[] names = maleNameList.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        return names[Random.Range(0, names.Length)];
    }
}
