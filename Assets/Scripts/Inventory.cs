using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Game Data/Inventory")]
public class Inventory : DataModel
{
	public override void initFirstTime()
	{
		this.mainItems = new List<MainItemInven>();
		this.resourceItems = new List<ResourceItemInven>();
		this.scrollItems = new List<ScrollItemInven>();
		this.attritionItems = new List<AttritionItemInven>();
		this.currentOpenSlotAttrition = 19;
		this.currentOpenSlotMainItem = 19;
		this.currentOpenSlotResource = 19;
		this.freeKey = new int[3];
		this.pickUpAItem(ItemFactory.makeAMainItem("ARMOR-SOKU-WHITE"));
		this.pickUpAItem(ItemFactory.makeAMainItem("RING-SOKU-WHITE"));
		this.pickUpAItem(ItemFactory.makeAMainItem("AMULET-SOKU-WHITE"));
		this.pickUpAItem(ItemFactory.makeAMainItem("SHOE-SOKU-WHITE"));
		this.pickUpAItem(ItemFactory.makeAMainItem("PANTS-SOKU-WHITE"));
		this.pickUpAItem(ItemFactory.makeAMainItem("GLOVER-SOKU-WHITE"));
	}

	public override void loadFromFireBase()
	{
		throw new NotImplementedException();
	}

	public void addFreeKey(int id, int value)
	{
		this.freeKey[id] = value;
		this.save();
	}

	public bool hasFreeKey()
	{
		foreach (int num in this.freeKey)
		{
			if (num == 1)
			{
				return true;
			}
		}
		return false;
	}

	public void extendSlot(TypeExtend type, int number)
	{
		if (type != TypeExtend.MAIN)
		{
			if (type != TypeExtend.MATERIAL)
			{
				if (type == TypeExtend.ATTRITION)
				{
					this.currentOpenSlotAttrition += number;
					if (this.currentOpenSlotAttrition >= this.maxSlotMainItem)
					{
						this.currentOpenSlotAttrition = this.maxSlotMainItem;
					}
				}
			}
			else
			{
				this.currentOpenSlotResource += number;
				if (this.currentOpenSlotResource >= this.maxSlotMainItem)
				{
					this.currentOpenSlotResource = this.maxSlotMainItem;
				}
			}
		}
		else
		{
			this.currentOpenSlotMainItem += number;
			if (this.currentOpenSlotMainItem >= this.maxSlotMainItem)
			{
				this.currentOpenSlotMainItem = this.maxSlotMainItem;
			}
		}
		this.save();
	}

	public void pickUpAItem(ItemInven item)
	{
		if (item.GetType() == typeof(MainItemInven) && this.getFreeSlotMain() > 0)
		{
			this.mainItems.Add((MainItemInven)item);
		}
		if (item.GetType() == typeof(ResourceItemInven))
		{
			this.addToResourceItem((ResourceItemInven)item);
		}
		if (item.GetType() == typeof(ScrollItemInven) && this.getFreeSlotResource() > 0)
		{
			this.scrollItems.Add((ScrollItemInven)item);
		}
		if (item.GetType() == typeof(AttritionItemInven))
		{
			this.addToAttriItem((AttritionItemInven)item);
		}
		this.save();
	}

	private void addToResourceItem(ResourceItemInven item)
	{
		int lastResourceItem = this.getLastResourceItem(item.code);
		int num = item.number;
		if (lastResourceItem != -1)
		{
			num = item.number + this.resourceItems[lastResourceItem].number - 100;
			if (num <= 0)
			{
				this.resourceItems[lastResourceItem].number += item.number;
				return;
			}
			this.resourceItems[lastResourceItem].number = 100;
		}
		int num2 = num / 100;
		for (int i = 0; i < num2; i++)
		{
			if (this.getFreeSlotResource() > 0)
			{
				this.resourceItems.Add(ItemFactory.makeAResItem(item.code.Split(new char[]
				{
					'|'
				})[0], 100));
			}
		}
		int num3 = num - 100 * num2;
		if (num3 > 0 && this.getFreeSlotResource() > 0)
		{
			this.resourceItems.Add(ItemFactory.makeAResItem(item.code.Split(new char[]
			{
				'|'
			})[0], num3));
		}
	}

