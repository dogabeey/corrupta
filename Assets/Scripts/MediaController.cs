using UnityEngine;

public class MediaController : MonoBehaviour
{
    public Media mediaSO;
    public Ideology ideology;
    public float influence;

    public void Init(Media mediaSO)
    {
        this.mediaSO = mediaSO;
        ideology = mediaSO.ideology;
        influence = mediaSO.influence;
    }
}
