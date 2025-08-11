using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementDefine", menuName = "Game Data/Achievement Define")]
public class AchievementDefine : ScriptableObject
{
	public Achievement getAchievement(string code)
	{
		foreach (Achievement achievement in this.achievements)
		{
			if (achievement.code.Equals(code))
			{
				return achievement;
			}
		}
		return null;
	}

	public Achievement[] achievements;
}
