using System;
using UnityEngine;

[Serializable]
public class MainItemInven : ItemInven
{
	public string getLevelString()
	{
		return "+" + this.level;
	}

	public void upgradeGold(Action callBack = null)
	{
		if (this.canUpgradeWithGold())
		{
			int num = UnityEngine.Random.Range(0, 101);
			DataHolder.Instance.playerData.addGold(-this.getGoldCostUpgadeNextLevel());
			if (num > this.getPercentSuccess())
			{
				throw new Exception("FAIL");
			}
			this.level++;
			DataHolder.Instance.inventory.save();
			DataHolder.Instance.playerData.reCalStat();
			SoundManager.Instance.playAudio("PayGold");
			if (this.code.Contains("ARMOR"))
			{
				DataHolder.Instance.missionData.addDone(null, "UP-ARMOR", 1);
				DataHolder.Instance.achievementData.addDone(null, "UP-ARMOR", 1);
			}
			if (this.code.Contains("SHOE"))
			{
				DataHolder.Instance.missionData.addDone(null, "UP-SHOE", 1);
				DataHolder.Instance.achievementData.addDone(null, "UP-SHOE", 1);
			}
			if (this.code.Contains("GLOVER"))
			{
				DataHolder.Instance.missionData.addDone(null, "UP-GLOVER", 1);
				DataHolder.Instance.achievementData.addDone(null, "UP-GLOVER", 1);
			}
			if (this.code.Contains("PANTS"))
			{
				DataHolder.Instance.missionData.addDone(null, "UP-PANTS", 1);
				DataHolder.Instance.achievementData.addDone(null, "UP-PANTS", 1);
			}
		}
	}

	public void upgradeRuby(Action callBack = null)
	{
		if (this.canUpgradeWithRuby())
		{
			DataHolder.Instance.playerData.addRuby(-this.getRubyCostUpgadeNextLevel());
			int num = UnityEngine.Random.Range(0, 101);
			if (num > this.getPercentSuccess())
			{
				throw new Exception("FAIL");
			}
			this.level++;
			DataHolder.Instance.inventory.save();
			DataHolder.Instance.playerData.reCalStat();
			SoundManager.Instance.playAudio("PayRuby");
			if (this.code.Contains("ARMOR"))
			{
				DataHolder.Instance.missionData.addDone(null, "UP-ARMOR", 1);
				DataHolder.Instance.achievementData.addDone(null, "UP-ARMOR", 1);
			}
			if (this.code.Contains("SHOE"))
			{
				DataHolder.Instance.missionData.addDone(null, "UP-SHOE", 1);
				DataHolder.Instance.achievementData.addDone(null, "UP-SHOE", 1);
			}
			if (this.code.Contains("GLOVER"))
			{
				DataHolder.Instance.missionData.addDone(null, "UP-GLOVER", 1);
				DataHolder.Instance.achievementData.addDone(null, "UP-GLOVER", 1);
			}
			if (this.code.Contains("PANTS"))
			{
				DataHolder.Instance.missionData.addDone(null, "UP-PANTS", 1);
				DataHolder.Instance.achievementData.addDone(null, "UP-PANTS", 1);
			}
		}
	}

	public bool canUpgradeWithGold()
	{
		if (this.level == 10)
		{
			return false;
		}
		int goldCostUpgadeNextLevel = this.getGoldCostUpgadeNextLevel();
		return goldCostUpgadeNextLevel != -1 && DataHolder.Instance.playerData.gold >= goldCostUpgadeNextLevel;
	}

	public bool canUpgradeWithRuby()
	{
		return this.level != 10 && DataHolder.Instance.playerData.ruby >= this.getRubyCostUpgadeNextLevel();
	}

	public int getGoldCostUpgadeNextLevel()
	{
		MainItem mainByCode = DataHolder.Instance.mainItemsDefine.getMainByCode(this.code);
		return DataHolder.Instance.nItemDefine.costToUpgrade[(int)mainByCode.color].gold[this.level];
	}

	public int getRubyCostUpgadeNextLevel()
	{
		MainItem mainByCode = DataHolder.Instance.mainItemsDefine.getMainByCode(this.code);
		return DataHolder.Instance.nItemDefine.costToUpgrade[(int)mainByCode.color].ruby[this.level];
	}

	private int getPercentSuccess()
	{
		MainItem mainByCode = DataHolder.Instance.mainItemsDefine.getMainByCode(this.code);
		return DataHolder.Instance.nItemDefine.costToUpgrade[(int)mainByCode.color].percent[this.level];
	}

	public int level;
}
