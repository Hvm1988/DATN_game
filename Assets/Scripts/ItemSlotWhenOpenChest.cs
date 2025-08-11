using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotWhenOpenChest : MonoBehaviour
{
	public void init(ItemInven item, Sprite[] bgSprites)
	{
		if (item.GetType() == typeof(ResourceItemInven))
		{
			this.res.SetActive(true);
			this.scroll.SetActive(false);
			ResourceItem resByCode = DataHolder.Instance.mainItemsDefine.getResByCode(item.code);
			this.resIcon.sprite = resByCode.icon;
			this.numberes.text = "x" + ((ResourceItemInven)item).number;
			this.name.text = resByCode.name;
			this.bg.sprite = bgSprites[(int)resByCode.color];
		}
		else
		{
			this.res.SetActive(false);
			this.scroll.SetActive(true);
			ScrollItem scrollByCode = DataHolder.Instance.mainItemsDefine.getScrollByCode(item.code);
			this.scrollIcon.sprite = scrollByCode.icon;
			this.productIcon.sprite = scrollByCode.productIcon;
			this.name.text = scrollByCode.name;
			this.bg.sprite = bgSprites[(int)scrollByCode.color];
		}
	}

	public GameObject res;

	public GameObject scroll;

	public new Text name;

	public Image resIcon;

	public Text numberes;

	public Image scrollIcon;

	public Image productIcon;

	public Image bg;
}
