using System;
using Common;
using UnityEngine;

namespace Shop
{
	[CreateAssetMenu(fileName = "ShopDefine", menuName = "Game Data/ShopDefine")]
	public class ShopDefine : DataModel
	{
		public override void initFirstTime()
		{
		}

		public override void loadFromFireBase()
		{
			
		}

		public ShopDefine.Package getPackage(string name)
		{
			foreach (ShopDefine.Package package in this.packages)
			{
				if (package.name.Equals(name))
				{
					return package;
				}
			}
			return null;
		}

		public ShopDefine.Package[] packages;

		public ShopDefine.SingleItem[] rubys;

		public ShopDefine.SingleItem[] items;

		public ShopDefine.SingleItem[] staminas;

		public abstract class IapBuyData
		{
			public abstract void buy();

			public string name;

			public int price;

			public int realPrice;

			public int bonusValue;
		}

		[Serializable]
		public class Package : ShopDefine.IapBuyData
		{

			public override void buy()
			{
				IAPBuy.Instance.onClickBuy(this.items, this.realPrice);
			}
			public Item[] items;
		}

		[Serializable]
		public class SingleItem : ShopDefine.IapBuyData
		{
			public override void buy()
			{
				if ((this.firstTimeBonus > 0 && !PlayerPrefs.HasKey("bought" + this.code)) || PlayerPrefs.GetInt("bought" + this.code) == 0)
				{
					Item item = new Item();
					float num = 1f + (float)this.firstTimeBonus / 100f;
					item.number = (int)((float)this.item.number * num);
					item.code = this.item.code;
					item.type = this.item.type;
					Item[] items = new Item[]
					{
						item
					};
					IAPBuy.Instance.onClickBuy(items, this.realPrice);
				}
				else
				{
					Item[] items2 = new Item[]
					{
						this.item
					};
					IAPBuy.Instance.onClickBuy(items2, this.realPrice);
				}
			}

			public Item item;

			public string code;

			public int firstTimeBonus;
		}
	}
}
