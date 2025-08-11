using System;
using Common;
using UnityEngine;

namespace Event
{
	[CreateAssetMenu(fileName = "VipGiftDefine", menuName = "Game Data/Vip Gift Define")]
	public class VipGiftDefine : DataModel
	{
		public override void initFirstTime()
		{
		}

		public override void loadFromFireBase()
		{
			
		}

		public VipGiftDefine.StackGift[] stackGifts;

		[Serializable]
		public class StackGift
		{
			public void onReward()
			{
				foreach (Item item in this.items)
				{
					item.reward(1);
				}
			}

			public string name;

			public Item[] items;
		}
	}
}
