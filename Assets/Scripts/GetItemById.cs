using System;
using System.Collections.Generic;

internal class GetItemById
{
	public static Dictionary<int, string> getInstance()
	{
		if (GetItemById.itemDictionary == null)
		{
			GetItemById.itemDictionary = new Dictionary<int, string>();
			GetItemById.itemDictionary.Add(1, "VAI-THO");
			GetItemById.itemDictionary.Add(2, "THAO-MOC");
			GetItemById.itemDictionary.Add(3, "DA-THU");
			GetItemById.itemDictionary.Add(4, "LONG-VU");
			GetItemById.itemDictionary.Add(5, "GO-TRON");
			GetItemById.itemDictionary.Add(6, "DA-VU-TRU");
			GetItemById.itemDictionary.Add(7, "CAT");
			GetItemById.itemDictionary.Add(8, "DA-TINH-ANH");
			GetItemById.itemDictionary.Add(9, "MAI-RUA");
			GetItemById.itemDictionary.Add(10, "DRAGON-ENERGY");
			GetItemById.itemDictionary.Add(11, "LINHHON-NAMEX");
			GetItemById.itemDictionary.Add(12, "RANGNANH-KHIDOT");
		}
		return GetItemById.itemDictionary;
	}

	private static Dictionary<int, string> itemDictionary;
}
