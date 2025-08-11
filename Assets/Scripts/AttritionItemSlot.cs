using System;
using UnityEngine;
using UnityEngine.UI;

public class AttritionItemSlot : MonoBehaviour
{
	public void init(bool isEmpty, bool isLocked)
	{
		this.isEmpty = isEmpty;
		this.isLocked = isLocked;
		if (isEmpty)
		{
			this.iconRes.enabled = false;
			this.locked.enabled = false;
			this.number.enabled = false;
			this.bgSlot.sprite = this.darkSlot;
			this.bgSlot.enabled = true;
		}
		if (isLocked)
		{
			this.iconRes.enabled = false;
			this.locked.enabled = true;
			this.number.enabled = false;
			this.bgSlot.enabled = false;
		}
	}

	public void init(ItemInven item)
	{
		this.item = item;
		this.number.enabled = true;
		this.locked.enabled = false;
		this.bgSlot.enabled = true;
		this.bgSlot.sprite = this.lightSlot;
		AttritionItem attrByCode = DataHolder.Instance.mainItemsDefine.getAttrByCode(item.code);
		this.iconRes.gameObject.SetActive(true);
		this.iconRes.enabled = true;
		this.iconRes.sprite = attrByCode.icon;
		this.number.text = ((AttritionItemInven)item).number + string.Empty;
	}

	public void onClick()
	{
		if (this.item != null)
		{
			InventoryManager.Instance.showAttritionItemInfoPop((AttritionItemInven)this.item);
		}
	}

	public ItemInven item;

	public Image iconRes;

	public Image locked;

	public Text number;

	public Image bgSlot;

	public Sprite lightSlot;

	public Sprite darkSlot;

	public bool isEmpty;

	public bool isLocked;
}
