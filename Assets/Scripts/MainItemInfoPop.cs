using System;
using UnityEngine;
using UnityEngine.UI;

public class MainItemInfoPop : ItemInfoPop
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
		MainItem mainByCode = DataHolder.Instance.mainItemsDefine.getMainByCode(this.item.code);
		base.setUI(mainByCode);
		for (int i = 0; i < this.opDes.Length; i++)
		{
			if (i < mainByCode.optionItem.Count)
			{
				this.opDes[i].enabled = true;
				this.opDes[i].text = mainByCode.optionItem[i].getOpDesRichText(((MainItemInven)this.item).level);
			}
			else
			{
				this.opDes[i].enabled = false;
			}
		}
		if (DataHolder.Instance.playerData.isEquipped(mainByCode.getItemType(), this.item.key))
		{
			this.equippText.text = "UnEquip";
		}
		else
		{
			this.equippText.text = "Equip";
		}
	}

	public void equipOnclick()
	{
		MainItem mainByCode = DataHolder.Instance.mainItemsDefine.getMainByCode(this.item.code);
		UnityEngine.Debug.Log(mainByCode.code);
		if (DataHolder.Instance.playerData.isEquipped(mainByCode.getItemType(), this.item.key))
		{
			DataHolder.Instance.playerData.UnEquippItem(mainByCode.getItemType());
		}
		else
		{
			DataHolder.Instance.playerData.equippItem(mainByCode.getItemType(), this.item.key);
		}
		InventoryManager.Instance.refresh();
		UpgradeNotifier.Instance.refresh();
	}

	public override void sell()
	{
		base.gameObject.SetActive(false);
		InventoryManager.Instance.showSellPop(this.item);
	}

	public Text[] opNumbers;

	public Text[] opDes;

	public Button equippBtn;

	public Text equippText;
}
