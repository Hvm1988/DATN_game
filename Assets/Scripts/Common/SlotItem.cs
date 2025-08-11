using System;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
	public class SlotItem : MonoBehaviour
	{
		public void init(Item item, ItemDefine itemDefine)
		{
			if (itemDefine == null)
			{
				itemDefine = DataHolder.Instance.shopItemDefine;
			}
			if (this.icon != null)
			{
				this.icon.sprite = itemDefine.getIcon(item.type, item.code);
				this.icon.transform.localScale = itemDefine.getScale(item.type);
			}
			if (this.number != null)
			{
				this.number.text = "x" + CustomInt.toString(item.number);
			}
			if (this.name != null)
			{
				this.name.text = itemDefine.getName(item.type, item.code);
			}
		}

		public Image icon;

		public new Text name;

		public Text number;
	}
}
