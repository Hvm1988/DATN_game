using System;
using UnityEngine;

public class EventNotifier : Notifier
{
	public static EventNotifier Instance
	{
		get
		{
			if (EventNotifier.instance == null)
			{
				EventNotifier.instance = UnityEngine.Object.FindObjectOfType<EventNotifier>();
			}
			return EventNotifier.instance;
		}
	}

	public override void setUI()
	{
		this.counter = 0;
		for (int i = 0; i < DataHolder.Instance.playerData.rewardGrownGift.Length; i++)
		{
			if (DataHolder.Instance.playerData.rewardGrownGift[i] == 0)
			{
				this.counter = 1;
				break;
			}
		}
		if (DataHolder.Instance.playerData.haveRewardIAPStack())
		{
			this.counter = 1;
		}
		if (DataHolder.Instance.playerData.getCanRewarDaily() != -1)
		{
			this.counter = 1;
		}
		this.redNote.SetActive(this.counter > 0);
	}

	private static EventNotifier instance;
}
