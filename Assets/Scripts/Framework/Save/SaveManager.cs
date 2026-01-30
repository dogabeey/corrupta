using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Threading;
using Sirenix.OdinInspector;
using System.Linq;
using DG.Tweening.Core.Easing;

[CreateAssetMenu(fileName = "SaveManager", menuName = "Corrupta/Managers/Save Manager...")]
public class SaveManager : ManageableScriptableObject
{
	public static SaveManager Instance
	{
		get
		{
			return GameManager.Instance.saveManager;
        }
    }

	#region Member Variables

	private List<ISaveable> saveables;
	private JSONNode loadedSave;

	[SerializeField] SaveData[] saveDatas;
	[SerializeField] string saveProfile = "default";
	[SerializeField] bool saveOnQuit = true;

	#endregion

	#region Properties

	/// <summary>
	/// Path to the save file on the device
	/// </summary>

	public string GetSaveFilePath(bool isGlobal)
	{
		if (isGlobal)
		{
			return Application.persistentDataPath + "/Global";
		}
		else
		{
			return Application.persistentDataPath + "/" + saveProfile;
		}
	}

	/// <summary>
	/// List of registered saveables
	/// </summary>
	private List<ISaveable> Saveables
	{
		get
		{
			if (saveables == null)
			{
				saveables = new List<ISaveable>();
			}

			return saveables;
		}
	}
	#endregion


	#region Public Methods

	/// <summary>
	/// Registers a saveable to be saved
	/// </summary>
	public void Register(ISaveable saveable)
	{
		Saveables.Add(saveable);
	}

	public JSONNode LoadSave(ISaveable saveable)
	{
		// Check if the save file has been loaded and if not try and load it
		if (loadedSave == null && !LoadSave(saveable, out loadedSave))
		{
			return null;
		}

		// Check if the loaded save file has the given save id
		if (!loadedSave.AsObject.HasKey(saveable.SaveId))
		{
			return null;
		}

		// Return the JSONNode for the save id
		return loadedSave[saveable.SaveId];
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Saves all registered saveables to the save file
	/// </summary>
	public void Save(Action onSaveComplete = null)
	{

		if (saveables != null)
		{
			foreach (SaveData saveData in saveDatas)
			{
				Dictionary<string, object> saveJson = new Dictionary<string, object>();
				string saveString = saveData.saveDataType.ToString();
				for (int i = 0; i < saveables.Count; i++)
				{
					if (saveables[i].SaveDataType != saveData.saveDataType)
					{
						continue;
					}
					//saveJson.Add(saveables[i].SaveId, saveables[i].Save());
					if (saveJson.ContainsKey(saveables[i].SaveId))
					{
						saveJson[saveables[i].SaveId] = saveables[i].Save();
					}
					else
					{
						saveJson.Add(saveables[i].SaveId, saveables[i].Save());
					}
					System.IO.Directory.CreateDirectory($"{GetSaveFilePath(saveData.isGlobalProfile)}");
					System.IO.File.WriteAllText($"{GetSaveFilePath(saveData.isGlobalProfile)}/{saveString}.json", JsonConvert.SerializeObject(saveJson));
				}
			}
		}

		onSaveComplete?.Invoke();
	}

	/// <summary>
	/// Tries to load the save file
	/// </summary>
	private bool LoadSave(ISaveable saveable, out JSONNode json)
	{
		SaveData saveData = Array.Find(saveDatas, data => data.saveDataType == saveable.SaveDataType);
		string saveString = saveData.saveDataType.ToString();
		string filePath = $"{GetSaveFilePath(saveData.isGlobalProfile)}/{saveString}.json";
		if (!System.IO.File.Exists(filePath))
		{
			json = null;
			return false;
		}
		string fileContents = System.IO.File.ReadAllText(filePath);
		json = JSON.Parse(fileContents);
		return true;
	}
#if UNITY_EDITOR
	[MenuItem("Corrupta/Save Manager/Clear All Saves")]
	[Button]
    private static void ClearAllSaves()
	{
		foreach (SaveData saveData in Instance.saveDatas)
		{
			string saveString = saveData.saveDataType.ToString();
			string filePath = $"{Instance.GetSaveFilePath(saveData.isGlobalProfile)}/{saveString}.json";
			if (System.IO.File.Exists(filePath))
			{
				System.IO.File.Delete(filePath);
			}
		}
    }
	[MenuItem("Corrupta/Save Manager/Save Now")]
    [Button]
    private static void SaveNow()
	{
		Instance.Save();
	}
	[MenuItem("Corrupta/Save Manager/Show Save Folder")]
    [Button]
    private static void ShowSaveFolder()
	{
		EditorUtility.RevealInFinder(Application.persistentDataPath);
    }
#endif
}
#endregion
public enum SaveDataType
	{
		MetaData,
		Settings,
		WorldProgression,
		LevelProgression,
		Tutorial
	}
public class SaveData
{
	public SaveDataType saveDataType;
	[Tooltip("If true, this save will be a global save that is not tied to a specific profile (i.e. graphics settings)")]
    public bool isGlobalProfile = false;
}