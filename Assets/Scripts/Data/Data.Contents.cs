using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//data
namespace Data
{ 
#region Stage
	[Serializable]
	public class StageInfo
	{
		public string code;
		public string name;
		public string subText;
	}

	public class StageInfoData : ILoader<string, StageInfo>
	{
		public List<StageInfo> stageInfos = new List<StageInfo>();

		public Dictionary<string, StageInfo> MakeDict()
		{
			Dictionary<string, StageInfo> dict = new Dictionary<string, StageInfo>();
			foreach (StageInfo info in stageInfos)
				dict.Add(info.code, info);
			return dict;
		}
	}
	#endregion
}