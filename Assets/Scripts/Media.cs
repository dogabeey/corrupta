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
}

