using System;
using UnityEngine;

[Serializable]
public class MediaController : ObjectController
{
    public Media mediaSO;
    public Ideology ideology;
    public float influence;

    public MediaController(Media mediaSO, Ideology ideology, float influence)
    {
        this.mediaSO = mediaSO;
        this.ideology = ideology;
        this.influence = influence;
    }

    public void Init(Media mediaSO)
    {
        this.mediaSO = mediaSO;
        id = mediaSO.id;
        ideology = mediaSO.ideology;
        influence = mediaSO.influence;
    }
}
