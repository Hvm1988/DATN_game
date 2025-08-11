using System;
using UnityEngine;

namespace Event
{
	public class VipGiftManager : MonoBehaviour
	{
		private void OnEnable()
		{
			this.curVip = DataHolder.Instance.playerData.curVip;
			if (this.curVip == -1)
			{
				this.setActiveGift(0);
			}
			else
			{
				this.setActiveGift(this.curVip);
			}
			this.setUI();
		}

		private void setUI()
		{
			float[] iapGiftPoint = DataHolder.Instance.playerDefine.iapGiftPoint;
			float totalIAPPurchared = DataHolder.Instance.playerData.totalIAPPurchared;
			for (int i = 0; i < iapGiftPoint.Length; i++)
			{
				if (DataHolder.Instance.playerData.rewardIAPStackGift[i] == 0)
				{
					this.vipGiftStacks[i].setRewardBtn(true);
				}
				if (DataHolder.Instance.playerData.rewardIAPStackGift[i] != 0)
				{
					this.vipGiftStacks[i].setRewardBtn(false);
				}
			}
		}

		private void setGiftDetail()
		{
			if (this.vipGiftStacks.Length == 0)
			{
				return;
			}
			int firstCanRewardIAP = DataHolder.Instance.playerData.getFirstCanRewardIAP();
			if (firstCanRewardIAP != -1)
			{
				this.setActiveGift(firstCanRewardIAP);
				this.vipGiftStacks[firstCanRewardIAP].setRewardBtn(true);
			}
			else
			{
				int firstLockRewardIAP = DataHolder.Instance.playerData.getFirstLockRewardIAP();
				if (firstLockRewardIAP != -1)
				{
					this.vipGiftStacks[firstLockRewardIAP].gameObject.SetActive(true);
				}
				else
				{
					this.vipGiftStacks[this.vipGiftStacks.Length - 1].gameObject.SetActive(true);
				}
			}
		}

		public void setActiveGift(int id)
		{
			this.vipGiftStacks[this.curID].gameObject.SetActive(false);
			this.vipGiftStacks[id].gameObject.SetActive(true);
			this.curID = id;
			this.iapStackGift.selectAVip(id);
		}

		public void next()
		{
			int activeGift = this.curID + 1;
			if (this.curID == this.vipGiftStacks.Length - 1)
			{
				activeGift = 0;
			}
			this.setActiveGift(activeGift);
		}

		public void prev()
		{
			int activeGift = this.curID - 1;
			if (this.curID == 0)
			{
				activeGift = this.vipGiftStacks.Length - 1;
			}
			this.setActiveGift(activeGift);
		}

		public VipGiftSlot[] vipGiftStacks;

		public IAPStackGift iapStackGift;

		public int curID;

		private int curVip;
	}
}
