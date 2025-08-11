using System;
using System.Diagnostics;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	public static InventoryManager Instance
	{
		get
		{
			if (InventoryManager.instance == null)
			{
				InventoryManager.instance = UnityEngine.Object.FindObjectOfType<InventoryManager>();
			}
			return InventoryManager.instance;
		}
	}

	public void showMainItemInfoPop(MainItemInven item)
	{
		if (item == null)
		{
			return;
		}
		if (item.code.Equals(string.Empty))
		{
			return;
		}
		this.mainItemInfoPop.init(item);
		this.mainItemInfoPop.gameObject.SetActive(true);
	}

	public void showResItemInfoPop(ResourceItemInven item)
	{
		if (item == null)
		{
			return;
		}
		if (item.code.Equals(string.Empty))
		{
			return;
		}
		this.resItemInfoPop.init(item);
		this.resItemInfoPop.gameObject.SetActive(true);
	}

	public void showScrollItemInfoPop(ScrollItemInven item)
	{
		if (item == null)
		{
			return;
		}
		if (item.code.Equals(string.Empty))
		{
			return;
		}
		this.scrollItemInfoPop.init(item);
		this.scrollItemInfoPop.gameObject.SetActive(true);
	}

	public void showAttritionItemInfoPop(AttritionItemInven item)
	{
		if (item == null)
		{
			return;
		}
		if (item.code.Equals(string.Empty))
		{
			return;
		}
		this.attrItemInfoPop.init(item);
		this.attrItemInfoPop.gameObject.SetActive(true);
	}

	public void showSellPop(ItemInven item)
	{
		this.sellPop.init(item);
		this.sellPop.gameObject.SetActive(true);
	}

	public static event InventoryManager.OnRefresh onRefresh;

	public void refresh()
	{
		if (InventoryManager.onRefresh != null)
		{
			InventoryManager.onRefresh(null);
		}
	}

	private static InventoryManager instance;

	public MainItemInfoPop mainItemInfoPop;

	public ResItemInfoPop resItemInfoPop;

	public ScrollItemInfoPop scrollItemInfoPop;

	public AttritionInfoPop attrItemInfoPop;

	public SellPop sellPop;

	public delegate void OnRefresh(NItem item = null);
}
