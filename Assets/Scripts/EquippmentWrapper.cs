using System;
using System.Collections.Generic;
using UnityEngine;

public class EquippmentWrapper : MonoBehaviour
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
			if (i < DataHolder.Instance.inventory.mainItems.Count)
			{
				this.equipmentSlots[i].init(DataHolder.Instance.inventory.mainItems[i]);
			}
			else if (i < DataHolder.Instance.inventory.currentOpenSlotMainItem)
			{
				this.equipmentSlots[i].init(true, false);
			}
			else
			{
				this.equipmentSlots[i].init(false, true);
			}
		}
	}

	public GameObject equippmentPrefabs;

	public List<EquipmentSlot> equipmentSlots;

	public RectTransform holder;
}
