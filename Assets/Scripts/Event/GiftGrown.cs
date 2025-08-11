using System;
using Common;
using UnityEngine.UI;

namespace Event
{
	public class GiftGrown : SlotItem
	{
		private void OnEnable()
		{
			this.gGift = DataHolder.Instance.grownGiftDefine.growGifts[this.id];
			this.level.text = "Level " + (this.gGift.level + 1).ToString();
			base.init(this.gGift.item, DataHolder.Instance.shopItemDefine);
			this.rewardBtn.interactable = DataHolder.Instance.playerData.canRewardGrownUp(this.id);
		}

		public void onClick()
		{
			DataHolder.Instance.playerData.rewardGrownUp(this.id);
			this.gGift.item.reward(1);
			this.rewardBtn.interactable = DataHolder.Instance.playerData.canRewardGrownUp(this.id);
		}

		public int id;

		public Button rewardBtn;

		public GrownGiftDefine.GrownGift gGift;

		public Text level;
	}
}
