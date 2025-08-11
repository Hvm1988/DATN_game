using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiftSlot : MonoBehaviour
{
	public void init(bool isEnable)
	{
		this.rewardBtn.interactable = isEnable;
	}

	public void onClick()
	{
		foreach (GiftSlot.Gift gift in this.gifts)
		{
			gift.reward();
		}
	}

	public Button rewardBtn;

	public List<GiftSlot.Gift> gifts;

	[Serializable]
	public class Gift
	{
		public void reward()
		{
			switch (this.type)
			{
			case ShopItemType.GOLD:
				DataHolder.Instance.playerData.addGold(this.number);
				break;
			case ShopItemType.RUBY:
				DataHolder.Instance.playerData.addRuby(this.number);
				break;
			case ShopItemType.RES:
			{
				ResourceItemInven item = ItemFactory.makeAResItem(this.code, this.number);
				DataHolder.Instance.inventory.pickUpAItem(item);
				break;
			}
			case ShopItemType.SKIP:
				DataHolder.Instance.playerData.addSkip(this.number);
				break;
			case ShopItemType.KEY:
			{
				AttritionItemInven item2 = ItemFactory.makeAAttrItem(this.code, this.number);
				DataHolder.Instance.inventory.pickUpAItem(item2);
				break;
			}
			case ShopItemType.ENERGY:
				DataHolder.Instance.playerData.addEnergy(this.number);
				break;
			case ShopItemType.SCROLL:
			{
				ScrollItemInven item3 = ItemFactory.makeAScrollItem(this.code);
				DataHolder.Instance.inventory.pickUpAItem(item3);
				break;
			}
			}
		}

		public string code;

		public ShopItemType type;

		public int number;
	}
}
