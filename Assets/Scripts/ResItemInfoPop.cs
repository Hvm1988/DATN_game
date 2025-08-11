using System;
using UnityEngine.UI;

public class ResItemInfoPop : ItemInfoPop
{
	public override void init(ItemInven item)
	{
		this.item = item;
		this._item = DataHolder.Instance.mainItemsDefine.getResByCode(this.item.code);
		this.setUI(this._item);
	}

	private void OnEnable()
	{
		InventoryManager.onRefresh += this.setUI;
	}

	private void OnDisable()
	{
		InventoryManager.onRefresh -= this.setUI;
	}

	public override void setUI(NItem NI = null)
	{
		base.setUI(this._item);
		this.sellNumber = 1;
		if (this.item.GetType() == typeof(ResourceItemInven))
		{
			this.numberSellText.text = "1/" + ((ResourceItemInven)this.item).number;
			this.sellSlider.minValue = 1f;
			this.sellSlider.maxValue = (float)((ResourceItemInven)this.item).number;
		}
		this.sellSlider.value = 1f;
	}

	public override void sell()
	{
		if (this.item.GetType() == typeof(ResourceItemInven))
		{
			ResourceItemInven item = ItemFactory.makeASellRes(this.item.key, this._item.code, this.sellNumber);
			InventoryManager.Instance.showSellPop(item);
		}
		base.gameObject.SetActive(false);
	}

	public void onSellSliderChange(float value)
	{
		this.sellNumber = (int)value;
		this.numberSellText.text = this.sellNumber + "/" + ((ResourceItemInven)this.item).number;
		this.sellValue.text = "Sell for :" + this._item.getSell() * this.sellNumber;
	}

	public int sellNumber;

	public Text numberSellText;

	private ResourceItem _item;

	public Slider sellSlider;
}
