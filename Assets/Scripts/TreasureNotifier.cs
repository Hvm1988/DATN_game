using System;
using UnityEngine;

public class TreasureNotifier : Notifier
{
	public static TreasureNotifier Instance
	{
		get
		{
			if (TreasureNotifier.instance == null)
			{
				TreasureNotifier.instance = UnityEngine.Object.FindObjectOfType<TreasureNotifier>();
			}
			return TreasureNotifier.instance;
		}
	}

	public override void setUI()
	{
		this.counter = 0;
		if (DataHolder.Instance.inventory.getAllAttrByCode("SILVER-KEY") > 0 || DataHolder.Instance.inventory.getAllAttrByCode("GOLDEN-KEY") > 0 || DataHolder.Instance.inventory.getAllAttrByCode("DIAMOND-KEY") > 0 || DataHolder.Instance.inventory.getAllAttrByCode("LEGENDARY-KEY") > 0 || DataHolder.Instance.inventory.hasFreeKey())
		{
			this.counter = 1;
		}
		this.redNote.SetActive(this.counter > 0);
	}

	private static TreasureNotifier instance;
}
