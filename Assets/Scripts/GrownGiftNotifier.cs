using System;
using UnityEngine;

public class GrownGiftNotifier : Notifier
{
	public static GrownGiftNotifier Instance
	{
		get
		{
			if (GrownGiftNotifier.instance == null)
			{
				GrownGiftNotifier.instance = UnityEngine.Object.FindObjectOfType<GrownGiftNotifier>();
			}
			return GrownGiftNotifier.instance;
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
		this.redNote.SetActive(this.counter > 0);
	}

	private static GrownGiftNotifier instance;
}