	public int getFreeSlotResource()
	{
		return this.currentOpenSlotResource - this.scrollItems.Count - this.resourceItems.Count;
	}

	public int getFreeSlotMain()
	{
		return this.currentOpenSlotMainItem - this.mainItems.Count;
	}

	private void addToAttriItem(AttritionItemInven item)
	{
		int lastAttrItem = this.getLastAttrItem(item.code);
		int num = item.number;
		if (lastAttrItem != -1)
		{
			num = item.number + this.attritionItems[lastAttrItem].number - 100;
			if (num <= 0)
			{
				this.attritionItems[lastAttrItem].number += item.number;
				return;
			}
			this.attritionItems[lastAttrItem].number = 100;
		}
		int num2 = num / 100;
		for (int i = 0; i < num2; i++)
		{
			this.attritionItems.Add(ItemFactory.makeAAttrItem(item.code.Split(new char[]
			{
				'|'
			})[0], 100));
		}
		int num3 = num - 100 * num2;
		if (num3 > 0)
		{
			this.attritionItems.Add(ItemFactory.makeAAttrItem(item.code.Split(new char[]
			{
				'|'
			})[0], num3));
		}
	}

	public void minusAttrItem(string code, int number)
	{
		int num = number;
		for (int i = this.attritionItems.Count - 1; i >= 0; i--)
		{
			if (this.attritionItems[i].code.Contains(code))
			{
				if (this.attritionItems[i].number > num)
				{
					this.attritionItems[i].number -= num;
					return;
				}
				num -= this.attritionItems[i].number;
				this.attritionItems[i].number = 0;
			}
		}
		this.clearAttrList();
		this.save();
	}

	private void removeFromResourceItem(ResourceItemInven item)
	{
		ResourceItemInven resbyKey = this.getResbyKey(item.key);
		if (resbyKey == null)
		{
			throw new Exception("NOT_EXIST");
		}
		resbyKey.number -= item.number;
		if (resbyKey.number == 0)
		{
			this.resourceItems.Remove(resbyKey);
		}
	}

	private void removeFromAttItem(AttritionItemInven item)
	{
		AttritionItemInven attbyKey = this.getAttbyKey(item.key);
		if (attbyKey == null)
		{
			throw new Exception("NOT_EXIST");
		}
		attbyKey.number -= item.number;
		if (attbyKey.number == 0)
		{
			this.attritionItems.Remove(attbyKey);
		}
	}

	public void minusResourceItem(string code, int number)
	{
		int num = number;
		for (int i = this.resourceItems.Count - 1; i >= 0; i--)
		{
			if (this.resourceItems[i].code.Contains(code))
			{
				if (this.resourceItems[i].number > num)
				{
					this.resourceItems[i].number -= num;
					return;
				}
				num -= this.resourceItems[i].number;
				this.resourceItems[i].number = 0;
			}
		}
	}

	public void clearResList()
	{
		for (int i = 0; i < this.resourceItems.Count; i++)
		{
			if (this.resourceItems[i].number == 0)
			{
				this.resourceItems.RemoveAt(i);
			}
		}
	}

	public void clearAttrList()
	{
		for (int i = 0; i < this.attritionItems.Count; i++)
		{
			if (this.attritionItems[i].number == 0)
			{
				this.attritionItems.RemoveAt(i);
			}
		}
	}

	private int getLastResourceItem(string code)
	{
		for (int i = 0; i < this.resourceItems.Count; i++)
		{
			if (this.resourceItems[i].code.Contains(code) && this.resourceItems[i].number < 100)
			{
				return i;
			}
		}
		return -1;
	}

	private int getLastAttrItem(string code)
	{
		for (int i = 0; i < this.attritionItems.Count; i++)
		{
			if (this.attritionItems[i].code.Contains(code) && this.attritionItems[i].number < 100)
			{
				return i;
			}
		}
		return -1;
	}

