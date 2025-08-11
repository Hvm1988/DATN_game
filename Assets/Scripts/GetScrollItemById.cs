using System;
using System.Collections.Generic;

public class GetScrollItemById
{
	public static Dictionary<int, string> getInstance()
	{
		if (GetScrollItemById.scrollItem == null)
		{
			GetScrollItemById.scrollItem = new Dictionary<int, string>();
			GetScrollItemById.scrollItem.Add(1, "ARMOR-SOKU-GREEN-SCROLL");
			GetScrollItemById.scrollItem.Add(2, "ARMOR-SOKU-BLUE-SCROLL");
			GetScrollItemById.scrollItem.Add(3, "ARMOR-SOKU-PINK-SCROLL");
			GetScrollItemById.scrollItem.Add(4, "ARMOR-SOKU-YELLOW-SCROLL");
			GetScrollItemById.scrollItem.Add(5, "ARMOR-SOKU-RED-SCROLL");
			GetScrollItemById.scrollItem.Add(6, "SHOE-SOKU-GREEN-SCROLL");
			GetScrollItemById.scrollItem.Add(7, "SHOE-SOKU-BLUE-SCROLL");
			GetScrollItemById.scrollItem.Add(8, "SHOE-SOKU-PINK-SCROLL");
			GetScrollItemById.scrollItem.Add(9, "SHOE-SOKU-YELLOW-SCROLL");
			GetScrollItemById.scrollItem.Add(10, "SHOE-SOKU-RED-SCROLL");
			GetScrollItemById.scrollItem.Add(11, "RING-SOKU-GREEN-SCROLL");
			GetScrollItemById.scrollItem.Add(12, "RING-SOKU-BLUE-SCROLL");
			GetScrollItemById.scrollItem.Add(13, "RING-SOKU-PINK-SCROLL");
			GetScrollItemById.scrollItem.Add(14, "RING-SOKU-YELLOW-SCROLL");
			GetScrollItemById.scrollItem.Add(15, "RING-SOKU-RED-SCROLL");
			GetScrollItemById.scrollItem.Add(16, "AMULET-SOKU-GREEN-SCROLL");
			GetScrollItemById.scrollItem.Add(17, "AMULET-SOKU-BLUE-SCROLL");
			GetScrollItemById.scrollItem.Add(18, "AMULET-SOKU-PINK-SCROLL");
			GetScrollItemById.scrollItem.Add(19, "AMULET-SOKU-YELLOW-SCROLL");
			GetScrollItemById.scrollItem.Add(20, "AMULET-SOKU-RED-SCROLL");
			GetScrollItemById.scrollItem.Add(21, "PANTS-SOKU-GREEN-SCROLL");
			GetScrollItemById.scrollItem.Add(22, "PANTS-SOKU-BLUE-SCROLL");
			GetScrollItemById.scrollItem.Add(23, "PANTS-SOKU-PINK-SCROLL");
			GetScrollItemById.scrollItem.Add(24, "PANTS-SOKU-YELLOW-SCROLL");
			GetScrollItemById.scrollItem.Add(25, "PANTS-SOKU-RED-SCROLL");
			GetScrollItemById.scrollItem.Add(26, "GLOVER-SOKU-GREEN-SCROLL");
			GetScrollItemById.scrollItem.Add(27, "GLOVER-SOKU-BLUE-SCROLL");
			GetScrollItemById.scrollItem.Add(28, "GLOVER-SOKU-PINK-SCROLL");
			GetScrollItemById.scrollItem.Add(29, "GLOVER-SOKU-YELLOW-SCROLL");
			GetScrollItemById.scrollItem.Add(30, "GLOVER-SOKU-RED-SCROLL");
		}
		return GetScrollItemById.scrollItem;
	}

	private static Dictionary<int, string> scrollItem;
}
