using System;
using UnityEngine.UI;

public class MainItemInfoInLibary : ItemInfoInLibary
{
	public override void setUI(NItem _item = null)
	{
		this.mainItem = (MainItem)_item;
		base.setUI(this.mainItem);
		for (int i = 0; i < this.opDes.Length; i++)
		{
			if (i < this.mainItem.optionItem.Count)
			{
				this.opDes[i].enabled = true;
				this.opDes[i].text = this.mainItem.optionItem[i].getOpDesRichText(0);
			}
			else
			{
				this.opDes[i].enabled = false;
			}
		}
	}

	public Text[] opDes;

	public MainItem mainItem;
}
