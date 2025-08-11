using System;
using UnityEngine;
using UnityEngine.UI;

public class ResourceItemSlot : MonoBehaviour
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
			this.scrollIcon.enabled = false;
			this.productIcon.enabled = false;
			this.bgSlot.enabled = true;
			this.bgSlot.sprite = this.darkSlot;
		}
		if (isLocked)
		{
			this.bgSlot.enabled = false;
			this.iconRes.enabled = false;
			this.locked.enabled = true;
			this.number.enabled = false;
			this.scrollIcon.enabled = false;
			this.productIcon.enabled = false;
		}
	}

	public void init(ItemInven item)
	{
		this.item = item;
		this.locked.enabled = false;
		this.bgSlot.enabled = true;
		this.bgSlot.sprite = this.lightSlot;
		if (item.GetType() == typeof(ResourceItemInven))
		{
			ResourceItem resByCode = DataHolder.Instance.mainItemsDefine.getResByCode(item.code);
			this.iconRes.gameObject.SetActive(true);
			this.scrollIcon.gameObject.SetActive(false);
			this.iconRes.enabled = true;
			this.iconRes.sprite = resByCode.icon;
			this.number.enabled = true;
			this.number.text = ((ResourceItemInven)item).number + string.Empty;
		}
		else if (item.GetType() == typeof(ScrollItemInven))
		{
			this.number.enabled = false;
			ScrollItem scrollByCode = DataHolder.Instance.mainItemsDefine.getScrollByCode(item.code);
			this.iconRes.gameObject.SetActive(false);
			this.scrollIcon.gameObject.SetActive(true);
			this.scrollIcon.sprite = scrollByCode.icon;
			this.productIcon.sprite = scrollByCode.productIcon;
			this.scrollIcon.enabled = true;
			this.productIcon.enabled = true;
		}
	}

	public void onClick()
	{
		if (this.item == null || this.item.code.Equals(string.Empty))
		{
			return;
		}
		if (this.place == PlaceOfRes.INVENTORY)
		{
			if (this.item != null)
			{
				if (this.item.GetType() == typeof(ResourceItemInven))
				{
					InventoryManager.Instance.showResItemInfoPop((ResourceItemInven)this.item);
				}
				else
				{
					InventoryManager.Instance.showScrollItemInfoPop((ScrollItemInven)this.item);
				}
			}
		}
		else if (this.item != null)
		{
			if (this.item.GetType() == typeof(ResourceItemInven))
			{
				return;
			}
			if (this.item.GetType() == typeof(ScrollItemInven))
			{
				CrafItem.Instance.onClickToAScroll((ScrollItemInven)this.item);
			}
			else
			{
				CrafItem.Instance.onClickToAScroll(null);
			}
		}
		else
		{
			CrafItem.Instance.onClickToAScroll(null);
		}
	}

	public ItemInven item;

	public Image bgSlot;

	public Sprite lightSlot;

	public Sprite darkSlot;

	public Image iconRes;

	public Image locked;

	public Text number;

	public Image scrollIcon;

	public Image productIcon;

	public bool isEmpty;

	public bool isLocked;

	public PlaceOfRes place;
}
