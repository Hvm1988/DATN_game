using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemInfoPop : MonoBehaviour
{
	public abstract void init(ItemInven item);

	public abstract void sell();

	public virtual void setUI(NItem item = null)
	{
		this.icon.sprite = item.icon;
		this.name.text = item.name;
		switch (item.color)
		{
		case ItemColor.WHITE:
			this.color.text = "Common";
			this.color.color = Color.white;
			this.name.color = Color.white;
			break;
		case ItemColor.GREEN:
			this.color.text = "Uncommon";
			this.color.color = Color.green;
			this.name.color = Color.green;
			break;
		case ItemColor.BLUE:
			this.color.text = "Rare";
			this.color.color = Color.blue;
			this.name.color = Color.blue;
			break;
		case ItemColor.PINK:
			this.color.text = "Mythical";
			this.color.color = Color.magenta;
			this.name.color = Color.magenta;
			break;
		case ItemColor.YELLOW:
			this.color.text = "Imortal";
			this.color.color = Color.yellow;
			this.name.color = Color.yellow;
			break;
		case ItemColor.RED:
			this.color.text = "Legendary";
			this.color.color = Color.red;
			this.name.color = Color.red;
			break;
		}
		if (this.sellValue != null)
		{
			this.sellValue.text = item.getSellValue() + string.Empty;
		}
		this.des.text = item.getDes();
	}

	public Image icon;

	public new Text name;

	public Text color;

	public Text des;

	public Text sellValue;

	public Button sellBtn;

	public Button gotBtn;

	public ItemInven item;
}
