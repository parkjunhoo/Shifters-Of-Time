using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<string, Data.StageInfo> StageInfoDict { get; private set; } = new Dictionary< string , StageInfo>();

    public void Init()
    {
        StageInfoDict = LoadJson<Data.StageInfoData, string, Data.StageInfo>("Stages/StageInfos").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
		TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
	}
}
