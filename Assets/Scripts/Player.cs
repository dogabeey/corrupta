using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour, ISaveable
{
	public Person playerPerson;
	public int remainingAP = 10;

	// ISaveable
	public string SaveId => $"Player";
	public SaveDataType SaveDataType => SaveDataType.WorldProgression;

	private void Awake()
	{
		// Ensure this instance participates in saving.
		if (SaveManager.Instance != null)
		{
			SaveManager.Instance.Register(this);
		}
	}

	public Dictionary<string, object> Save()
	{
		var data = new Dictionary<string, object>();
		data["remainingAP"] = remainingAP;
		data["playerPersonId"] = playerPerson != null ? playerPerson.id : -1;
		return data;
	}

	public bool Load(Action onLoadSuccess, Action onLoadFail)
	{
		var node = SaveManager.Instance != null ? SaveManager.Instance.LoadSave(this) : null;
		if (node == null)
		{
			onLoadFail?.Invoke();
			return false;
		}

		try
		{
			// SimpleJSON indexers return default values (0/"") when missing, so also check key presence.
			var obj = node.AsObject;

			if (obj != null && obj.HasKey("remainingAP"))
			{
				remainingAP = obj["remainingAP"].AsInt;
			}

			if (obj != null && obj.HasKey("playerPersonId"))
			{
				int personId = obj["playerPersonId"].AsInt;
				playerPerson = personId >= 0 ? Person.GetInstanceByID(personId) : null;
			}

			onLoadSuccess?.Invoke();
			return true;
		}
		catch
		{
			onLoadFail?.Invoke();
			return false;
		}
	}
}
