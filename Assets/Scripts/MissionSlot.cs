using System;
using UnityEngine;
using UnityEngine.UI;

public class MissionSlot : MonoBehaviour
{
	public void init(string code, MissionWrapper mw)
	{
		this.mw = mw;
		this.code = code;
		this.mission = DataHolder.Instance.missionDefine.getMission(code);
		this.setUI(true);
	}

	public void setUI(bool setSibling = true)
	{
		MissionData.MissionSave missionSave = DataHolder.Instance.missionData.getMission(this.code);
		if (missionSave.canReward(this.mission))
		{
			if (setSibling)
			{
				base.transform.SetSiblingIndex(0);
			}
			this.rewardBtn.interactable = true;
			this.rewarded.SetActive(false);
		}
		else
		{
			this.rewardBtn.interactable = false;
			if (missionSave.status == 1)
			{
				this.rewardBtn.gameObject.SetActive(false);
				this.rewarded.SetActive(true);
			}
			else
			{
				this.rewardBtn.gameObject.SetActive(true);
				this.rewarded.SetActive(false);
			}
		}
		this.des.text = this.mission.getDetail();
		this.process.text = missionSave.done + "/" + this.mission.number;
		this.icon.sprite = this.mission.icon;
		this.numberGift.text = this.mission.gift.number + string.Empty;
	}

	public void reward()
	{
		SoundManager.Instance.playAudio("ButtonClick");
		DataHolder.Instance.missionData.reward(this.code);
		this.mission.gift.reward();
		this.mw.setUI();
	}

	public string code;

	public Text des;

	public Text process;

	public Text numberGift;

	public Mission mission;

	public Image icon;

	public Button rewardBtn;

	public GameObject rewarded;

	public MissionWrapper mw;
}
