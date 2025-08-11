using System;
using UnityEngine;

public class ResourceItemWrawpper : MonoBehaviour
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
		int count = DataHolder.Instance.inventory.scrollItems.Count;
		for (int i = 0; i < DataHolder.Instance.inventory.maxSlotMainItem; i++)
		{
			if (i < DataHolder.Instance.inventory.scrollItems.Count)
			{
				this.resourceSlots[i].init(DataHolder.Instance.inventory.scrollItems[i]);
			}
			else if (i - count < DataHolder.Instance.inventory.resourceItems.Count)
			{
				this.resourceSlots[i].init(DataHolder.Instance.inventory.resourceItems[i - count]);
			}
			else if (i < DataHolder.Instance.inventory.currentOpenSlotResource)
			{
				this.resourceSlots[i].init(true, false);
			}
			else
			{
				this.resourceSlots[i].init(false, true);
			}
		}
	}

	public ResourceItemSlot[] resourceSlots;
}
