using System;
using System.Collections.Generic;

internal class ItemDictonary
{
	public static Dictionary<string, int> getInstance()
	{
		if (ItemDictonary.itemDictionary == null)
		{
			ItemDictonary.itemDictionary = new Dictionary<string, int>();
			ItemDictonary.itemDictionary.Add("VAI-THO", 0);
			ItemDictonary.itemDictionary.Add("THAO-MOC", 1);
			ItemDictonary.itemDictionary.Add("DA-THU", 2);
			ItemDictonary.itemDictionary.Add("LONG-VU", 3);
			ItemDictonary.itemDictionary.Add("GO-TRON", 4);
			ItemDictonary.itemDictionary.Add("DA-VU-TRU", 5);
			ItemDictonary.itemDictionary.Add("CAT", 6);
			ItemDictonary.itemDictionary.Add("DA-TINH-ANH", 7);
			ItemDictonary.itemDictionary.Add("MAI-RUA", 8);
			ItemDictonary.itemDictionary.Add("DRAGON-ENERGY", 9);
			ItemDictonary.itemDictionary.Add("LINHHON-NAMEX", 10);
			ItemDictonary.itemDictionary.Add("RANGNANH-KHIDOT", 11);
		}
		return ItemDictonary.itemDictionary;
	}

	private static Dictionary<string, int> itemDictionary;
}
