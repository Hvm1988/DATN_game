using System;
using UnityEngine;

public class Extend : MonoBehaviour
{
	public void init(int type)
	{
		this.type = (TypeExtend)type;
	}

	public void buySlot(int number)
	{
		int num = 80;
		if (number == 50)
		{
			num = 300;
		}
		try
		{
			DataHolder.Instance.playerData.addRuby(-num);
			DataHolder.Instance.inventory.extendSlot(this.type, number);
			InventoryManager.Instance.refresh();
			base.gameObject.SetActive(false);
		}
		catch (Exception ex)
		{
			if (ex.Message.Equals("NOT_ENOUGHT_RUBY"))
			{
				this.am.notEnougtRuby.SetActive(true);
			}
		}
	}

	public TypeExtend type;

	public AccesspriesManager am;
}
