using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory
{
	public static MainItemInven makeAMainItem(string code)
	{
		MainItemInven mainItemInven = new MainItemInven();
		mainItemInven.code = code;
		PlayerPrefs.SetInt("MAINITEM_ID", PlayerPrefs.GetInt("MAINITEM_ID") + 1);
		mainItemInven.key = mainItemInven.code + "|" + PlayerPrefs.GetInt("MAINITEM_ID");
		return mainItemInven;
	}

	public static ResourceItemInven makeAResItem(string code, int number)
	{
		ResourceItemInven resourceItemInven = new ResourceItemInven();
		resourceItemInven.code = code;
		PlayerPrefs.SetInt("RESITEM_ID", PlayerPrefs.GetInt("RESITEM_ID") + 1);
		resourceItemInven.key = resourceItemInven.code + "|" + PlayerPrefs.GetInt("RESITEM_ID");
		resourceItemInven.number = number;
		return resourceItemInven;
	}

	public static ResourceItemInven makeAResItem(int index, int number)
	{
		string code = DataHolder.Instance.mainItemsDefine.resourceItem[index].code;
		ResourceItemInven resourceItemInven = new ResourceItemInven();
		resourceItemInven.code = code;
		PlayerPrefs.SetInt("RESITEM_ID", PlayerPrefs.GetInt("RESITEM_ID") + 1);
		resourceItemInven.key = resourceItemInven.code + "|" + PlayerPrefs.GetInt("RESITEM_ID");
		resourceItemInven.number = number;
		return resourceItemInven;
	}

	public static AttritionItemInven makeAAttrItem(string code, int number)
	{
		AttritionItemInven attritionItemInven = new AttritionItemInven();
		attritionItemInven.code = code;
		PlayerPrefs.SetInt("ATTRITEM_ID", PlayerPrefs.GetInt("ATTRITEM_ID") + 1);
		attritionItemInven.key = attritionItemInven.code + "|" + PlayerPrefs.GetInt("ATTRITEM_ID");
		attritionItemInven.number = number;
		return attritionItemInven;
	}

	public static ResourceItemInven makeASellRes(string key, string code, int number)
	{
		return new ResourceItemInven
		{
			key = key,
			code = code,
			number = number
		};
	}

	public static AttritionItemInven makeASellAtt(string key, string code, int number)
	{
		return new AttritionItemInven
		{
			key = key,
			code = code,
			number = number
		};
	}

	public static ScrollItemInven makeAScrollItem(string code)
	{
		ScrollItemInven scrollItemInven = new ScrollItemInven();
		scrollItemInven.code = code;
		PlayerPrefs.SetInt("SCROLLITEM_ID", PlayerPrefs.GetInt("SCROLLITEM_ID") + 1);
		scrollItemInven.key = scrollItemInven.code + "|" + PlayerPrefs.GetInt("SCROLLITEM_ID");
		return scrollItemInven;
	}

	public static ScrollItemInven makeAScrollItem(int index)
	{
		string code = DataHolder.Instance.mainItemsDefine.scrollItems[index].code;
		ScrollItemInven scrollItemInven = new ScrollItemInven();
		scrollItemInven.code = code;
		PlayerPrefs.SetInt("SCROLLITEM_ID", PlayerPrefs.GetInt("SCROLLITEM_ID") + 1);
		scrollItemInven.key = scrollItemInven.code + "|" + PlayerPrefs.GetInt("SCROLLITEM_ID");
		return scrollItemInven;
	}

	public static void init(MainItemsDefine mainItemsDefine)
	{
		ItemFactory.itemDefine = mainItemsDefine;
	}

	public static ResourceItemInven makeRandomResItem(ItemColor minColor, ItemColor maxColor)
	{
		string code = string.Empty;
		List<string> list = new List<string>();
		for (int i = 0; i < DataHolder.Instance.mainItemsDefine.resourceItem.Count; i++)
		{
			if (DataHolder.Instance.mainItemsDefine.resourceItem[i].color <= maxColor && DataHolder.Instance.mainItemsDefine.resourceItem[i].color >= minColor)
			{
				list.Add(DataHolder.Instance.mainItemsDefine.resourceItem[i].code);
			}
		}
		code = list[UnityEngine.Random.Range(0, list.Count)];
		return ItemFactory.makeAResItem(code, 50);
	}

	public static ScrollItemInven makeRandomScrollItem(ItemColor minColor, ItemColor maxColor)
	{
		string code = string.Empty;
		List<string> list = new List<string>();
		for (int i = 0; i < DataHolder.Instance.mainItemsDefine.scrollItems.Count; i++)
		{
			if (DataHolder.Instance.mainItemsDefine.scrollItems[i].color <= maxColor && DataHolder.Instance.mainItemsDefine.scrollItems[i].color >= minColor)
			{
				list.Add(DataHolder.Instance.mainItemsDefine.scrollItems[i].code);
			}
		}
		code = list[UnityEngine.Random.Range(0, list.Count)];
		return ItemFactory.makeAScrollItem(code);
	}

	public static MainItemsDefine itemDefine;
}
