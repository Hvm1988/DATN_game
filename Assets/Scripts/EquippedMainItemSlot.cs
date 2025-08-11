using System;
using UnityEngine;
using UnityEngine.UI;

public class EquippedMainItemSlot : MonoBehaviour
{
	private void OnEnable()
	{
		this.setUI(null);
		InventoryManager.onRefresh += this.setUI;
	}

	private void OnDisable()
	{
		InventoryManager.onRefresh -= this.setUI;
	}

	public void setUI(NItem i = null)
	{
		MainItemInven itemByType = DataHolder.Instance.playerData.getItemByType(this.type);
		if (itemByType == null)
		{
			this.icon.sprite = this.emptySprite;
			this.levelObject.SetActive(false);
		}
		else
		{
			MainItem mainByCode = DataHolder.Instance.mainItemsDefine.getMainByCode(itemByType.code);
			this.levelObject.SetActive(true);
			this.icon.sprite = mainByCode.icon;
			this.level.text = itemByType.getLevelString();
		}
	}

	public void onClick()
	{
		InventoryManager.Instance.showMainItemInfoPop(DataHolder.Instance.playerData.getItemByType(this.type));
	}

	public Image icon;

	public MainItemType type;

	public Text level;

	public Sprite emptySprite;

	public GameObject levelObject;
}
