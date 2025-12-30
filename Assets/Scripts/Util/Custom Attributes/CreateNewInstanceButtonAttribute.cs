using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class CreateNewInstanceButtonAttribute : Attribute
{
    public string buttonLabel;
    public AssetPathEnum pathEnum;

    public CreateNewInstanceButtonAttribute(
        string buttonLabel = "+",
        AssetPathEnum pathEnum = AssetPathEnum.People
    )
    {
        this.buttonLabel = buttonLabel;
        this.pathEnum = pathEnum;
    }

    public string GetPathFromEnum(AssetPathEnum pathEnum)
    {
        switch (pathEnum)
        {
            case AssetPathEnum.People:
                return "Assets/Resources/ScriptableObjects/People";
            case AssetPathEnum.Parties:
                return "Assets/Resources/ScriptableObjects/Parties";
            case AssetPathEnum.Ideologies:
                return "Assets/Resources/ScriptableObjects/Ideologies";
            case AssetPathEnum.Occupations:
                return "Assets/Resources/ScriptableObjects/Occupations";
            case AssetPathEnum.Medias:
                return "Assets/Resources/ScriptableObjects/Medias";
            case AssetPathEnum.CityDefinitions:
                return "Assets/Resources/ScriptableObjects/CityDefinitions";
            case AssetPathEnum.Cities:
                return "Assets/Resources/ScriptableObjects/Cities";
            default:
                Debug.LogError("Invalid AssetPathEnum provided.");
                return string.Empty;
        }
    }
}

public enum AssetPathEnum
{
    People,
    Cities,
    Parties,
    Ideologies,
    Occupations,
    Medias,
    CityDefinitions
}