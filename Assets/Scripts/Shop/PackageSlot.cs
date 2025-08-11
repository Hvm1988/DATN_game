using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
	public class PackageSlot : IAPSlot
	{
		private void Start()
		{
			this.init();
			if (this.titleIcon != null)
			{
				this.titleIcon.sprite = this.titleSprites[this.titleSpriteId[this.name]];
			}
		}

		public override void init()
		{
			this.packageData = DataHolder.Instance.shopDefine.getPackage(this.name);
			this.setUI();
		}

		public override void setUI()
		{
			if (this.packageData == null)
			{
				return;
			}
			for (int i = 0; i < this.packageData.items.Length; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.itemPrefaps);
				gameObject.transform.SetParent(this.holder, false);
				gameObject.transform.localScale = Vector2.one;
				//gameObject.GetComponent<PackageSlotItem>().init(this.packageData.items[i], DataHolder.Instance.shopItemDefine);
			}
			if (this.price_txt != null)
			{
				this.price_txt.text = ((float)this.packageData.price - 0.01f).ToString() + "$";
			}
			if (this.realPrice_txt != null)
			{
				this.realPrice_txt.text = ((float)this.packageData.realPrice - 0.01f).ToString() + "$";
			}
			float num = ((float)this.packageData.price - (float)this.packageData.realPrice) / (float)this.packageData.price;
			num *= 100f;
			if (this.saleOffValue != null)
			{
				this.saleOffValue.text = "Sale off\n" + (int)num + "%";
			}
		}

		public void onClickBuy()
		{
			this.packageData.buy();

		}

		public new string name;

		public GameObject itemPrefaps;

		public ShopDefine.Package packageData;

		[SerializeField]
		public Transform holder;

		public Sprite[] titleSprites;

		public Text saleOffValue;

		private Dictionary<string, int> titleSpriteId = new Dictionary<string, int>
		{
			{
				"starter",
				0
			},
			{
				"material",
				1
			},
			{
				"diamond",
				2
			},
			{
				"plantium",
				3
			},
			{
				"super",
				4
			},
			{
				"rich",
				5
			}
		};

		public Image titleIcon;
	}
}
