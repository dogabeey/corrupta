using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CitizenGroup
{
    public const int POP_PER_GROUP = 100000;

    public Party party;
    public Ideology ideology;
    public Occupation occupation;

    public float wealth;
    public float happiness;
    public float education;
}
