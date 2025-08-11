using System;
using UnityEngine;

namespace Common
{
	[CreateAssetMenu(fileName = "ItemShopDefine", menuName = "Game Data/Item Shop Define")]
	public class ItemDefine : ScriptableObject
	{
		public ItemDefine.Item getItem(ItemTypeUI type)
		{
			foreach (ItemDefine.Item item in this.items)
			{
				if (item.type == type)
				{
					return item;
				}
			}
			return null;
		}

		public Sprite getIcon(ItemTypeUI type, string code)
		{
			if (type == ItemTypeUI.KEY)
			{
				AttritionItem attrByCode = DataHolder.Instance.mainItemsDefine.getAttrByCode(code);
				return attrByCode.icon;
			}
			if (type == ItemTypeUI.RES)
			{
				ResourceItem resByCode = DataHolder.Instance.mainItemsDefine.getResByCode(code);
				return resByCode.icon;
			}
			if (type == ItemTypeUI.MAINITEM)
			{
				MainItem mainByCode = DataHolder.Instance.mainItemsDefine.getMainByCode(code);
				return mainByCode.icon;
			}
			if (type == ItemTypeUI.SCROLL_RANDOM)
			{
				int num = int.Parse(code);
				return this.scrollIcons[num];
			}
			return this.getItem(type).getIcon();
		}

		public Vector2 getScale(ItemTypeUI type)
		{
			ItemDefine.Item item = this.getItem(type);
			if (item != null)
			{
				return item.scale;
			}
			return Vector2.one;
		}

		public string getName(ItemTypeUI type, string code)
		{
			if (type == ItemTypeUI.KEY)
			{
				AttritionItem attrByCode = DataHolder.Instance.mainItemsDefine.getAttrByCode(code);
				return attrByCode.name;
			}
			if (type == ItemTypeUI.RES)
			{
				ResourceItem resByCode = DataHolder.Instance.mainItemsDefine.getResByCode(code);
				return resByCode.name;
			}
			if (type == ItemTypeUI.MAINITEM)
			{
				MainItem mainByCode = DataHolder.Instance.mainItemsDefine.getMainByCode(code);
				return mainByCode.name;
			}
			if (type == ItemTypeUI.SCROLL_RANDOM)
			{
				int num = int.Parse(code);
				return this.scrollName[num];
			}
			return this.getItem(type).getName();
		}

		public ItemDefine.Item[] items;

		public Sprite[] scrollIcons;

		public string[] scrollName;

		[Serializable]
		public class Item
		{
			public Sprite getIcon()
			{
				if (this.type == ItemTypeUI.GOLD || this.type == ItemTypeUI.RUBY || this.type == ItemTypeUI.SKIP)
				{
					return this.icon;
				}
				return null;
			}

			public string getName()
			{
				if (this.type == ItemTypeUI.GOLD || this.type == ItemTypeUI.RUBY || this.type == ItemTypeUI.SKIP)
				{
					return this.name;
				}
				return string.Empty;
			}

			public string name;

			public string code;

			public ItemTypeUI type;

			public Sprite icon;

			public Vector2 scale;
		}
	}
}
