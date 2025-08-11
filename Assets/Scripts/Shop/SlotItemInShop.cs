using System;
using Common;
using UnityEngine.UI;

namespace Shop
{
	public class SlotItemInShop : SlotItem
	{
		public void init(ShopDefine.SingleItem item, ItemDefine itemDefine)
		{
			this.item = item;
			base.init(item.item, itemDefine);
			this.price.text = (float)item.realPrice - 0.01f + "$";
		}

		public void onBuy()
		{
			this.item.buy();

		}

		public ShopDefine.SingleItem item;

		public Text price;
	}
}
