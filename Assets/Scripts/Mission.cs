using System;
using UnityEngine;

[Serializable]
public class Mission
{
	public string getDetail()
	{
		if (this.detail.Contains("#VALUE"))
		{
			return this.detail.Replace("#VALUE", this.number + string.Empty);
		}
		return this.detail;
	}

	public string code;

	public string detail;

	public Sprite icon;

	public int number;

	public GiftSlot.Gift gift;
}
