using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Media", menuName = "Corrupta/New Media...")]
public class Media : ListedScriptableObject<Media>
{

    string name;
    Person founder;
    Person boss;
    Ideology ideology;

    enum mediaType { newspaper, tvChannel, blog }
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

