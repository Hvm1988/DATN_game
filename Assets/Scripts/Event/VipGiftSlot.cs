using System;
using Common;
using UnityEngine.UI;

namespace Event
{
	public class VipGiftSlot : ItemSlotWrapper
	{
		private void Start()
		{
			this.init();
		}

		public override void init()
		{
			this.stackGift = DataHolder.Instance.vipGiftDefine.stackGifts[this.id];
			this.setUI();
		}

		public void setRewardBtn(bool isEnable)
		{
			this.rewardBtn.interactable = isEnable;
		}

		public void reward()
		{
			this.stackGift.onReward();
			DataHolder.Instance.playerData.setRewardIAPStackGift(this.id, 1);
			this.rewardBtn.interactable = false;
			if (VipGiftNotifier.Instance != null)
			{
				VipGiftNotifier.Instance.setUI();
			}
			if (EventNotifier.Instance != null)
			{
				EventNotifier.Instance.setUI();
			}
		}

		public override void setUI()
		{
			for (int i = 0; i < this.slots.Length; i++)
			{
				if (i < this.stackGift.items.Length)
				{
					this.slots[i].gameObject.SetActive(true);
					this.slots[i].init(this.stackGift.items[i], DataHolder.Instance.shopItemDefine);
				}
				else
				{
					this.slots[i].gameObject.SetActive(false);
				}
			}
		}

		public int id;

		public VipGiftDefine.StackGift stackGift;

		public VipGiftSlotItem[] slots;

		public Button rewardBtn;

		public IAPStackGift iapSG;
	}
}
