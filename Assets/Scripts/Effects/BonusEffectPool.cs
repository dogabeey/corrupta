using Sirenix.OdinInspector;
using System.Collections.Generic;

public class BonusEffectPool : SerializedMonoBehaviour
{
    public List<BonusEffect> sources = new List<BonusEffect>();

    // Add a bonus effect for each idelogy and each occupation

    public void AddBonusEffect(BonusTargetScope scope, BonusOperation operation, BonusStat target, Ideology ideology, Occupation occupation, float value)
    {
        BonusEffect bonusEffect = new()
        {
            scope = scope,
            operation = operation,
            target = target,
            ideology = ideology,
            occupation = occupation,
            value = value
        };
        sources.Add(bonusEffect);
    }
}
