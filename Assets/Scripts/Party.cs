using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Party", menuName = "Corrupta/New Party...")]
public class Party : ListedScriptableObject<Party>
{
    public static List<Party> parties = new List<Party>();

    public string partyName;
    public string logo;
    public string color;

    [InlineButton(nameof(CreateNewFounder), label: "*", ShowIf = nameof(HasNoFounder))]
    public Person founder;
    public Person chairPerson;
    public Person viceChairPerson;
    public Ideology ideology;

    public List<Person> deputyList = new List<Person>();
    public override void Start()
    {
    }
    public override void Update()
    {
    }
    public override void OnManagerDestroy()
    {
    }

    #region Odin Inspector
    public void CreateNewFounder()
    {
        Person newPerson = OdinEditorWindow.CreateInstance<Person>();
        newPerson.CreateNewAssetFromSO();
        founder = newPerson;
    }
    public bool HasNoFounder() => !founder;
    #endregion
}

