using System;
using UnityEngine;

public class HeroDefine
{
	public static string getUserData(string heroName)
	{
		if (PlayerPrefs.GetString(heroName).Equals(string.Empty))
		{
			PlayerPrefs.SetString(heroName, HeroDefine.formatData);
			return HeroDefine.formatData;
		}
		return PlayerPrefs.GetString(heroName);
	}

	private static string formatData = "{\"exp\":0, \n\t\"item\":[0, 0, 0, 0, 0],\n\t\"levelAttack\": 0,\n\t\"levelDefend\": 0,\n\t\"idSkill2\": 0,\n\t\"levelSkill2\": 0,\n\t\"levelSkill3\": 0, \n\t\"levelSupport\": 0}";
}
