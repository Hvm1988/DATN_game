using System;
using UnityEngine;

public class AttritionItemWrapper : MonoBehaviour
{
	private void OnEnable()
	{
		InventoryManager.onRefresh += this.init;
		this.init(null);
	}

	private void OnDisable()
	{
		InventoryManager.onRefresh -= this.init;
	}

	private void init(NItem NI = null)
	{
		for (int i = 0; i < DataHolder.Instance.inventory.maxSlotMainItem; i++)
		{
			if (i < DataHolder.Instance.inventory.attritionItems.Count)
			{
				this.attritionSlots[i].init(DataHolder.Instance.inventory.attritionItems[i]);
			}
			else if (i < DataHolder.Instance.inventory.currentOpenSlotResource)
			{
				this.attritionSlots[i].init(true, false);
			}
			else
			{
				this.attritionSlots[i].init(false, true);
			}
		}
	}

	public AttritionItemSlot[] attritionSlots;
}
