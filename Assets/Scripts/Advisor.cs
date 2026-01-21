using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public abstract class AdvisorType
{
    public abstract string AdvisorName { get; }
    public abstract List<AdvisorAbility> Abilities { get; }
    public abstract Sprite AdvisorIcon { get; }
    public abstract float CostMultiplier { get; }
}

public class PRDirector : AdvisorType
{
    public override string AdvisorName => "Public Relations Director";
    private List<AdvisorAbility> abilities = new List<AdvisorAbility>()
    {
    };
    public override List<AdvisorAbility> Abilities => abilities;
    public override Sprite AdvisorIcon => null;
    public override float CostMultiplier => 1f;
}
public class ChiefOfStaff : AdvisorType
{
    public override string AdvisorName => "Chief of Staff";
    private List<AdvisorAbility> abilities = new List<AdvisorAbility>()
    {
    };
    public override List<AdvisorAbility> Abilities => abilities;
    public override Sprite AdvisorIcon => null;
    public override float CostMultiplier => 1f;
}
public class FinanceChairman : AdvisorType
{
    public override string AdvisorName => "Finance Chairman";
    private List<AdvisorAbility> abilities = new List<AdvisorAbility>()
    {
    };
    public override List<AdvisorAbility> Abilities => abilities;
    public override Sprite AdvisorIcon => null;
    public override float CostMultiplier => 1f;
}
public class OppositionResearcher : AdvisorType
{
    public override string AdvisorName => "Opposition Researcher";
    private List<AdvisorAbility> abilities = new List<AdvisorAbility>()
    {
    };
    public override List<AdvisorAbility> Abilities => abilities;
    public override Sprite AdvisorIcon => null;
    public override float CostMultiplier => 1f;
}