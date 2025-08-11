using System;
using UnityEngine;
using UnityEngine.UI;

public class ChestManager : MonoBehaviour
{
	private void OnEnable()
	{
		this.setUI();
	}

	public void setUI()
	{
		for (int i = 0; i < this.keyCode.Length; i++)
		{
			int allAttrByCode = DataHolder.Instance.inventory.getAllAttrByCode(this.keyCode[i]);
			this.keyNumber[i].text = "x" + allAttrByCode;
			if (allAttrByCode > 0)
			{
				this.useKeyX1Btns[i].SetActive(true);
				this.freeX1Btns[i].SetActive(false);
				this.rubyX1Btns[i].SetActive(false);
				if (i <= 2 && this.countDowns[i] != null)
				{
					this.countDowns[i].text = string.Empty;
				}
			}
			else
			{
				this.useKeyX1Btns[i].SetActive(false);
				this.freeX1Btns[i].SetActive(false);
				this.rubyX1Btns[i].SetActive(true);
				if (i <= 2 && DataHolder.Instance.inventory.freeKey[i] == 1)
				{
					this.freeX1Btns[i].SetActive(true);
					this.rubyX1Btns[i].SetActive(false);
					if (i <= 2 && this.countDowns[i] != null)
					{
						this.countDowns[i].text = string.Empty;
					}
				}
			}
			if (allAttrByCode >= 10)
			{
				this.useKeyX10Btns[i].SetActive(true);
				this.rubyX10Btns[i].SetActive(false);
			}
			else
			{
				this.useKeyX10Btns[i].SetActive(false);
				this.rubyX10Btns[i].SetActive(true);
			}
		}
	}

	private void setChestImage(int type)
	{
		for (int i = 0; i < this.bodyImages.Length; i++)
		{
			this.bodyImages[i].sprite = this.bodyChests[type];
		}
		for (int j = 0; j < this.upImages.Length; j++)
		{
			this.upImages[j].sprite = this.upChests[type];
		}
	}

	public void useX1Key(int type)
	{
		if (TutorialManager.isTutorialing)
		{
			return;
		}
		if (DataHolder.Instance.inventory.getFreeSlotResource() == 0)
		{
			UIController.Instance.outSlotItem.init(OutOfSlotItem.TypeOut.CHEST_X1, null);
			return;
		}

		SoundManager.Instance.playAudio("ButtonClick");
		this.setChestImage(type);
		this.openChest.SetActive(true);
		ItemColor minColor = this.chestData.chests[type].minColor;
		ItemColor maxColor = this.chestData.chests[type].maxColor;
		int num = UnityEngine.Random.Range(1, 101);
		ItemInven item;
		if (num < 40)
		{
			item = ItemFactory.makeRandomResItem(minColor, maxColor);
		}
		else
		{
			item = ItemFactory.makeRandomScrollItem(minColor, maxColor);
		}
		this.x1Item.init(item, this.bgItemSprites);
		DataHolder.Instance.inventory.minusAttrItem(this.keyCode[type], 1);
		if (InventoryManager.Instance != null)
		{
			InventoryManager.Instance.refresh();
		}
		DataHolder.Instance.inventory.pickUpAItem(item);
		this.setUI();
		this.ccd.checkTime();
		TreasureNotifier.Instance.refresh();
		AccessoriesNotifier.Instance.refresh();
	}

	public void buyX1Key(int type)
	{
		this.setChestImage(type);
		if (DataHolder.Instance.inventory.getFreeSlotResource() == 0)
		{
			UIController.Instance.outSlotItem.init(OutOfSlotItem.TypeOut.CHEST_X1, null);
			return;
		}
		try
		{
			DataHolder.Instance.playerData.addRuby(-this.rubyX1[type]);
			this.openChest.SetActive(true);
			ItemColor minColor = this.chestData.chests[type].minColor;
			ItemColor maxColor = this.chestData.chests[type].maxColor;
			int num = UnityEngine.Random.Range(1, 101);
			ItemInven item;
			if (num < 40)
			{
				item = ItemFactory.makeRandomResItem(minColor, maxColor);
			}
			else
			{
				item = ItemFactory.makeRandomScrollItem(minColor, maxColor);
			}
			this.x1Item.init(item, this.bgItemSprites);
			DataHolder.Instance.inventory.pickUpAItem(item);
			SoundManager.Instance.playAudio("PayRuby");
			SoundManager.Instance.playAudio("Unlock");
			this.setUI();
			this.ccd.checkTime();
			TreasureNotifier.Instance.refresh();
			AccessoriesNotifier.Instance.refresh();

		}
		catch (Exception ex)
		{
			SoundManager.Instance.playAudio("ButtonClick");
			if (ex.Message.Equals("NOT_ENOUGHT_RUBY"))
			{
				this.am.notEnougtRuby.SetActive(true);
			}
		}
	}

