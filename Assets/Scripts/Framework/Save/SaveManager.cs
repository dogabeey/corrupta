using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Threading;
using Sirenix.OdinInspector;
using Lionsfall.SimpleJSON;

[CreateAssetMenu(fileName = "SaveManager", menuName = "Corrupta/Managers/Save Manager...")]
public class SaveManager : ManageableScriptableObject
{
	public static SaveManager Instance => GameManager.Instance.saveManager;

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

	public override void Start()
	{
	}

	public override void Update()
	{


	}
	public override void OnManagerDestroy()
	{
		if (saveOnQuit)
		{
			Save();
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
				}
				System.IO.Directory.CreateDirectory($"{GetSaveFilePath(saveData.isGlobalProfile)}");
                System.IO.File.WriteAllText($"{GetSaveFilePath(saveData.isGlobalProfile)}/{saveString}.json", JsonConvert.SerializeObject(saveJson));
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
    #endregion

}
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