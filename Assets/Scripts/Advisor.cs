using UnityEngine;
using System;
using System.Collections.Generic;

public class Advisor<T> where T : AdvisorType
{
    public T type;
    public string advisorName;
    public float costMultiplier;
    public float effectBonusMultiplier;
    public float apCostMultiplier;
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

public class PRDirector : AdvisorType
{
    public override string AdvisorTypeName => "Public Relations Director";
    private List<AdvisorAbility> abilities = new List<AdvisorAbility>();
    public override List<AdvisorAbility> Abilities => abilities;
    public override Sprite AdvisorIcon => null;
    public override float CostMultiplier => 1f;
}
public class ChiefOfStaff : AdvisorType
{
    public override string AdvisorTypeName => "Chief of Staff";
    private List<AdvisorAbility> abilities = new List<AdvisorAbility>();
    public override List<AdvisorAbility> Abilities => abilities;
    public override Sprite AdvisorIcon => null;
    public override float CostMultiplier => 1f;
}
public class FinanceChairman : AdvisorType
{
    public override string AdvisorTypeName => "Finance Chairman";
    private List<AdvisorAbility> abilities = new List<AdvisorAbility>();
    public override List<AdvisorAbility> Abilities => abilities;
    public override Sprite AdvisorIcon => null;
    public override float CostMultiplier => 1f;
}
public class OppositionResearcher : AdvisorType
{
    public override string AdvisorTypeName => "Opposition Researcher";
    private List<AdvisorAbility> abilities = new List<AdvisorAbility>();
    public override List<AdvisorAbility> Abilities => abilities;
    public override Sprite AdvisorIcon => null;
    public override float CostMultiplier => 1f;
}