	public void freex1(int type)
	{
		if (DataHolder.Instance.inventory.getFreeSlotResource() == 0)
		{
			UIController.Instance.outSlotItem.init(OutOfSlotItem.TypeOut.CHEST_X1, null);
			return;
		}
		SoundManager.Instance.playAudio("ButtonClick");
		this.setChestImage(type);
		this.openChest.SetActive(true);
		ItemColor minColor = this.chestData.chests[type].minColor;
		ItemColor maxColor = this.chestData.chests[type].maxColor;
		int num = UnityEngine.Random.Range(1, 101);
		ItemInven item;
		if (num < 40)
		{
			item = ItemFactory.makeRandomResItem(minColor, maxColor);
		}
		else
		{
			item = ItemFactory.makeRandomScrollItem(minColor, maxColor);
		}
		this.x1Item.init(item, this.bgItemSprites);
		DataHolder.Instance.inventory.pickUpAItem(item);
		DatePassHelper.saveNowToPref("COUNTDOWN-KEY" + (type + 1).ToString(), DatePassHelper.DateFormat.ddMMyyyyhhmmss);
		DataHolder.Instance.inventory.addFreeKey(type, 0);
		SoundManager.Instance.playAudio("Unlock");
		this.setUI();
		this.ccd.checkTime();
		TreasureNotifier.Instance.refresh();
		AccessoriesNotifier.Instance.refresh();

	}

	public void usex10Key(int type)
	{
		SoundManager.Instance.playAudio("ButtonClick");
		this.setChestImage(type);
		if (DataHolder.Instance.inventory.getFreeSlotResource() < 10)
		{
			UIController.Instance.outSlotItem.init(OutOfSlotItem.TypeOut.CHEST_X10, null);
			return;
		}
		this.openx10Chest.SetActive(true);
		for (int i = 0; i < 10; i++)
		{
			ItemColor minColor = this.chestData.chests[type].minColor;
			ItemColor maxColor = this.chestData.chests[type].maxColor;
			int num = UnityEngine.Random.Range(1, 101);
			ItemInven item;
			if (num < 40)
			{
				item = ItemFactory.makeRandomResItem(minColor, maxColor);
			}
			else
			{
				item = ItemFactory.makeRandomScrollItem(minColor, maxColor);
			}
			this.x10Items[i].init(item, this.bgItemSprites);
			DataHolder.Instance.inventory.pickUpAItem(item);
			DataHolder.Instance.inventory.minusAttrItem(this.keyCode[type], 10);
			if (InventoryManager.Instance != null)
			{
				InventoryManager.Instance.refresh();
			}
		}
		SoundManager.Instance.playAudio("Unlock");
		this.setUI();
		this.ccd.checkTime();
		TreasureNotifier.Instance.refresh();
		AccessoriesNotifier.Instance.refresh();

	}

	public void buyx10Key(int type)
	{
		this.setChestImage(type);
		if (DataHolder.Instance.inventory.getFreeSlotResource() < 10)
		{
			UIController.Instance.outSlotItem.init(OutOfSlotItem.TypeOut.CHEST_X10, null);
			return;
		}
		try
		{
			DataHolder.Instance.playerData.addRuby(-this.rubyX10[type]);
			this.openx10Chest.SetActive(true);
			for (int i = 0; i < 10; i++)
			{
				ItemColor minColor = this.chestData.chests[type].minColor;
				ItemColor maxColor = this.chestData.chests[type].maxColor;
				int num = UnityEngine.Random.Range(1, 101);
				ItemInven item;
				if (num < 40)
				{
					item = ItemFactory.makeRandomResItem(minColor, maxColor);
				}
				else
				{
					item = ItemFactory.makeRandomScrollItem(minColor, maxColor);
				}
				this.x10Items[i].init(item, this.bgItemSprites);
				DataHolder.Instance.inventory.pickUpAItem(item);
				DataHolder.Instance.inventory.minusAttrItem(this.keyCode[type], 1);
				SoundManager.Instance.playAudio("PayRuby");
			}
			SoundManager.Instance.playAudio("Unlock");
			this.setUI();
			this.ccd.checkTime();
			TreasureNotifier.Instance.refresh();
			AccessoriesNotifier.Instance.refresh();

		}
		catch (Exception ex)
		{
			SoundManager.Instance.playAudio("ButtonClick");
			if (ex.Message.Equals("NOT_ENOUGHT_RUBY"))
			{
				this.am.notEnougtRuby.SetActive(true);
			}
		}
	}

	public Text[] keyNumber;

	public GameObject[] freeX1Btns;

	public GameObject[] rubyX1Btns;

	public GameObject[] useKeyX1Btns;

	public GameObject[] rubyX10Btns;

	public GameObject[] useKeyX10Btns;

	public Text[] countDowns;

	public bool[] counting = new bool[3];

	public GameObject openChest;

	public GameObject openx10Chest;

	public ChestsData chestData;

	public ItemSlotWhenOpenChest x1Item;

	public ItemSlotWhenOpenChest[] x10Items;

	public Sprite[] bgItemSprites;

	public Sprite[] bodyChests;

	public Sprite[] upChests;

	public Image[] bodyImages;

	public Image[] upImages;

	public AccesspriesManager am;

	public ChestCountDown ccd;

	private int typeOpen;

	private string[] keyCode = new string[]
	{
		"SILVER-KEY",
		"GOLDEN-KEY",
		"DIAMOND-KEY",
		"LEGENDARY-KEY"
	};

	private int[] rubyX1 = new int[]
	{
		30,
		60,
		90,
		120
	};

	private int[] rubyX10 = new int[]
	{
		270,
		540,
		810,
		1080
	};

	private int[] countDownSec = new int[]
	{
		300,
		600,
		900
	};
}
