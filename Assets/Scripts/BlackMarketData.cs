using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

[CreateAssetMenu(fileName = "BlackMarketData", menuName = "Game Data/BlackMarket Data")]
public class BlackMarketData : DataModel
{
	public override void initFirstTime()
	{
		this.initItem();
	}

	public override void loadFromFireBase()
	{
		throw new NotImplementedException();
	}

	public void checkInit()
	{
		this.loadFromPref();
	}

	public void initItem()
	{
		this.status = new int[10];
		this.itemToBuys = new List<BlackMarketData.ItemInBM>();
		List<KeyValuePair<ItemTypeUI, int>> list = new List<KeyValuePair<ItemTypeUI, int>>(this.itemToBuyType);
		for (int i = 0; i < list.Count; i++)
		{
			ItemTypeUI key = list[i].Key;
			int num = this.itemToBuyType[key];
			for (int j = 0; j < num; j++)
			{
				BlackMarketData.ItemInBM itemInBM = new BlackMarketData.ItemInBM();
				itemInBM.type = key;
				if (key == ItemTypeUI.RES)
				{
					ResourceItem resourceItem = (ResourceItem)DataHolder.Instance.mainItemsDefine.getRandomItemWithRange(ItemType.RES, ItemColor.BLUE, ItemColor.RED);
					itemInBM.code = resourceItem.code;
					itemInBM.number = 100;
					itemInBM.icon = resourceItem.icon;
					itemInBM.price = 50;
				}
				if (key == ItemTypeUI.KEY)
				{
					AttritionItem attritionItem = (AttritionItem)DataHolder.Instance.mainItemsDefine.getRandomItemWithRange(ItemType.ATTRITION, ItemColor.WHITE, ItemColor.RED);
					itemInBM.code = attritionItem.code;
					itemInBM.number = 10;
					itemInBM.icon = attritionItem.icon;
					itemInBM.price = 100;
				}
				if (key == ItemTypeUI.SCROLL)
				{
					ScrollItem scrollItem = (ScrollItem)DataHolder.Instance.mainItemsDefine.getRandomItemWithRange(ItemType.SCROLL, ItemColor.WHITE, ItemColor.RED);
					itemInBM.code = scrollItem.code;
					itemInBM.number = 1;
					itemInBM.icon = scrollItem.icon;
					itemInBM.price = 50;
				}
				if (key == ItemTypeUI.GOLD)
				{
					itemInBM.code = "GOLD";
					itemInBM.number = 10000;
					itemInBM.icon = this.goldIcon;
					itemInBM.price = 50;
				}
				if (key == ItemTypeUI.SKIP)
				{
					itemInBM.code = "SKIP";
					itemInBM.number = 100;
					itemInBM.icon = this.skipIcon;
					itemInBM.price = 50;
				}
				if (key == ItemTypeUI.ENERGY)
				{
					itemInBM.code = "ENERGY";
					itemInBM.number = 50;
					itemInBM.icon = this.energyIcon;
					itemInBM.price = 40;
				}
				this.itemToBuys.Add(itemInBM);
			}
		}
		for (int k = 0; k < this.itemToBuys.Count; k++)
		{
			BlackMarketData.ItemInBM value = this.itemToBuys[k];
			int index = UnityEngine.Random.Range(0, this.itemToBuys.Count);
			this.itemToBuys[k] = this.itemToBuys[index];
			this.itemToBuys[index] = value;
		}
		this.save();
	}

	public List<BlackMarketData.ItemInBM> itemToBuys;

	public int[] status = new int[10];

	public Sprite goldIcon;

	public Sprite skipIcon;

	public Sprite energyIcon;

	private Dictionary<ItemTypeUI, int> itemToBuyType = new Dictionary<ItemTypeUI, int>
	{
		{
			ItemTypeUI.RES,
			3
		},
		{
			ItemTypeUI.KEY,
			2
		},
		{
			ItemTypeUI.GOLD,
			1
		},
		{
			ItemTypeUI.SKIP,
			1
		},
		{
			ItemTypeUI.ENERGY,
			1
		},
		{
			ItemTypeUI.SCROLL,
			2
		}
	};

	[Serializable]
	public class ItemInBM : Item
	{
		public int price;

		public Sprite icon;
	}
}
