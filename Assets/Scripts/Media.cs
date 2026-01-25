using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Media", menuName = "Corrupta/New Media...")]
public class Media : ListedScriptableObject<Media>
{

    public string mediaName;
    public Ideology ideology;
    public float influence;
    public MediaType mediaType;

    public enum MediaType { newspaper, tvChannel, blog }
    public override void Start()
    {
    }
    public override void Update()
    {
    }
    public override void OnManagerDestroy()
    {
    }
}
