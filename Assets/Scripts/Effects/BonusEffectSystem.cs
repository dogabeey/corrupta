using System;
using System.Collections.Generic;
using UnityEngine;

public enum BonusTargetStat
{
    None = 0,

    // Person stats
    PersonManagement,
    PersonDiplomacy,
    PersonWisdom,
    PersonSpeech,
    PersonIntrigue,

    // Opinion / popularity-style stats
    Popularity,

    // Country stats (example)
    CountryDiplomacy,
}

public enum BonusOperation
{
    Add = 0,
    Multiply = 1
}

public enum BonusScope
{
    Global = 0,
    OnlyOccupation = 1,
    OnlyIdeology = 2,
    OccupationAndIdeology = 3
}

[Serializable]
public struct BonusEffect
{
    [Tooltip("What stat/value this bonus modifies")]
    public BonusTargetStat target;

    [Tooltip("Add = flat (+1). Multiply = multiplier (+5% = 1.05)")]
    public BonusOperation operation;

    [Tooltip("For Add: +1, -2, etc. For Multiply: 1.05 for +5%, 0.9 for -10%")]
    public float value;

    [Tooltip("Where this bonus applies (global or filtered)")]
    public BonusScope scope;

    [Tooltip("Optional filter when scope requires occupation")]
    public Occupation occupation;

    [Tooltip("Optional filter when scope requires ideology")]
    public Ideology ideology;

    [Tooltip("UI-friendly label override (optional). When empty, a label is generated.")]
    public string customLabel;

    public bool AppliesTo(Occupation occ, Ideology ideo)
    {
        switch (scope)
        {
            case BonusScope.Global:
                return true;
            case BonusScope.OnlyOccupation:
                return occupation != null && occupation == occ;
            case BonusScope.OnlyIdeology:
                return ideology != null && ideology == ideo;
            case BonusScope.OccupationAndIdeology:
                return occupation != null && ideology != null && occupation == occ && ideology == ideo;
            default:
                return false;
        }
    }

    public string GetLabel()
    {
        if (!string.IsNullOrWhiteSpace(customLabel)) return customLabel;

        string targetStr = target.ToString();
        string scopeStr = scope switch
        {
            BonusScope.Global => string.Empty,
            BonusScope.OnlyOccupation => occupation ? $" ({occupation.className})" : " (Occupation)",
            BonusScope.OnlyIdeology => ideology ? $" ({ideology.name})" : " (Ideology)",
            BonusScope.OccupationAndIdeology => $" ({(occupation ? occupation.className : "Occupation")} + {(ideology ? ideology.name : "Ideology")})",
            _ => string.Empty
        };

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
    public static float ApplyAdditiveThenMultiplicative(float baseValue, IEnumerable<BonusEffect> bonuses, BonusTargetStat stat, Occupation occ = null, Ideology ideo = null)
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
