using System;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
	public class RubyTabSlot : SlotItem
	{
		private void OnEnable()
		{
			IAPBuy.onBuySuccessIAP += this.onBuySuccess;
			this.init();
		}

		private void OnDisable()
		{
			IAPBuy.onBuySuccessIAP -= this.onBuySuccess;
		}

		public void init()
		{
			this.item = DataHolder.Instance.shopDefine.rubys[this.id];
			base.init(this.item.item, DataHolder.Instance.shopItemDefine);
			this.setUI();
		}

		public void onBuySuccess()
		{
			PlayerPrefs.SetInt("bought" + this.item.code, 1);
			this.setUI();
		}

		public void setUI()
		{
			this.isFirstTime = (!PlayerPrefs.HasKey("bought" + this.item.code) || PlayerPrefs.GetInt("bought" + this.item.code) == 0);
			this.firstTimeBanner.SetActive(this.isFirstTime && this.item.firstTimeBonus > 0);
			this.title_txt.text = this.item.name;
			if (this.isFirstTime && this.item.firstTimeBonus > 0)
			{
				this.desInBanner.text = "First time buy \n<color=#28FF00FF>+" + this.item.firstTimeBonus + "%</color>";
				float num = (float)this.item.firstTimeBonus / 100f;
				this.number_txt.text = CustomInt.toString(this.item.item.number) + "<color=#28FF00FF> +" + CustomInt.toString((int)((float)this.item.item.number * num)) + "</color>";
			}
			else
			{
				this.number_txt.text = CustomInt.toString(this.item.item.number) + string.Empty;
			}
			this.price.text = (float)this.item.realPrice - 0.01f + "$";
		}

		public void onBuy()
		{
			this.item.buy();
			this.setUI();
		}

		public Text title_txt;

		public GameObject firstTimeBanner;

		public ShopDefine.SingleItem item;

		public Text number_txt;

		public int id;

		public bool isFirstTime;

		public Text price;

		public Text desInBanner;
	}
}
