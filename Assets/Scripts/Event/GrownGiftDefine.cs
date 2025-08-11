using System;
using Common;
using UnityEngine;

namespace Event
{
	[CreateAssetMenu(fileName = "GrownGiftDefine", menuName = "Game Data/GrownGiftDefine")]
	public class GrownGiftDefine : DataModel
	{
		public override void initFirstTime()
		{
		}

		public override void loadFromFireBase()
		{
			
		}

		public GrownGiftDefine.GrownGift[] growGifts;

		[Serializable]
		public class GrownGift
		{
			public int level;

			public Item item;
		}
	}
}
