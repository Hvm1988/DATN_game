using System;
using UnityEngine;

public class NItem
{
	public NItem()
	{
	}

	public NItem(NItem another)
	{
		this.name = another.name;
		this.code = another.code;
		this.color = another.color;
		this.setDes(another.getDes());
		this.setSellValue(another.getSell());
		this.icon = another.icon;
	}

	public string getSellValue()
	{
		return "Sell for: " + this.sellValue;
	}

	public string getDes()
	{
		return this.description;
	}

	public void setDes(string des)
	{
		this.description = des;
	}

	public int getSell()
	{
		return this.sellValue;
	}

	public void setSellValue(int value)
	{
		this.sellValue = value;
	}

	public string code;

	public string name;

	public ItemColor color;

	[SerializeField]
	public Sprite icon;

	[SerializeField]
	private string description;

	[SerializeField]
	private int sellValue;
}
