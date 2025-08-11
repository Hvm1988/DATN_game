using System;
using LitJson;
using UnityEngine;

public abstract class DataModel : ScriptableObject
{
	public virtual void save()
	{
		PlayerPrefs.SetString(base.GetType().ToString(), JsonUtility.ToJson(this));
	}

	public abstract void initFirstTime();

	public abstract void loadFromFireBase();

	public void loadFromJson(string jsonData)
	{
		JsonUtility.FromJsonOverwrite(jsonData, this);
		this.save();
	}

	public void loadFromJson(string jsonData, string key)
	{
		string text = JsonMapper.ToObject(jsonData)[key].ToJson();
		UnityEngine.Debug.Log(key + "===" + text);
		this.loadFromJson(text);
	}

	public void loadFromJson(string jsonData, string[] keys)
	{
		string text = jsonData;
		JsonData jsonData2 = new JsonData();
		for (int i = 0; i < keys.Length; i++)
		{
			jsonData2 = JsonMapper.ToObject(text)[keys[i]].ToJson();
			text = jsonData2.ToJson();
		}
		UnityEngine.Debug.Log(keys[keys.Length - 1] + "===" + text);
		this.loadFromJson(text);
	}

	public virtual void loadFromPref()
	{
		if (!PlayerPrefs.HasKey(base.GetType().ToString()) || PlayerPrefs.GetString(base.GetType().ToString()).Equals(string.Empty))
		{
			this.initFirstTime();
			this.save();
		}
		else
		{
			JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(base.GetType().ToString()), this);
		}
	}
}
