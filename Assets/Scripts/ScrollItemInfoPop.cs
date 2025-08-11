using System;
using UnityEngine.UI;

public class ScrollItemInfoPop : ItemInfoPop
{
	public override void init(ItemInven item)
	{
		this.item = item;
		this.setUI(null);
	}

	private void OnEnable()
	{
		InventoryManager.onRefresh += this.setUI;
	}

	private void OnDisable()
	{
		InventoryManager.onRefresh -= this.setUI;
	}

	public override void setUI(NItem item = null)
	{
		ScrollItem scrollByCode = DataHolder.Instance.mainItemsDefine.getScrollByCode(this.item.code);
		base.setUI(scrollByCode);
		this.productIcon.sprite = scrollByCode.productIcon;
		MainItem mainByCode = DataHolder.Instance.mainItemsDefine.getMainByCode(scrollByCode.codeProduct);
		for (int i = 0; i < this.opDes.Length; i++)
		{
			if (i < mainByCode.optionItem.Count)
			{
				this.opDes[i].enabled = true;
				this.opDes[i].text = mainByCode.optionItem[i].getOpDesRichText(0);
			}
			else
			{
				this.opDes[i].enabled = false;
			}
		}
	}

	public override void sell()
	{
		InventoryManager.Instance.showSellPop(this.item);
		base.gameObject.SetActive(false);
	}

	public Image productIcon;

	public Text[] opDes;
}
