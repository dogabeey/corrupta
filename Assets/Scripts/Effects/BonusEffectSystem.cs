using System;
using System.Collections.Generic;
using UnityEngine;

public enum BonusStat
{
    None = 0,

    // Person stats
    Management,
    Diplomacy,
    Wisdom,
    Speech,
    Intrigue,

    // Cost related
    AllCostReduction,
    AdvisorCostReduction,
    AdvisorSalaryReduction,


    // Opinion / popularity-style stats
    MonthlyPopularity,


    // Country stats (example)
    CountryDiplomacy,
}

public enum BonusOperation
{
    Add = 0,
    Multiply = 1
}

[Flags]
public enum BonusTargetScope
{
    Global = 0,
    Occupation = 1 << 0,
    Ideology = 1 << 1,
    City = 1 << 2,
}

[Serializable]
public struct BonusEffect
{
    [Tooltip("What stat/value this bonus modifies")]
    public BonusStat target;

    [Tooltip("Add = flat (+1). Multiply = multiplier (+5% = 1.05)")]
    public BonusOperation operation;

    [Tooltip("For Add: +1, -2, etc. For Multiply: 1.05 for +5%, 0.9 for -10%")]
    public float value;

    [Tooltip("Where this bonus applies (global or filtered). Use flags to combine (Occupation | Ideology).")]
    public BonusTargetScope scope;

    [Tooltip("Optional filter when scope includes Occupation")]
    public Occupation occupation;

    [Tooltip("Optional filter when scope includes Ideology")]
    public Ideology ideology;

    public float bonusEffectCostMultiplier;

    [Tooltip("UI-friendly label override (optional). When empty, a label is generated.")]
    public string customLabel;

    public readonly bool AppliesTo(Occupation occ, Ideology ideo)
    {
        // Global means no filtering.
        if (scope == BonusTargetScope.Global) return true;

        // If a flag is set, the corresponding filter must match.
        if (scope.HasFlag(BonusTargetScope.Occupation))
        {
            if (occupation == null) return false;
            if (occ == null) return false;
            if (occupation != occ) return false;
        }

        if (scope.HasFlag(BonusTargetScope.Ideology))
        {
            if (ideology == null) return false;
            if (ideo == null) return false;
            if (ideology != ideo) return false;
        }

        return true;
    }

    public readonly string GetLabel()
    {
        if (!string.IsNullOrWhiteSpace(customLabel)) return customLabel;

        string targetStr = target.ToString();
        string scopeStr = string.Empty;

        if (scope != BonusTargetScope.Global)
        {
            string occPart = scope.HasFlag(BonusTargetScope.Occupation)
                ? (occupation ? occupation.name : "Occupation")
                : null;

            string ideoPart = scope.HasFlag(BonusTargetScope.Ideology)
                ? (ideology ? ideology.name : "Ideology")
                : null;

            if (!string.IsNullOrEmpty(occPart) && !string.IsNullOrEmpty(ideoPart))
                scopeStr = $" ({occPart} + {ideoPart})";
            else if (!string.IsNullOrEmpty(occPart))
                scopeStr = $" ({occPart})";
            else if (!string.IsNullOrEmpty(ideoPart))
                scopeStr = $" ({ideoPart})";
        }

        if (operation == BonusOperation.Add)
        {
            string sign = value >= 0 ? "+" : string.Empty;
            return $"{sign}{value:0.#} {targetStr}{scopeStr}";
        }
        else
        {
            // Convert multiplier to percentage delta
            float pct = (value - 1f) * 100f;
            string sign = pct >= 0 ? "+" : string.Empty;
            return $"{sign}{pct:0.#}% {targetStr}{scopeStr}";
        }
    }
}

public interface IBonusEffectSource
{
    IReadOnlyList<BonusEffect> BonusEffects { get; }
}

public static class BonusEffectEvaluator
{
    public static float ApplyAdditiveThenMultiplicative(float baseValue, IEnumerable<BonusEffect> bonuses, BonusStat stat, Occupation occ = null, Ideology ideo = null)
    {
        float add = 0f;
        float mul = 1f;

        foreach (var b in bonuses)
        {
            if (b.target != stat) continue;
            if (!b.AppliesTo(occ, ideo)) continue;

            if (b.operation == BonusOperation.Add) add += b.value;
            else mul *= b.value;
        }

        return (baseValue + add) * mul;
    }

    public static IEnumerable<BonusEffect> CollectBonuses(IEnumerable<IBonusEffectSource> sources)
    {
        if (sources == null) yield break;

        foreach (var s in sources)
        {
            if (s == null) continue;
            var list = s.BonusEffects;
            if (list == null) continue;
            for (int i = 0; i < list.Count; i++)
                yield return list[i];
        }
    }
}
