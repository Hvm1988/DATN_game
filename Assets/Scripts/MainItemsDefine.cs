using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game Data/Item Define")]
public class MainItemsDefine : ScriptableObject
{
	public ResourceItem getResByCode(string code)
	{
		foreach (ResourceItem resourceItem in this.resourceItem)
		{
			if (resourceItem.code.Equals(code))
			{
				return resourceItem;
			}
		}
		return null;
	}

	public MainItem getMainByCode(string code)
	{
		foreach (MainItem mainItem in this.mainItem)
		{
			if (mainItem.code.Equals(code))
			{
				return mainItem;
			}
		}
		return null;
	}

	public ScrollItem getScrollByCode(string code)
	{
		foreach (ScrollItem scrollItem in this.scrollItems)
		{
			if (scrollItem.code.Equals(code))
			{
				return scrollItem;
			}
		}
		return null;
	}

	public AttritionItem getAttrByCode(string code)
	{
		foreach (AttritionItem attritionItem in this.attritionItems)
		{
			if (attritionItem.code.Equals(code))
			{
				return attritionItem;
			}
		}
		return null;
	}

	public int getIDScrollByCode(string coce)
	{
		for (int i = 0; i < this.scrollItems.Count; i++)
		{
			if (this.scrollItems[i].code.Equals(coce))
			{
				return i;
			}
		}
		return -1;
	}

	public NItem getRandomItemWithRange(ItemType type, ItemColor minColor, ItemColor maxColor)
	{
		List<NItem> list = new List<NItem>();
		if (type == ItemType.MAIN)
		{
			foreach (MainItem mainItem in this.mainItem)
			{
				if (mainItem.color >= minColor && mainItem.color <= maxColor)
				{
					list.Add(mainItem);
				}
			}
		}
		if (type == ItemType.RES)
		{
			foreach (ResourceItem resourceItem in this.resourceItem)
			{
				if (resourceItem.color >= minColor && resourceItem.color <= maxColor)
				{
					list.Add(resourceItem);
				}
			}
		}
		if (type == ItemType.SCROLL)
		{
			foreach (ScrollItem scrollItem in this.scrollItems)
			{
				if (scrollItem.color >= minColor && scrollItem.color <= maxColor)
				{
					list.Add(scrollItem);
				}
			}
		}
		if (type == ItemType.ATTRITION)
		{
			foreach (AttritionItem attritionItem in this.attritionItems)
			{
				if (attritionItem.color >= minColor && attritionItem.color <= maxColor)
				{
					list.Add(attritionItem);
				}
			}
		}
		if (list.Count > 0)
		{
			return list[UnityEngine.Random.Range(0, list.Count)];
		}
		return null;
	}

	public List<MainItem> mainItem;

	public List<ResourceItem> resourceItem;

	public List<ScrollItem> scrollItems;

	public List<AttritionItem> attritionItems;
}
