using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemEat : MonoBehaviour
{
	public void onShow(string codeItem)
	{
		this.imgItem.sprite = DataHolder.Instance.mainItemsDefine.getScrollByCode(codeItem).productIcon;
		this.imgColor.sprite = DataHolder.Instance.mainItemsDefine.getScrollByCode(codeItem).icon;
	}

	public Image imgColor;

	public Image imgItem;
}
