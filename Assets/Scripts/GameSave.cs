using System;
using UnityEngine;

public class GameSave
{
	public static void setPrefsInt(string key, int value)
	{
		PlayerPrefs.SetInt(key, value);
	}

	public static int getPrefsInt(string key)
	{
		return PlayerPrefs.GetInt(key);
	}

	public static void setPrefsStr(string key, string str)
	{
		PlayerPrefs.SetString(key, str);
	}

	public static string getPrefsStr(string key)
	{
		return PlayerPrefs.GetString(key);
	}

	public static void resetItemEat()
	{
		for (int i = 0; i < GameSave.itemsEat.Length; i++)
		{
			GameSave.itemsEat[i] = 0;
		}
	}

	public static int damageBase;

	public static int defendBase;

	public static int HPBase;

	public static int damageHero;

	public static int defendHero;

	public static int HPHero;

	public static int level;

	public static int EXP;

	public static int damageAtt = 80;

	public static int damageSkill2 = 100;

	public static int damageSkill3 = 100;

	public static int damagePet = 80;

	public static int damageRotation = 80;

	public static int minGetCoin = 1;

	public static int maxGetCoin = 10;

	public static int getCoin;

	public static int getExp;

	public static int getRuby;

	public static bool gamePasue;

	public static bool endGame;

	public static int heroOneHit = 15;

	public static int heroGetOneHit = 10;

	public static int percentInstanceMaterial = 100;

	public static int[] itemsEat = new int[12];

	public static string levelName;

	public static bool isNextGame;
}