	private bool isFull()
	{
		return false;
	}

	public void sellAItem(ItemInven item)
	{
		if (item.GetType() == typeof(MainItemInven))
		{
			MainItemInven mainItemInven = (MainItemInven)item;
			MainItem mainByCode = DataHolder.Instance.mainItemsDefine.getMainByCode(mainItemInven.code);
			DataHolder.Instance.playerData.addGold(mainByCode.getSell());
			this.mainItems.Remove((MainItemInven)item);
		}
		if (item.GetType() == typeof(ScrollItemInven))
		{
			ScrollItemInven scrollItemInven = (ScrollItemInven)item;
			ScrollItem scrollByCode = DataHolder.Instance.mainItemsDefine.getScrollByCode(scrollItemInven.code);
			DataHolder.Instance.playerData.addGold(scrollByCode.getSell());
			this.scrollItems.Remove(scrollItemInven);
		}
		if (item.GetType() == typeof(ResourceItemInven))
		{
			ResourceItemInven resourceItemInven = (ResourceItemInven)item;
			ResourceItem resByCode = DataHolder.Instance.mainItemsDefine.getResByCode(resourceItemInven.code);
			DataHolder.Instance.playerData.addGold(resByCode.getSell());
			this.removeFromResourceItem(resourceItemInven);
		}
		if (item.GetType() == typeof(AttritionItemInven))
		{
			AttritionItemInven attritionItemInven = (AttritionItemInven)item;
			AttritionItem attrByCode = DataHolder.Instance.mainItemsDefine.getAttrByCode(attritionItemInven.code);
			DataHolder.Instance.playerData.addGold(attrByCode.getSell());
			this.removeFromAttItem(attritionItemInven);
		}
		this.save();
	}

	public MainItemInven getMainItemByKey(string key)
	{
		for (int i = 0; i < this.mainItems.Count; i++)
		{
			if (this.mainItems[i].key.Equals(key))
			{
				return this.mainItems[i];
			}
		}
		return null;
	}

	public ResourceItemInven getResbyKey(string key)
	{
		for (int i = 0; i < this.resourceItems.Count; i++)
		{
			if (this.resourceItems[i].key.Equals(key))
			{
				return this.resourceItems[i];
			}
		}
		return null;
	}

	public AttritionItemInven getAttbyKey(string key)
	{
		for (int i = 0; i < this.attritionItems.Count; i++)
		{
			if (this.attritionItems[i].key.Equals(key))
			{
				return this.attritionItems[i];
			}
		}
		return null;
	}

	public int getIDMainItemByCode(string code)
	{
		for (int i = 0; i < this.mainItems.Count; i++)
		{
			if (this.mainItems[i].code.Equals(code))
			{
				return i;
			}
		}
		return -1;
	}

	public int getAllResByCode(string code)
	{
		int num = 0;
		foreach (ResourceItemInven resourceItemInven in this.resourceItems)
		{
			if (resourceItemInven.code.Contains(code))
			{
				num += resourceItemInven.number;
			}
		}
		return num;
	}

	public int getAllAttrByCode(string code)
	{
		int num = 0;
		foreach (AttritionItemInven attritionItemInven in this.attritionItems)
		{
			if (attritionItemInven.code.Contains(code))
			{
				num += attritionItemInven.number;
			}
		}
		return num;
	}

	public List<MainItemInven> mainItems = new List<MainItemInven>();

	public List<ResourceItemInven> resourceItems = new List<ResourceItemInven>();

	public List<ScrollItemInven> scrollItems = new List<ScrollItemInven>();

	public List<AttritionItemInven> attritionItems = new List<AttritionItemInven>();

	public int maxSlotMainItem = 52;

	public int currentOpenSlotMainItem = 19;

	public int currentOpenSlotResource = 19;

	public int currentOpenSlotAttrition = 19;

	public int[] freeKey;
}
