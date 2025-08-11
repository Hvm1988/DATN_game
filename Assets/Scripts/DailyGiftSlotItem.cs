using System;
using Common;
using UnityEngine;
using UnityEngine.UI;

public class DailyGiftSlotItem : SlotItem
{
	public void onClick()
	{
	}

	public new void init(Item item, ItemDefine itemDefine)
	{
		base.init(item, itemDefine);
		this.item = item;
		this.rewarded.SetActive(false);
	}

	public void disable()
	{
		this.bg.color = new Color(1f, 1f, 1f, 0.8f);
		this.day.color = new Color(1f, 1f, 1f, 0.8f);
		this.icon.color = new Color(1f, 1f, 1f, 0.8f);
		this.rewarded.SetActive(true);
		this.boderEffect.SetActive(false);
	}

	public void hightLight()
	{
		this.bg.sprite = this.light;
		this.boderEffect.SetActive(true);
	}

	public Sprite dark;

	public Sprite light;

	public Image bg;

	public Text day;

	public int id;

	public Item item;

	public GameObject boderEffect;

	public GameObject rewarded;
}
