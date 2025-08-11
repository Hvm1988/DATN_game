using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoInLibary : MonoBehaviour
{
	public virtual void setUI(NItem item)
	{
		this.iconImg.sprite = item.icon;
		this.nameTxt.text = item.name;
		switch (item.color)
		{
		case ItemColor.WHITE:
			this.colorTxt.text = "Common";
			this.colorTxt.color = Color.white;
			this.nameTxt.color = Color.white;
			break;
		case ItemColor.GREEN:
			this.colorTxt.text = "Uncommon";
			this.colorTxt.color = Color.green;
			this.nameTxt.color = Color.green;
			break;
		case ItemColor.BLUE:
			this.colorTxt.text = "Rare";
			this.colorTxt.color = Color.blue;
			this.nameTxt.color = Color.blue;
			break;
		case ItemColor.PINK:
			this.colorTxt.text = "Mythical";
			this.colorTxt.color = Color.magenta;
			this.nameTxt.color = Color.magenta;
			break;
		case ItemColor.YELLOW:
			this.colorTxt.text = "Imortal";
			this.colorTxt.color = Color.yellow;
			this.nameTxt.color = Color.yellow;
			break;
		case ItemColor.RED:
			this.colorTxt.text = "Legendary";
			this.colorTxt.color = Color.red;
			this.nameTxt.color = Color.red;
			break;
		}
		this.desTxt.text = item.getDes();
	}

	public Image iconImg;

	public Text nameTxt;

	public Text colorTxt;

	public Text desTxt;
}
