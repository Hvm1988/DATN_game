using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScrollItem : NItem
{
	public ScrollItem(ScrollItem another) : base(another)
	{
		this.costRes = another.costRes;
		this.productIcon = another.productIcon;
		this.codeProduct = another.codeProduct;
		this.goldCost = another.goldCost;
		this.rubyCost = another.rubyCost;
	}

	public List<ScrollItem.ItemCostResource> getCostRes()
	{
		return this.costRes;
	}

	[SerializeField]
	private List<ScrollItem.ItemCostResource> costRes = new List<ScrollItem.ItemCostResource>();

	public Sprite productIcon;

	public string codeProduct;

	public int goldCost;

	public int rubyCost;

	[Serializable]
	public class ItemCostResource
	{
		public string code;

		public int cost;
	}
}
