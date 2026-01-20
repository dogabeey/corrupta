using UnityEngine;
// Turn actions are actions player can take every turn, like assign advisors, make decisions, etc.
public interface ITurnAction
{
    public abstract string ActionName { get; }
    int ActionPointCost { get; }
    Sprite ActionIcon { get; }
    void Execute();
}

public abstract class CharacterAbility : ITurnAction
{
    public abstract string ActionName { get; }
    public abstract Sprite ActionIcon { get; }
    public abstract int ActionPointCost { get; }
    public abstract void Execute();
}
public abstract class AdvisorAbility : ITurnAction
{
    public abstract string ActionName { get; }
    public abstract Sprite ActionIcon { get; }
    public abstract int ActionPointCost { get; }
    public abstract void Execute();
}