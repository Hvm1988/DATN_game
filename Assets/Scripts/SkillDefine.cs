using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillDefine", menuName = "Game Data/Skill Data")]
public class SkillDefine : ScriptableObject
{
	public Skill getSkill(string code)
	{
		foreach (Skill skill in this.skillPassives)
		{
			if (skill.code.Equals(code))
			{
				return skill;
			}
		}
		foreach (Skill skill2 in this.skillActives)
		{
			if (skill2.code.Equals(code))
			{
				return skill2;
			}
		}
		return null;
	}

	public List<SkillPassive> skillPassives;

	public List<SkillActive> skillActives;
}
