using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutOfSlotItem : MonoBehaviour
{
	public void init(OutOfSlotItem.TypeOut type, Action okCallback = null)
	{
		this.typeOut = type;
		this.content.text = OutOfSlotItem.contentTemplate[type];
		this.holder.SetActive(true);
		if (okCallback != null)
		{
			this.okBtn.onClick.AddListener(delegate()
			{
				okCallback();
			});
		}
	}

	public GameObject holder;

	public Text content;

	public Button okBtn;

	public OutOfSlotItem.TypeOut typeOut;

	public GameObject accessories;

	public Button callBackBtn;

	public GameObject shopPanel;

	public GameObject dailyPanel;

	public GameObject playPanel;

	private static Dictionary<OutOfSlotItem.TypeOut, string> contentTemplate = new Dictionary<OutOfSlotItem.TypeOut, string>
	{
		{
			OutOfSlotItem.TypeOut.SHOP,
			"You must free at least 1 material slot in your inventory to buy this items. "
		},
		{
			OutOfSlotItem.TypeOut.DAILY_RES,
			"You must free at least 1 material slot in your inventory to get this items. "
		},
		{
			OutOfSlotItem.TypeOut.DAILY_MAIN,
			"You must free at least 1 item slot in your inventory to get this items. "
		},
		{
			OutOfSlotItem.TypeOut.CRAFT,
			"You must free at least 1 item slot in your inventory to craft this items. "
		},
		{
			OutOfSlotItem.TypeOut.PLAY,
			"You must free at least 3 material slot in your inventory to pickup item what droped in game. "
		},
		{
			OutOfSlotItem.TypeOut.CHEST_X1,
			"You must free at least 1 material slot in your inventory to buy this items. "
		},
		{
			OutOfSlotItem.TypeOut.CHEST_X10,
			"You must free at least 10 material slot in your inventory to buy this items. "
		}
	};

	public enum TypeOut
	{
		SHOP,
		DAILY_RES,
		DAILY_MAIN,
		CRAFT,
		PLAY,
		CHEST_X1,
		CHEST_X10
	}
}
