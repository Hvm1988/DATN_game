using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
	public abstract string getDetailSkill(int level);

	public bool isUnlock()
	{
		return DataHolder.Instance.playerData.level >= this.levelReq;
	}

	public int getGoldNextLevel(int curLevel)
	{
		if (curLevel == 10)
		{
			return -1;
		}
		string[] array = this.goldUpgrades.Split(new char[]
		{
			','
		});
		return int.Parse(array[curLevel]);
	}

	public int getRubyNextLevel(int curLevel)
	{
		if (curLevel == 10)
		{
			return -1;
		}
		string[] array = this.rubyUpgrades.Split(new char[]
		{
			','
		});
		return int.Parse(array[curLevel]);
	}

	public string name;

	public string code;

	public Sprite icon;

	public string des;

	public int goldUp;

	public int rubyUp;

	public string detailSkill;

	public int levelReq;

	public string skillReq;

	public string[] inGroupSkills;

	public List<string> nextSkill;

	public string goldUpgrades;

	public string rubyUpgrades;
}
