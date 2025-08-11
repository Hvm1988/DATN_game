using System;
using Common;
using UnityEngine;

namespace Event
{
	[CreateAssetMenu(fileName = "DailyGiftData", menuName = "Game Data/DailyGiftData")]
	public class DailyGiftDefine : DataModel
	{
		public override void initFirstTime()
		{
		}

		public override void loadFromFireBase()
		{
		}

		public Item[] items;
	}
}
