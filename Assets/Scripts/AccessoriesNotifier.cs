using System;
using UnityEngine;

public class AccessoriesNotifier : Notifier
{
	public static AccessoriesNotifier Instance
	{
		get
		{
			if (AccessoriesNotifier.instance == null)
			{
				AccessoriesNotifier.instance = UnityEngine.Object.FindObjectOfType<AccessoriesNotifier>();
			}
			return AccessoriesNotifier.instance;
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

	private static AccessoriesNotifier instance;
}
