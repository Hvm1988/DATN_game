using System;
using System.Collections.Generic;

namespace Common
{
	[Serializable]
	public class Item
	{
		public Item()
		{
			this.number = 0;
		}

		public void reward(int multiple = 1)
		{
			switch (this.type)
			{
			case ItemTypeUI.GOLD:
				DataHolder.Instance.playerData.addGold(this.number * multiple);
				break;
			case ItemTypeUI.RUBY:
				DataHolder.Instance.playerData.addRuby(this.number * multiple);
				break;
			case ItemTypeUI.RES:
			{
				ResourceItemInven item = ItemFactory.makeAResItem(this.code, this.number * multiple);
				DataHolder.Instance.inventory.pickUpAItem(item);
				break;
			}
			case ItemTypeUI.SKIP:
				DataHolder.Instance.playerData.addSkip(this.number * multiple);
				break;
			case ItemTypeUI.KEY:
			{
				AttritionItemInven item2 = ItemFactory.makeAAttrItem(this.code, this.number * multiple);
				DataHolder.Instance.inventory.pickUpAItem(item2);
				break;
			}
			case ItemTypeUI.ENERGY:
				DataHolder.Instance.playerData.addEnergy(this.number * multiple);
				break;
			case ItemTypeUI.SCROLL:
			{
				ScrollItemInven item3 = ItemFactory.makeAScrollItem(this.code);
				DataHolder.Instance.inventory.pickUpAItem(item3);
				break;
			}
			case ItemTypeUI.SCROLL_RANDOM:
			{
				int num = int.Parse(this.code);
				List<string> list = new List<string>();
				NItem randomItemWithRange = DataHolder.Instance.mainItemsDefine.getRandomItemWithRange(ItemType.SCROLL, (ItemColor)num, (ItemColor)num);
				ScrollItemInven item4 = ItemFactory.makeAScrollItem(randomItemWithRange.code);
				DataHolder.Instance.inventory.pickUpAItem(item4);
				break;
			}
			}
		}

		public string code;

		public ItemTypeUI type;

		public int number;
	}
}
