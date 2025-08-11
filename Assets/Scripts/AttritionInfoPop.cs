using System;
using UnityEngine.UI;

public class AttritionInfoPop : ItemInfoPop
{
	public override void init(ItemInven item)
	{
		this.item = item;
		this._item = DataHolder.Instance.mainItemsDefine.getAttrByCode(this.item.code);
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
		this.numberSellText.text = "1/" + ((AttritionItemInven)this.item).number;
		this.sellSlider.minValue = 1f;
		this.sellSlider.maxValue = (float)((AttritionItemInven)this.item).number;
		this.sellSlider.value = 1f;
	}

	public override void sell()
	{
		AttritionItemInven item = ItemFactory.makeASellAtt(this.item.key, this._item.code, this.sellNumber);
		InventoryManager.Instance.showSellPop(item);
		base.gameObject.SetActive(false);
	}

	public void onSellSliderChange(float value)
	{
		this.sellNumber = (int)this.sellSlider.value;
		this.numberSellText.text = this.sellNumber + "/" + ((AttritionItemInven)this.item).number;
		this.sellValue.text = "Sell for :" + this._item.getSell() * this.sellNumber;
	}

	public int sellNumber;

	public Text numberSellText;

	private AttritionItem _item;

	public Slider sellSlider;
}
