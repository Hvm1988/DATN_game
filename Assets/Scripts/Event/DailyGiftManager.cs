using System;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Event
{
	public class DailyGiftManager : ItemSlotWrapper
	{
		private void Start()
		{
			this.init();
		}

		private void OnEnable()
		{
			this.onClick(DataHolder.Instance.playerData.getFirstLockGift());
		}

		public override void init()
		{
			this.items = DataHolder.Instance.dailyGiftDefine.items;
			this.setUI();
		}

		public override void setUI()
		{
			for (int i = 0; i < this.dailyGiftSlots.Length; i++)
			{
				this.dailyGiftSlots[i].init(this.items[i], DataHolder.Instance.shopItemDefine);
				if (DataHolder.Instance.playerData.dailyGift[i] == 0)
				{
					this.dailyGiftSlots[i].hightLight();
				}
				if (DataHolder.Instance.playerData.dailyGift[i] == 1)
				{
					this.dailyGiftSlots[i].disable();
				}
			}
		}

		public void reward()
		{
			if ((this.items[this.curID].type == ItemTypeUI.SCROLL || this.items[this.curID].type == ItemTypeUI.SCROLL_RANDOM || this.items[this.curID].type == ItemTypeUI.RES) && DataHolder.Instance.inventory.getFreeSlotResource() < 1)
			{
				UIController.Instance.outSlotItem.init(OutOfSlotItem.TypeOut.DAILY_RES, null);
				return;
			}
			if (this.items[this.curID].type == ItemTypeUI.MAINITEM && DataHolder.Instance.inventory.getFreeSlotMain() < 1)
			{
				UIController.Instance.outSlotItem.init(OutOfSlotItem.TypeOut.DAILY_MAIN, null);
				return;
			}
			DatePassHelper.saveNowToPref("DAILYGIFT", DatePassHelper.DateFormat.ddMMyyyy);
			DataHolder.Instance.playerData.rewardDailyGift(this.curID);
			this.items[this.curID].reward(1);
			this.setUI();
			this.rewardBtn.interactable = false;
			DailyGiftNotifier.Instance.refresh();
			EventNotifier.Instance.refresh();

		}

		public void onClick(int id)
		{
			SoundManager.Instance.playAudio("ButtonClick");
			this.curID = id;
			DailyGiftSlotItem dailyGiftSlotItem = this.dailyGiftSlots[id];
			this.icon.sprite = dailyGiftSlotItem.icon.sprite;
			this.number.text = "x" + dailyGiftSlotItem.item.number;
			if (DataHolder.Instance.playerData.dailyGift[id] == 0)
			{
				this.rewardBtn.interactable = true;
			}
			else
			{
				this.rewardBtn.interactable = false;
			}
		}

		public DailyGiftSlotItem[] dailyGiftSlots;

		public Item[] items;

		public Image icon;

		public Text number;

		public Button rewardBtn;

		public GameObject dailyGiftChild_prefabs;

		private int curID;
	}
}
