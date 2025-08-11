using System;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
	public void init(bool isEmpty, bool isLocked)
	{
		this.isEmpty = isEmpty;
		this.isLocked = isLocked;
		if (isEmpty)
		{
			this.icon.enabled = false;
			this.locked.enabled = false;
			this.equipped.enabled = false;
			this.level.enabled = false;
		}
		if (isLocked)
		{
			this.icon.enabled = false;
			this.locked.enabled = true;
			this.equipped.enabled = false;
			this.level.enabled = false;
		}
	}

	public void init(MainItemInven _item)
	{
		this.item = _item;
		if (this.item != null)
		{
			MainItem mainByCode = DataHolder.Instance.mainItemsDefine.getMainByCode(_item.code);
			this.locked.enabled = false;
			this.icon.sprite = mainByCode.icon;
			this.icon.enabled = true;
			this.equipped.enabled = DataHolder.Instance.playerData.isEquipped(mainByCode.getItemType(), this.item.key);
			this.level.enabled = true;
			this.level.text = this.item.getLevelString();
		}
	}

	public void onClick()
	{
		if (this.item != null)
		{
			InventoryManager.Instance.showMainItemInfoPop(this.item);
		}
	}

	public MainItemInven item;

	public Image icon;

	public Image locked;

	public Text equipped;

	public Text level;

	public bool isEmpty;

	public bool isLocked;
}
