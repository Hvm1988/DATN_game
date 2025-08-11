using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionDefine", menuName = "Game Data/Mission Define")]
public class MissionDefine : ScriptableObject
{
	public Mission getMission(string code)
	{
		foreach (Mission mission in this.missions)
		{
			if (mission.code.Equals(code))
			{
				return mission;
			}
		}
		return null;
	}

	public Mission[] missions;
}
