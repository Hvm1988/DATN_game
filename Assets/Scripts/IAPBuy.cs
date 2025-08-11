using System;
using System.Collections.Generic;
using System.Diagnostics;
using Common;
using UnityEngine;

public class IAPBuy : MonoBehaviour
{
	public static event IAPBuy.OnBuySuccessIAP onBuySuccessIAP;

	private void Awake()
	{
        if (IAPBuy.Instance == null)
        {
            IAPBuy.Instance = this;
            UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
            
        } else
        {
            UnityEngine.Object.Destroy(base.gameObject);
        }
	}
    private void Start()
    {
        this.PurchaserMethod.Add(1, new Action(this.purchaser.BuyConsumable1));
        this.PurchaserMethod.Add(2, new Action(this.purchaser.BuyConsumable2));
        this.PurchaserMethod.Add(3, new Action(this.purchaser.BuyConsumable3));
        this.PurchaserMethod.Add(4, new Action(this.purchaser.BuyConsumable4));
        this.PurchaserMethod.Add(6, new Action(this.purchaser.BuyConsumable5));
        this.PurchaserMethod.Add(12, new Action(this.purchaser.BuyConsumable6));
        this.PurchaserMethod.Add(13, new Action(this.purchaser.BuyConsumable7));
        this.PurchaserMethod.Add(26, new Action(this.purchaser.BuyConsumable8));
        this.PurchaserMethod.Add(52, new Action(this.purchaser.BuyConsumable9));
        this.PurchaserMethod.Add(5, new Action(this.purchaser.BuyConsumable10));
        this.PurchaserMethod.Add(10, new Action(this.purchaser.BuyConsumable11));
        this.PurchaserMethod.Add(20, new Action(this.purchaser.BuyConsumable12));
    }

    public void onSuccessPurchaser()
	{
		UnityEngine.Debug.Log("onSuccessPurchaser");
		DataHolder.Instance.playerData.addTotalIAPPurchared((float)this.curPrice);
		DataHolder.Instance.missionData.addDone(null, "PURCHASER", 1);
		if (IAPBuy.onBuySuccessIAP != null)
		{
			IAPBuy.onBuySuccessIAP();
		}
		foreach (Item item in this.items)
		{
			item.reward(1);
		}
	}

	public void onClickBuy(Item[] items, int price)
	{
		if (UIController.Instance != null && !this.isEnoughtSlot(items))
		{
			UIController.Instance.outSlotItem.init(OutOfSlotItem.TypeOut.SHOP, null);
			return;
		}
		//SpecialSoundManager.Instance.playAudio("PayPurchaser");
		this.items = items;
		this.curPrice = price;
		this.purchaser.onSuccessPurchaser = new Action(this.onSuccessPurchaser);
		if (this.PurchaserMethod[price] != null)
		{
			this.PurchaserMethod[price]();
		}
	}

	private bool isEnoughtSlot(Item[] items)
	{
		foreach (Item item in items)
		{
			if ((item.type == ItemTypeUI.SCROLL || item.type == ItemTypeUI.SCROLL_RANDOM || item.type == ItemTypeUI.RES) && DataHolder.Instance.inventory.getFreeSlotResource() < 1)
			{
				return false;
			}
		}
		return true;
	}

	public static IAPBuy Instance;

	private Dictionary<int, Action> PurchaserMethod = new Dictionary<int, Action>();

	public Purchaser purchaser;

	private int curPrice;

	private Item[] items;

	private Action callBackSuccess;

	public delegate void OnBuySuccessIAP();
}
