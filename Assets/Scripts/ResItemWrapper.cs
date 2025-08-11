using System;
using UnityEngine;

public class ResItemWrapper : MonoBehaviour
{
	private void OnEnable()
	{
		CrafItem.onRefresh += this.setUI;
		this.setUI();
	}

	private void OnDisable()
	{
		CrafItem.onRefresh -= this.setUI;
	}

	private void setUI()
	{
		DataHolder.Instance.inventory.clearResList();
		if (this.type == TypeRes.RES)
		{
			for (int i = 0; i < this.resItemSlots.Length; i++)
			{
				if (i < DataHolder.Instance.inventory.resourceItems.Count)
				{
					this.resItemSlots[i].gameObject.SetActive(true);
					this.resItemSlots[i].init(DataHolder.Instance.inventory.resourceItems[i]);
				}
				else
				{
					this.resItemSlots[i].gameObject.SetActive(false);
				}
			}
		}
		if (this.type == TypeRes.SCROLL)
		{
			for (int j = 0; j < this.resItemSlots.Length; j++)
			{
				if (j < DataHolder.Instance.inventory.scrollItems.Count)
				{
					this.resItemSlots[j].gameObject.SetActive(true);
					this.resItemSlots[j].init(DataHolder.Instance.inventory.scrollItems[j]);
				}
				else
				{
					this.resItemSlots[j].gameObject.SetActive(false);
				}
			}
			if (this.resItemSlots[0].item != null)
			{
				this.resItemSlots[0].onClick();
			}
		}
	}

	public ResourceItemSlot[] resItemSlots;

	public TypeRes type;
}
