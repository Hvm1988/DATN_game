using System;
using UnityEngine;

public class VipGiftNotifier : Notifier
{
	public static VipGiftNotifier Instance
	{
		get
		{
			if (VipGiftNotifier.instance == null)
			{
				VipGiftNotifier.instance = UnityEngine.Object.FindObjectOfType<VipGiftNotifier>();
			}
			return VipGiftNotifier.instance;
		}
	}

	public override void setUI()
	{
		this.counter = 0;
		if (DataHolder.Instance.playerData.haveRewardIAPStack())
		{
			this.counter = 1;
		}
		this.redNote.SetActive(this.counter > 0);
	}

	private static VipGiftNotifier instance;
}
