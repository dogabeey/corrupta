using UnityEngine;
// Turn actions are actions player can take every turn, like assign advisors, make decisions, etc.
public interface ITurnAction<T1, T2>
{
    public abstract string ActionName { get; }
    int ActionPointCost { get; }
    int CompletionTime { get; }
    Sprite ActionIcon { get; }
    void Execute(T1 source, T2 target);
}

public abstract class CharacterAbility : ITurnAction<Person, Person>
{
    public abstract string ActionName { get; }
    public abstract Sprite ActionIcon { get; }
    public abstract int ActionPointCost { get; }
    public abstract int CompletionTime { get; }
    public abstract void Execute(Person source, Person target);
}
public abstract class AdvisorAbility : ITurnAction<Person, Person>
{
    public abstract string ActionName { get; }
    public abstract Sprite ActionIcon { get; }
    public abstract int ActionPointCost { get; }
    public abstract int CompletionTime { get; }
    public abstract void Execute(Person source, Person target);
}