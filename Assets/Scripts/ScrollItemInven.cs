using System;
using System.Collections.Generic;

[Serializable]
public class ScrollItemInven : ItemInven
{
	public void craft()
	{
		if (this.canCraft())
		{
			ScrollItem scrollByCode = DataHolder.Instance.mainItemsDefine.getScrollByCode(this.code);
			List<ScrollItem.ItemCostResource> costRes = scrollByCode.getCostRes();
			foreach (ScrollItem.ItemCostResource itemCostResource in costRes)
			{
				DataHolder.Instance.inventory.minusResourceItem(itemCostResource.code, itemCostResource.cost);
			}
			DataHolder.Instance.inventory.scrollItems.Remove(this);
			DataHolder.Instance.inventory.pickUpAItem(ItemFactory.makeAMainItem(scrollByCode.codeProduct));
			return;
		}
		throw new Exception("NOT_ENOUGHT_RES");
	}

	private bool canCraft()
	{
		List<ScrollItem.ItemCostResource> costRes = DataHolder.Instance.mainItemsDefine.getScrollByCode(this.code).getCostRes();
		foreach (ScrollItem.ItemCostResource itemCostResource in costRes)
		{
			if (itemCostResource.cost > DataHolder.Instance.inventory.getAllResByCode(itemCostResource.code))
			{
				return false;
			}
		}
		return true;
	}
}
