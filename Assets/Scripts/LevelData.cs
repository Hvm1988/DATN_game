using System;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelData")]
public class LevelData : DataModel
{
	public override void initFirstTime()
	{
	}

	public override void loadFromFireBase()
	{
		
	}

	public static string getLevel(int level)
	{
		return string.Empty;
	}

	public override void loadFromPref()
	{
		if (!PlayerPrefs.HasKey(base.GetType().ToString() + this.code) || PlayerPrefs.GetString(base.GetType().ToString() + this.code).Equals(string.Empty))
		{
			this.initFirstTime();
			this.save();
		}
		else
		{
			JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(base.GetType().ToString() + this.code), this);
		}
	}

	public override void save()
	{
		PlayerPrefs.SetString(base.GetType().ToString() + this.code, JsonUtility.ToJson(this));
	}

	public string code;

	public string[] turn1;

	public string[] turn2;

	public string[] turn3;

	public string[] boss;

	public int[] materialTurn1;

	public int[] materialTurn2;

	public int[] materialTurn3;

	public int[] scroll;

	public string nameLevel;

	public string description;

	public int exp;

	public int gold;
}
