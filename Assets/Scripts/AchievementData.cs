using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementData", menuName = "Game Data/Achievement Data")]
public class AchievementData : DataModel
{
	public override void initFirstTime()
	{
		for (int i = 0; i < this.achievements.Length; i++)
		{
			this.achievements[i].curStep = 0;
			this.achievements[i].done = 0;
			this.achievements[i].status = -1;
		}
	}

	public override void loadFromFireBase()
	{
		throw new NotImplementedException();
	}

	public AchievementData.AchievementSave getAchievementSave(string code)
	{
		foreach (AchievementData.AchievementSave achievementSave in this.achievements)
		{
			if (achievementSave.code.Equals(code))
			{
				return achievementSave;
			}
		}
		return null;
	}

	public void addDone(Achievement achievement, string code, int value)
	{
		this.getAchievementSave(code).addDone(achievement, value);
		if (MissionNotifer.Instance != null)
		{
			MissionNotifer.Instance.setUI();
		}
		this.save();
	}

	public void reward(Achievement achievement)
	{
		this.getAchievementSave(achievement.code).reward(achievement);
		if (MissionNotifer.Instance != null)
		{
			MissionNotifer.Instance.setUI();
		}
		this.save();
	}

	public AchievementData.AchievementSave[] achievements;

	[Serializable]
	public class AchievementSave
	{
		public bool canReward(Achievement achievement)
		{
			if (achievement == null)
			{
				achievement = DataHolder.Instance.achievementDefine.getAchievement(this.code);
			}
			return this.done >= this.getCurNumber(achievement) && this.status != 1;
		}

		public string getCurDetail(Achievement achievement)
		{
			int num = this.getCurNumber(achievement);
			if (this.code.Equals("REACH-LEVEL"))
			{
				num++;
			}
			return achievement.detail.Replace("#VALUE", num + string.Empty);
		}

		public float getCurProcessFloat(Achievement achievement)
		{
			return (float)this.done / (float)this.getCurNumber(achievement);
		}

		public string getCurProcessString(Achievement achievement)
		{
			return this.done + "/" + this.getCurNumber(achievement);
		}

		public int getCurNumber(Achievement achievement)
		{
			if (achievement == null)
			{
				achievement = DataHolder.Instance.achievementDefine.getAchievement(this.code);
			}
			return achievement.number + achievement.numberStep * this.curStep;
		}

		public GiftSlot.Gift getCurGift(Achievement achievement)
		{
			if (achievement == null)
			{
				achievement = DataHolder.Instance.achievementDefine.getAchievement(this.code);
			}
			return new GiftSlot.Gift
			{
				code = achievement.gift.code,
				type = achievement.gift.type,
				number = achievement.gift.number + achievement.giftValueStep * this.curStep
			};
		}

		public void addDone(Achievement achievement, int value)
		{
			if (achievement == null)
			{
				achievement = DataHolder.Instance.achievementDefine.getAchievement(this.code);
			}
			int curNumber = this.getCurNumber(achievement);
			this.done += value;
			if (this.done >= curNumber)
			{
				this.status = 0;
			}
		}

		public void reward(Achievement achievement)
		{
			if (achievement == null)
			{
				achievement = DataHolder.Instance.achievementDefine.getAchievement(this.code);
			}
			this.getCurGift(achievement).reward();
			this.curStep++;
			if (!this.canReward(achievement))
			{
				this.status = 1;
			}
		}

		public string code;

		public int curStep;

		public int done;

		public int status;
	}
}
