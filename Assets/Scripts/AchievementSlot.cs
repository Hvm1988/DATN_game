using System;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
	public void init(string code, AchievementWrapper aw)
	{
		this.aw = aw;
		this.code = code;
		this.achievement = DataHolder.Instance.achievementDefine.getAchievement(code);
		this.setUI();
	}

	public void setUI()
	{
		AchievementData.AchievementSave achievementSave = DataHolder.Instance.achievementData.getAchievementSave(this.code);
		this.des.text = achievementSave.getCurDetail(this.achievement);
		this.processBar.fillAmount = achievementSave.getCurProcessFloat(this.achievement);
		this.process.text = achievementSave.getCurProcessString(this.achievement);
		this.rewardBtn.interactable = achievementSave.canReward(this.achievement);
		this.icon.sprite = this.achievement.icon;
		this.numberGift.text = achievementSave.getCurGift(this.achievement).number + string.Empty;
		if (achievementSave.canReward(this.achievement))
		{
			base.transform.SetSiblingIndex(0);
		}
	}

	public void reward()
	{
		DataHolder.Instance.achievementData.reward(this.achievement);
		this.aw.setUI();
	}

	public string code;

	public Achievement achievement;

	public Text des;

	public Image processBar;

	public Text process;

	public Button rewardBtn;

	public Image icon;

	public Text numberGift;

	public AchievementWrapper aw;
}
