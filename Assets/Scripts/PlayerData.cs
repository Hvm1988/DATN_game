using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : DataModel
{
	public string getExpString()
	{
		return CustomInt.toString(this.exp) + "/" + CustomInt.toString(this.getNextLevelExp());
	}

	public void increaPlayCount()
	{
		this.playCount++;
		this.save();
	}

	public bool canShowRate()
	{
		return this.playCount != 0 && (this.playCount == 2 || this.playCount % 5 == 0);
	}

	public bool canRewardGrownUp(int id)
{
    return rewardGrownGift != null && id >= 0 && id < rewardGrownGift.Length && rewardGrownGift[id] == 0;
}

	public void refreshIapStackGift()
	{
		for (int i = 0; i < this.rewardIAPStackGift.Length; i++)
		{
			if (i < this.rewardIAPStackGift.Length - 1 && this.rewardIAPStackGift[i] == 1 && DataHolder.Instance.playerDefine.iapGiftPoint[i + 1] > this.totalIAPPurchared)
			{
				this.rewardIAPStackGift[i] = 0;
				return;
			}
			if (i == this.rewardIAPStackGift.Length - 1 && this.rewardIAPStackGift[i] == 1)
			{
				this.rewardIAPStackGift[i] = 0;
				return;
			}
		}
		this.save();
	}

    public void rewardGrownUp(int id)
    {
        if (rewardGrownGift != null && id >= 0 && id < rewardGrownGift.Length)
        {
            rewardGrownGift[id] = 1;
            GrownGiftNotifier.Instance?.setUI();
            EventNotifier.Instance?.setUI();
            save();
        }
    }

    public void refreshGrownGift()
	{
		for (int i = 0; i < DataHolder.Instance.grownGiftDefine.growGifts.Length; i++)
		{
			if (this.level >= DataHolder.Instance.grownGiftDefine.growGifts[i].level && this.rewardGrownGift[i] == -1)
			{
				this.rewardGrownGift[i] = 0;
				if (GrownGiftNotifier.Instance != null)
				{
					GrownGiftNotifier.Instance.setUI();
				}
				if (EventNotifier.Instance != null)
				{
					EventNotifier.Instance.setUI();
				}
			}
		}
		this.save();
	}

    public int getFirstLockRewardIAP()
    {
        if (rewardIAPStackGift == null) return -1;
        for (int i = 0; i < rewardIAPStackGift.Length; i++)
            if (rewardIAPStackGift[i] == -1) return i;
        return -1;
    }

    public int getFirstCanRewardIAP()
    {
        if (rewardIAPStackGift == null) return -1;
        for (int i = 0; i < rewardIAPStackGift.Length; i++)
            if (rewardIAPStackGift[i] == 0) return i;
        return -1;
    }

    public int getCurrentVip()
    {
        var pts = DataHolder.Instance.playerDefine.iapGiftPoint;
        if (pts == null || pts.Length == 0) return -1;
        for (int i = pts.Length - 1; i >= 0; i--)
            if (pts[i] <= totalIAPPurchared) return i;
        return -1;
    }

    public bool haveRewardIAPStack()
    {
        return curVip >= 0 && rewardIAPStackGift != null
            && curVip < rewardIAPStackGift.Length
            && rewardIAPStackGift[curVip] == 0;
    }

    public void addTotalIAPPurchared(float value)
	{
		UnityEngine.Debug.Log("addTotalIAPPurchared");
		this.totalIAPPurchared += value;
		this.curVip = this.getCurrentVip();
		if (this.curVip != -1 && this.rewardIAPStackGift[this.curVip] == -1)
		{
			this.rewardIAPStackGift[this.curVip] = 0;
			if (VipGiftNotifier.Instance != null)
			{
				VipGiftNotifier.Instance.setUI();
			}
			if (EventNotifier.Instance != null)
			{
				EventNotifier.Instance.setUI();
			}
		}
		this.save();
	}

    public void setRewardIAPStackGift(int index, int value)
    {
        if (rewardIAPStackGift != null && index >= 0 && index < rewardIAPStackGift.Length)
        {
            rewardIAPStackGift[index] = value;
            save();
        }
    }

    public void unlockNextGiftDay()
	{
		for (int i = 0; i < this.dailyGift.Length; i++)
		{
			if (this.dailyGift[i] == -1)
			{
				this.dailyGift[i] = 0;
				this.save();
				break;
			}
		}
	}

    public void rewardDailyGift(int id)
    {
        if (dailyGift != null && id >= 0 && id < dailyGift.Length)
        {
            dailyGift[id] = 1;
            save();
        }
    }

    public int getFirstLockGift()
	{
		for (int i = 0; i < this.dailyGift.Length; i++)
		{
			if (this.dailyGift[i] == 0 || this.dailyGift[i] == -1)
			{
				return i;
			}
		}
		return 0;
	}

	public int getCanRewarDaily()
	{
		for (int i = 0; i < this.dailyGift.Length; i++)
		{
			if (this.dailyGift[i] == 0)
			{
				return i;
			}
		}
		return -1;
	}

	public string getGoldString()
	{
		return CustomInt.toString(this.gold);
	}

	public string getRubyString()
	{
		return CustomInt.toString(this.ruby);
	}

	public void addTotalKillMons(int value)
	{
		this.totalKillMons += value;
		this.save();
	}

	public void addTotalPassLevel(int value)
	{
		this.totalLevelPassed += value;
		this.save();
	}

	public void setBestCombo(int value)
	{
		if (value > this.bestCombo)
		{
			this.bestCombo = value;
		}
		this.save();
	}

	public override void initFirstTime()
	{
		this.name = string.Empty;
		this.armorSlot = "ARMOR-SOKU-WHITE|1";
		this.gloverSlot = "GLOVER-SOKU-WHITE|6";
		this.ringSlot = "RING-SOKU-WHITE|2";
		this.amuletSlot = "AMULET-SOKU-WHITE|3";
		this.pantsSlot = "PANTS-SOKU-WHITE|5";
		this.shoeSlot = "SHOE-SOKU-WHITE|4";
		this.ruby = 100;
		this.gold = 1000;
		this.energy = 100;
		this.level = 0;
		this.skip = 5;
		this.rewardGrownGift = new int[]
		{
			-1,
			-1,
			-1
		};
		this.refreshGrownGift();
		this.hp = 0;
		this.exp = 0;
		this.def = 0;
		this.speed = 0;
		this.dailyGift = new int[]
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		};
		this.curVip = -1;
		this.rewardIAPStackGift = new int[]
		{
			-1,
			-1,
			-1,
			-1,
			-1
		};
		this.totalIAPPurchared = 0f;
		this.addTotalIAPPurchared(0f);
		this.playCount = 0;
		this.clickedRate = false;
		this.firstDate = DatePassHelper.getNowString(DatePassHelper.DateFormat.ddMMyyyy);
	}

	public override void loadFromFireBase()
	{
		throw new NotImplementedException();
	}

	public void reCalStat()
	{
		this.calHP();
		this.calAtk();
		this.calDEF();
		if (CounterFather.Instance != null)
		{
			CounterFather.Instance.changePlayerStat();
		}
	}

    public int getNextLevelExp()
    {
        var def = DataHolder.Instance.playerDefine;
        int maxLv = def != null ? def.MaxLevel : 0;

        // max hoặc không có bảng -> không cần EXP tiếp
        if (maxLv <= 0 || level >= maxLv) return 0;

        // level+1 an toàn vì PlayerDefine đã clamp
        return def.getEXP(level + 1);
    }
    public float getExpFloat()
    {
        int next = getNextLevelExp();
        if (next <= 0) return 1f;                  // đã max
        return Mathf.Clamp01(exp / (float)next);   // tránh chia 0
    }


    public int getExpPercent()
    {
        return Mathf.RoundToInt(getExpFloat() * 100f);
    }


    public void addExp(int value)
    {
        if (value <= 0) return;

        var def = DataHolder.Instance.playerDefine;
        int maxLv = def != null ? def.MaxLevel : 0;
        if (maxLv <= 0) return;

        // gộp exp, lên cấp nhiều lần nếu đủ
        exp += value;

        while (level < maxLv)
        {
            int need = getNextLevelExp();     // 0 nếu max
            if (need <= 0) { exp = 0; break; }
            if (exp < need) break;

            exp -= need;
            level++;

            DataHolder.Instance.achievementData.addDone(null, "REACH-LEVEL", 1);
            refreshGrownGift();
            reCalStat();
        }

        if (CounterFather.Instance != null)
            CounterFather.Instance.changePlayerStat();
    }

    public void calHP()
	{
		PlayerDefine playerDefine = DataHolder.Instance.playerDefine;
		int num = playerDefine.getHP(this.level);
		MainItemType[] array = new MainItemType[]
		{
			MainItemType.ARMOR,
			MainItemType.AMULET,
			MainItemType.RING,
			MainItemType.PANTS,
			MainItemType.SHOE,
			MainItemType.GLOVER
		};
		foreach (MainItemType type in array)
		{
			MainItemInven itemByType = this.getItemByType(type);
			if (itemByType != null)
			{
				MainItem mainByCode = DataHolder.Instance.mainItemsDefine.getMainByCode(itemByType.code);
				foreach (OptionItem optionItem in mainByCode.optionItem)
				{
					if (optionItem.getTarget() == IncreaTarget.HP)
					{
						if (optionItem.getType() == TypeBonus.ADD)
						{
							num += optionItem.getValue(itemByType.level);
						}
						if (optionItem.getType() == TypeBonus.PERCENT)
						{
							int num2 = num * optionItem.getValue(itemByType.level) / 100;
							num += num2;
						}
					}
				}
			}
		}
		SkillSave askill = DataHolder.Instance.skillData.getASkill("ADD-HP");
		if (askill.level >= 1)
		{
			SkillPassive skillPassive = (SkillPassive)DataHolder.Instance.skillDefine.getSkill("ADD-HP");
			int num3 = skillPassive.effectValue[askill.level];
			int num4 = num * num3 / 100;
			num += num4;
		}
		this.hp = num;
		this.save();
	}

	public void calDEF()
	{
		PlayerDefine playerDefine = DataHolder.Instance.playerDefine;
		int num = playerDefine.getDEF(this.level);
		MainItemType[] array = new MainItemType[]
		{
			MainItemType.ARMOR,
			MainItemType.AMULET,
			MainItemType.RING,
			MainItemType.PANTS,
			MainItemType.SHOE,
			MainItemType.GLOVER
		};
		foreach (MainItemType type in array)
		{
			MainItemInven itemByType = this.getItemByType(type);
			if (itemByType != null)
			{
				MainItem mainByCode = DataHolder.Instance.mainItemsDefine.getMainByCode(itemByType.code);
				foreach (OptionItem optionItem in mainByCode.optionItem)
				{
					if (optionItem.getTarget() == IncreaTarget.DEF)
					{
						if (optionItem.getType() == TypeBonus.ADD)
						{
							num += optionItem.getValue(itemByType.level);
						}
						if (optionItem.getType() == TypeBonus.PERCENT)
						{
							int num2 = num * optionItem.getValue(itemByType.level) / 100;
							num += num2;
						}
					}
				}
			}
		}
		SkillSave askill = DataHolder.Instance.skillData.getASkill("ADD-DEF");
		if (askill.level >= 1)
		{
			SkillPassive skillPassive = (SkillPassive)DataHolder.Instance.skillDefine.getSkill("ADD-DEF");
			int num3 = skillPassive.effectValue[askill.level];
			int num4 = num * num3 / 100;
			num += num4;
		}
		this.def = num;
		this.save();
	}

	public int getDamageBonus()
	{
		PlayerDefine playerDefine = DataHolder.Instance.playerDefine;
		int num = playerDefine.getATK(this.level);
		int num2 = 0;
		MainItemType[] array = new MainItemType[]
		{
			MainItemType.ARMOR,
			MainItemType.AMULET,
			MainItemType.RING,
			MainItemType.PANTS,
			MainItemType.SHOE,
			MainItemType.GLOVER
		};
		foreach (MainItemType type in array)
		{
			MainItemInven itemByType = this.getItemByType(type);
			if (itemByType != null)
			{
				MainItem mainByCode = DataHolder.Instance.mainItemsDefine.getMainByCode(itemByType.code);
				foreach (OptionItem optionItem in mainByCode.optionItem)
				{
					if (optionItem.getTarget() == IncreaTarget.ATK)
					{
						if (optionItem.getType() == TypeBonus.ADD)
						{
							num2 += optionItem.getValue(itemByType.level);
						}
						if (optionItem.getType() == TypeBonus.PERCENT)
						{
							int num3 = num * optionItem.getValue(itemByType.level) / 100;
							num2 += num3;
						}
					}
				}
			}
		}
		return num2;
	}

	public void calAtk()
	{
		PlayerDefine playerDefine = DataHolder.Instance.playerDefine;
		int num = playerDefine.getATK(this.level);
		MainItemType[] array = new MainItemType[]
		{
			MainItemType.ARMOR,
			MainItemType.AMULET,
			MainItemType.RING,
			MainItemType.PANTS,
			MainItemType.SHOE,
			MainItemType.GLOVER
		};
		foreach (MainItemType type in array)
		{
			MainItemInven itemByType = this.getItemByType(type);
			if (itemByType != null)
			{
				MainItem mainByCode = DataHolder.Instance.mainItemsDefine.getMainByCode(itemByType.code);
				foreach (OptionItem optionItem in mainByCode.optionItem)
				{
					if (optionItem.getTarget() == IncreaTarget.ATK)
					{
						if (optionItem.getType() == TypeBonus.ADD)
						{
							num += optionItem.getValue(itemByType.level);
						}
						if (optionItem.getType() == TypeBonus.PERCENT)
						{
							int num2 = num * optionItem.getValue(itemByType.level) / 100;
							num += num2;
						}
					}
				}
			}
		}
		SkillSave askill = DataHolder.Instance.skillData.getASkill("ADD-ATK");
		if (askill.level >= 1)
		{
			SkillPassive skillPassive = (SkillPassive)DataHolder.Instance.skillDefine.getSkill("ADD-ATK");
			int num3 = skillPassive.effectValue[askill.level];
			int num4 = num * num3 / 100;
			num += num4;
		}
		this.atk = num;
		this.save();
	}

	public void equippItem(MainItemType type, string key)
	{
		switch (type)
		{
		case MainItemType.ARMOR:
			this.armorSlot = key;
			break;
		case MainItemType.RING:
			this.ringSlot = key;
			break;
		case MainItemType.GLOVER:
			this.gloverSlot = key;
			break;
		case MainItemType.AMULET:
			this.amuletSlot = key;
			break;
		case MainItemType.PANTS:
			this.pantsSlot = key;
			break;
		case MainItemType.SHOE:
			this.shoeSlot = key;
			break;
		}
		DataHolder.Instance.playerData.reCalStat();
	}

	public void UnEquippItem(MainItemType type)
	{
		switch (type)
		{
		case MainItemType.ARMOR:
			this.armorSlot = "EMPTY";
			break;
		case MainItemType.RING:
			this.ringSlot = "EMPTY";
			break;
		case MainItemType.GLOVER:
			this.gloverSlot = "EMPTY";
			break;
		case MainItemType.AMULET:
			this.amuletSlot = "EMPTY";
			break;
		case MainItemType.PANTS:
			this.pantsSlot = "EMPTY";
			break;
		case MainItemType.SHOE:
			this.shoeSlot = "EMPTY";
			break;
		}
		DataHolder.Instance.playerData.reCalStat();
	}

	public void setName(string nameTxt)
	{
		this.name = nameTxt;
		this.save();
		if (CounterFather.Instance != null)
		{
			CounterFather.Instance.changeName();
		}
	}

	public void addGold(int value)
	{
		if (value < 0 && this.gold < Mathf.Abs(value))
		{
			throw new Exception("NOT_ENOUGHT_GOLD");
		}
		this.gold += value;
		this.save();
		if (CounterFather.Instance != null)
		{
			CounterFather.Instance.changeGold();
		}
		if (UpgradeNotifier.Instance != null)
		{
			UpgradeNotifier.Instance.refresh();
		}
		if (SkillNotifer.Instance != null)
		{
			SkillNotifer.Instance.refresh();
		}
	}

	public void addRuby(int value)
	{
		if (value < 0 && this.ruby < Mathf.Abs(value))
		{
			throw new Exception("NOT_ENOUGHT_RUBY");
		}
		this.ruby += value;
		this.save();
		if (CounterFather.Instance != null)
		{
			CounterFather.Instance.changeRuby();
		}
		if (UpgradeNotifier.Instance != null)
		{
			UpgradeNotifier.Instance.refresh();
		}
		if (SkillNotifer.Instance != null)
		{
			SkillNotifer.Instance.refresh();
		}
	}

	public void addEnergy(int value)
	{
		if (value < 0 && this.energy < Mathf.Abs(value))
		{
			throw new Exception("NOT_ENOUGHT_ENERGY");
		}
		this.energy += value;
		this.save();
		if (CounterFather.Instance != null)
		{
			CounterFather.Instance.changeEnergy();
		}
	}

	public void addSkip(int value)
	{
		if (value < 0 && this.skip < Mathf.Abs(value))
		{
			throw new Exception("NOT_ENOUGHT_SKIP");
		}
		this.skip += value;
		this.save();
		if (CounterFather.Instance != null)
		{
			CounterFather.Instance.changeSkip();
		}
	}

	public MainItemInven getItemByType(MainItemType type)
	{
		string empty = string.Empty;
		switch (type)
		{
		case MainItemType.ARMOR:
			empty = this.armorSlot;
			break;
		case MainItemType.RING:
			empty = this.ringSlot;
			break;
		case MainItemType.GLOVER:
			empty = this.gloverSlot;
			break;
		case MainItemType.AMULET:
			empty = this.amuletSlot;
			break;
		case MainItemType.PANTS:
			empty = this.pantsSlot;
			break;
		case MainItemType.SHOE:
			empty = this.shoeSlot;
			break;
		}
		return DataHolder.Instance.inventory.getMainItemByKey(empty);
	}

	public bool isEquipped(MainItemType type, string key)
	{
		switch (type)
		{
		case MainItemType.ARMOR:
			return this.armorSlot.Equals(key);
		case MainItemType.RING:
			return this.ringSlot.Equals(key);
		case MainItemType.GLOVER:
			return this.gloverSlot.Equals(key);
		case MainItemType.AMULET:
			return this.amuletSlot.Equals(key);
		case MainItemType.PANTS:
			return this.pantsSlot.Equals(key);
		case MainItemType.SHOE:
			return this.shoeSlot.Equals(key);
		default:
			return false;
		}
	}

	public new string name = string.Empty;

	public string armorSlot = "EMPTY";

	public string gloverSlot = "EMPTY";

	public string ringSlot = "EMPTY";

	public string amuletSlot = "EMPTY";

	public string pantsSlot = "EMPTY";

	public string shoeSlot = "EMPTY";

	public int ruby;

	public int gold;

	public int energy;

	public int skip;

	public int level;

	public int hp;

	public int exp;

	public int atk;

	public int def;

	public int speed;

	public int[] dailyGift;

	public float totalIAPPurchared;

	public int[] rewardIAPStackGift;

	public int[] rewardGrownGift;

	public int totalLevelPassed;

	public string firstDate;

	public int totalKillMons;

	public int bestCombo;

	public int curVip;

	public bool clickedRate;

	public int playCount;
}
