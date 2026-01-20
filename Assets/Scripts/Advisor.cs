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
