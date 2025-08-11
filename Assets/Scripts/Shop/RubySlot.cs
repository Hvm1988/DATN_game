using System;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
	public class RubySlot : MonoBehaviour
	{
		private void Start()
		{
			this.init();
		}

		private void init()
		{
			this.itemToBuy = DataHolder.Instance.shopDefine.staminas[this.id];
			this.price.text = this.itemToBuy.realPrice + string.Empty;
			if (this.itemToBuy.bonusValue > 0)
			{
				this.bonusBanner.SetActive(true);
				this.bonusText.text = "Bonus: <color=#28FF00FF>" + this.itemToBuy.bonusValue + "%</color>";
				this.number.text = string.Concat(new object[]
				{
					this.itemToBuy.item.number,
					"<color=#28FF00FF>+",
					this.itemToBuy.item.number * this.itemToBuy.bonusValue / 100,
					"</color>"
				});
			}
			else
			{
				this.bonusBanner.SetActive(false);
				this.number.text = this.itemToBuy.item.number + string.Empty;
			}
		}

		public void onClickBuy()
		{
			SpecialSoundManager.Instance.playAudio("PayRuby");
			try
			{
				DataHolder.Instance.playerData.addRuby(-this.itemToBuy.realPrice);
				Item item = new Item();
				item.type = this.itemToBuy.item.type;
				item.code = this.itemToBuy.item.code;
				item.number = this.itemToBuy.item.number;
				if (this.itemToBuy.bonusValue > 0)
				{
					item.number += item.number * this.itemToBuy.bonusValue / 100;
				}
				item.reward(1);
			}
			catch (Exception ex)
			{
				if (ex.Message.Equals("NOT_ENOUGHT_RUBY"))
				{
					this.notEnoughtRuby.SetActive(true);
				}
			}
		}

		public ShopDefine.SingleItem itemToBuy;

		public Text number;

		public int id;

		public Text price;

		public GameObject bonusBanner;

		public Text bonusText;

		public GameObject notEnoughtRuby;
	}
}
