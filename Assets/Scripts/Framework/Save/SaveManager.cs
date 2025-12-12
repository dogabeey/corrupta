using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Threading;
using Sirenix.OdinInspector;
using Lionsfall.SimpleJSON;

namespace Lionsfall
{
	[CreateAssetMenu(fileName = "SaveManager", menuName = "Scriptable Objects/Managers/Save Manager...")]
	public class SaveManager : SerializedScriptableObject
	{
        public static string DATABASE_URL = "https://wordstones-users.firebaseio.com/";
		public static string userId;
		public static SaveManager Instance => GameManager.Instance.saveManager;

		#region Member Variables

		private List<ISaveable>	saveables;
		private JSONNode	loadedSave;

		[SerializeField] bool saveOnQuit = true;
		[SerializeField] Dictionary<SaveDataType, string> saveDataStrings = new Dictionary<SaveDataType, string>();

        #endregion

        #region Properties

        /// <summary>
        /// Path to the save file on the device
        /// </summary>
        public static string SaveFilePath { get { return Application.persistentDataPath ; } }

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

        #region Unity Methods

        [Button]
        public void ClearSaves(SaveDataType saveDataType)
        {
			// For each selected save data type, clear the registered saveables of that type
			List<SaveDataType> saveDataTypes = saveDataType.GetSelectedFlags();
			foreach (SaveDataType sdt in saveDataTypes)
			{
				// Remove the save file
				if (System.IO.Directory.Exists($"{SaveFilePath}/{saveDataStrings[sdt]}"))
				{
					System.IO.Directory.Delete($"{SaveFilePath}/{saveDataStrings[sdt]}");
					Debug.Log("Save file deleted");
				}
				else
				{
					Debug.Log("No save file found");
				}
			}
        }

        public void Start()
		{
			Debug.Log("Save file path: " + $"{SaveFilePath}");
        }
		public void Update()
		{

		}
		public void OnManagerDestroy()
		{
			if(saveOnQuit)
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

            Dictionary<string, object> saveJson = new Dictionary<string, object>();
			if(saveables != null)
			{
				for (int i = 0; i < saveables.Count; i++)
                {
					SaveDataType saveDataType = saveables[i].SaveDataType;
                    string saveString = saveDataStrings[saveDataType];
                    //saveJson.Add(saveables[i].SaveId, saveables[i].Save());
                    if (saveJson.ContainsKey(saveables[i].SaveId))
					{
						saveJson[saveables[i].SaveId] = saveables[i].Save();
					}else
					{
						saveJson.Add(saveables[i].SaveId, saveables[i].Save());
                    }
					System.IO.Directory.CreateDirectory($"{SaveFilePath}/{saveString}");
					System.IO.File.WriteAllText($"{SaveFilePath}/{saveString}/{saveables[i].SaveId}.json", JsonConvert.SerializeObject(saveJson));
                }
				
			}

            onSaveComplete?.Invoke();
		}

		/// <summary>
		/// Tries to load the save file
		/// </summary>
		private bool LoadSave(ISaveable saveable, out JSONNode json)
        {
            string saveString = saveDataStrings[saveable.SaveDataType];

            json = null;

			if (!System.IO.File.Exists($"{SaveFilePath}/{saveString}/{saveable.SaveId}.json"))
			{
				Debug.Log($"<color=yellow>No save file found at: {SaveFilePath}/{saveString}/{saveable.SaveId}.json</color>");
				return false;
			}

			Debug.Log($"<color=green>Loading save file: {SaveFilePath}/{saveString}/{saveable.SaveId}.json</color>");
            json = JSON.Parse(System.IO.File.ReadAllText($"{SaveFilePath}/{saveString}/{saveable.SaveId}.json"));

			

			return json != null;
		}


		#endregion
	}

	[Flags]
    public enum SaveDataType
    {
        MetaData = 1,
        Settings = 2,
        WorldProgression = 4,
        LevelProgression = 8,
        Tutorial = 16,
    }
}
