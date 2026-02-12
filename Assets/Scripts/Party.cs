using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Party", menuName = "Corrupta/New Party...")]
public class Party : ListedScriptableObject<Party>
{
    public static List<Party> parties = new List<Party>();

    public string partyName;
    public string logo;
    public string color;

    [CreateNewInstanceButton(pathEnum: AssetPathEnum.People)]
    public Person founder;
    [CreateNewInstanceButton(pathEnum: AssetPathEnum.People)]
    public Person chairPerson;
    [CreateNewInstanceButton(pathEnum: AssetPathEnum.People)]
    public Person viceChairPerson;
    [CreateNewInstanceButton(pathEnum: AssetPathEnum.Ideologies)]
    public Ideology ideology;
    public List<Person> deputyList;
}

