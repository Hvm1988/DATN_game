using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MainItem : NItem
{
	public MainItem(MainItem another) : base(another)
	{
		this.itemType = another.itemType;
		this.optionItem = another.optionItem;
	}

	public MainItemType getItemType()
	{
		return this.itemType;
	}

	[SerializeField]
	private MainItemType itemType;

	[SerializeField]
	public List<OptionItem> optionItem = new List<OptionItem>();
}
