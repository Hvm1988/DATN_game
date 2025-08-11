using System;
using UnityEngine;

public class DailyGiftNotifier : Notifier
{
	public static DailyGiftNotifier Instance
	{
		get
		{
			if (DailyGiftNotifier.instance == null)
			{
				DailyGiftNotifier.instance = UnityEngine.Object.FindObjectOfType<DailyGiftNotifier>();
			}
			return DailyGiftNotifier.instance;
		}
	}

	public override void setUI()
	{
		this.counter = 0;
		if (DataHolder.Instance.playerData.getCanRewarDaily() != -1)
		{
			this.counter = 1;
		}
		this.redNote.SetActive(this.counter > 0);
	}

	private static DailyGiftNotifier instance;
}
