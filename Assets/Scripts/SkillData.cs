using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSave", menuName = "Game Data/Skill Save")]
public class SkillData : DataModel
{
	public override void initFirstTime()
	{
		for (int i = 0; i < this.skillSaves.Count; i++)
		{
			if (this.skillSaves[i].code.Equals("NORMAL-ATK"))
			{
				this.skillSaves[i].level = 1;
				this.skillSaves[i].conditions = new int[]
				{
					1,
					1,
					1
				};
			}
			else if (this.skillSaves[i].code.Equals("TORNADO"))
			{
				this.skillSaves[i].level = 0;
				this.skillSaves[i].conditions = new int[]
				{
					1,
					0,
					1
				};
			}
			else
			{
				this.skillSaves[i].level = 0;
				this.skillSaves[i].conditions = new int[]
				{
					0,
					0,
					1
				};
			}
		}
	}

	public override void loadFromFireBase()
	{
		throw new NotImplementedException();
	}

	public SkillSave getASkill(string code)
	{
		foreach (SkillSave skillSave in this.skillSaves)
		{
			if (skillSave.code.Equals(code))
			{
				return skillSave;
			}
		}
		return null;
	}

	public bool isLearedSkill(string code)
	{
		foreach (SkillSave skillSave in this.skillSaves)
		{
			if (skillSave.code.Equals(code))
			{
				return skillSave.level > 0;
			}
		}
		return false;
	}

	public int getDamageAskill(string code)
	{
		SkillSave askill = this.getASkill(code);
		Skill skill = DataHolder.Instance.skillDefine.getSkill(code);
		if (askill.level == 0)
		{
			return 0;
		}
		return ((SkillActive)skill).damages[askill.level];
	}

	public int getEffectASkill(string code)
	{
		SkillSave askill = this.getASkill(code);
		Skill skill = DataHolder.Instance.skillDefine.getSkill(code);
		if (askill.level == 0)
		{
			return 0;
		}
		return ((SkillPassive)skill).effectValue[askill.level];
	}

	public List<SkillSave> skillSaves;
}
