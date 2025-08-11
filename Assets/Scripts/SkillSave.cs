using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillSave
{
	public void calCondition()
	{
		Skill skill = DataHolder.Instance.skillDefine.getSkill(this.code);
		if (skill.levelReq <= DataHolder.Instance.playerData.level)
		{
			this.conditions[1] = 1;
		}
	}

	public void upgrade(int type)
	{
		if (TutorialManager.isTutorialing)
		{
			return;
		}
		try
		{
			if (this.canUpgrade())
			{
				Skill skill = DataHolder.Instance.skillDefine.getSkill(this.code);
				if (type == 0)
				{
					DataHolder.Instance.playerData.addGold(-skill.getGoldNextLevel(this.level));
					SoundManager.Instance.playAudio("PayGold");
				}
				else
				{
					DataHolder.Instance.playerData.addRuby(-skill.getRubyNextLevel(this.level));
					SoundManager.Instance.playAudio("PayRuby");
				}
				if (this.level != 10)
				{
					this.level++;
					if (this.level == 1)
					{
						this.unlockNextSkill();
						this.lockSkillInGroup(skill);
					}
					DataHolder.Instance.skillData.save();
					DataHolder.Instance.playerData.reCalStat();
					DataHolder.Instance.missionData.addDone(null, "UP-SKILL", 1);
					SkillNotifer.Instance.refresh();
				}
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			if (ex.Message.Equals("NOT_ENOUGHT_GOLD") || ex.Message.Equals("NOT_ENOUGHT_RUBY"))
			{
				throw ex;
			}
		}
	}

	public bool canUpgrade()
	{
		return this.conditions[0] == 1 && this.conditions[1] == 1 && this.conditions[2] == 1;
	}

	public bool canUpgradeGold()
	{
		if (!this.canUpgrade())
		{
			return false;
		}
		if (this.level == 10)
		{
			return false;
		}
		Skill skill = DataHolder.Instance.skillDefine.getSkill(this.code);
		return DataHolder.Instance.playerData.gold >= skill.getGoldNextLevel(this.level);
	}

	public bool canUpgradeRuby()
	{
		if (!this.canUpgrade())
		{
			return false;
		}
		if (this.level == 10)
		{
			return false;
		}
		Skill skill = DataHolder.Instance.skillDefine.getSkill(this.code);
		return DataHolder.Instance.playerData.ruby >= skill.getRubyNextLevel(this.level);
	}

	public void lockSkillInGroup(Skill skill)
	{
		if (skill.inGroupSkills.Length > 0)
		{
			for (int i = 0; i < skill.inGroupSkills.Length; i++)
			{
				DataHolder.Instance.skillData.getASkill(skill.inGroupSkills[i]).conditions[2] = 0;
			}
		}
	}

	public void unlockNextSkill()
	{
		if (this.level == 0)
		{
			return;
		}
		Skill skill = DataHolder.Instance.skillDefine.getSkill(this.code);
		List<string> nextSkill = skill.nextSkill;
		foreach (string text in nextSkill)
		{
			SkillSave askill = DataHolder.Instance.skillData.getASkill(text);
			askill.conditions[0] = 1;
		}
	}

	public string getLevelString()
	{
		return "Lv." + this.level.ToString() + "/10";
	}

	public string code;

	public int level;

	public int[] conditions = new int[]
	{
		0,
		0,
		1
	};
}
