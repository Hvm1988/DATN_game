using System;
using UnityEngine;

public class SellPop : MonoBehaviour
{
	public void init(ItemInven item)
	{
		this.item = item;
	}

	public void sell()
	{
		DataHolder.Instance.inventory.sellAItem(this.item);
		InventoryManager.Instance.refresh();
		base.gameObject.SetActive(false);
	}

	public ItemInven item;
}
