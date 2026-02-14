using UnityEngine;
using System;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Advisor<T> where T : AdvisorType
{
    public T type;
    public int advisorPortraitID;
    public string advisorName;
    public float costMultiplier;
    public float effectBonusMultiplier;
    public float apCostMultiplier;

    public float Cost => GameConstants.Instance.baseAdvsiorCost * type.CostMultiplier * costMultiplier;
}

// Non-generic interface so different Advisor<T> can live in one list
public interface IAdvisor
{
    string AdvisorName { get; }
    AdvisorType Type { get; }
    float CostMultiplier { get; }
    float EffectBonusMultiplier { get; }
    float ApCostMultiplier { get; }
}

// Adapter base to reduce boilerplate
public abstract class AdvisorBase : IAdvisor
{
    public abstract string AdvisorName { get; }
    public abstract AdvisorType Type { get; }
    public abstract float CostMultiplier { get; }
    public abstract float EffectBonusMultiplier { get; }
    public abstract float ApCostMultiplier { get; }
}

// Concrete wrapper for Advisor<T>
[Serializable]
public sealed class AdvisorEntry<T> : AdvisorBase where T : AdvisorType
{
    public Advisor<T> advisor = new Advisor<T>();

    public override string AdvisorName => advisor.advisorName;
    public override AdvisorType Type => advisor.type;
    public override float CostMultiplier => advisor.costMultiplier;
    public override float EffectBonusMultiplier => advisor.effectBonusMultiplier;
    public override float ApCostMultiplier => advisor.apCostMultiplier;
}

public abstract class AdvisorType
{
    public abstract string AdvisorTypeName { get; }
    public abstract List<AdvisorAbility> Abilities { get; }
    public abstract Sprite AdvisorIcon { get; }
    public abstract float CostMultiplier { get; }
}
public class LegalCounsel : AdvisorType
{
    public override string AdvisorTypeName => "Legal Counsel";
    public override Sprite AdvisorIcon => null;
    public override List<AdvisorAbility> Abilities => new List<AdvisorAbility>();
    public override float CostMultiplier => 1f;

}
public class PRDirector : AdvisorType
{
    public override string AdvisorTypeName => "Public Relations Director";
    public override List<AdvisorAbility> Abilities => new List<AdvisorAbility>();
    public override Sprite AdvisorIcon => Addressables.LoadAssetAsync<Sprite>(GameConstants.Gfx.Icons.AdvisorIcons.pr_manager).Result;
    public override float CostMultiplier => 1f;
}
public class ChiefOfStaff : AdvisorType
{
    public override string AdvisorTypeName => "Chief of Staff";
    public override List<AdvisorAbility> Abilities => new List<AdvisorAbility>();
    public override Sprite AdvisorIcon => Addressables.LoadAssetAsync<Sprite>(GameConstants.Gfx.Icons.AdvisorIcons.chief_of_staff).Result;
    public override float CostMultiplier => 1f;
}
public class FinanceChairman : AdvisorType
{
    public override string AdvisorTypeName => "Finance Chairman";
    public override List<AdvisorAbility> Abilities => new List<AdvisorAbility>();
    public override Sprite AdvisorIcon => Addressables.LoadAssetAsync<Sprite>(GameConstants.Gfx.Icons.AdvisorIcons.finance_chairman).Result;
    public override float CostMultiplier => 1f;
}
public class OppositionResearcher : AdvisorType
{
    public override string AdvisorTypeName => "Opposition Researcher";
    private List<AdvisorAbility> abilities = new List<AdvisorAbility>();
    public override List<AdvisorAbility> Abilities => abilities;
    public override Sprite AdvisorIcon => Addressables.LoadAssetAsync<Sprite>(GameConstants.Gfx.Icons.AdvisorIcons.opposition_researcher).Result;
    public override float CostMultiplier => 1f;
}