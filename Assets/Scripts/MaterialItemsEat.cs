using System;
using UnityEngine;
using UnityEngine.UI;

public class MaterialItemsEat : MonoBehaviour
{
	public void show(int index, int num)
	{
		this.img.sprite = DataHolder.Instance.mainItemsDefine.resourceItem[index].icon;
		this.numItems.text = "+" + num.ToString();
	}

	public Image img;

	public Text numItems;
}
