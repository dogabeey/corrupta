using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lionsfall
{
	public interface ISaveable
	{
		string SaveId { get; }
        SaveDataType SaveDataType { get; }
        Dictionary<string, object> Save();
	}
}
