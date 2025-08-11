using System;
using System.Collections;
using Common;
using UnityEngine;
using UnityEngine.UI;

public class BlackMarket : MonoBehaviour
{
	private void OnEnable()
	{
		this.checkFreeDown();
		this.data.checkInit();
		this.setUI();
	}

	private void setUI()
	{
		for (int i = 0; i < this.data.itemToBuys.Count; i++)
		{
			BlackMarketData.ItemInBM itemInBM = this.data.itemToBuys[i];
			this.iconItems[i].sprite = itemInBM.icon;
			this.iconItems[i].transform.localScale = DataHolder.Instance.shopItemDefine.getScale(itemInBM.type);
			this.numbers[i].text = "x" + CustomInt.toString(itemInBM.number);
			if (itemInBM.number == 1)
			{
				this.numbers[i].enabled = false;
			}
			else
			{
				this.numbers[i].enabled = true;
			}
			this.priceTexts[i].text = itemInBM.price + string.Empty;
			if (itemInBM.type != ItemTypeUI.SCROLL)
			{
				this.productIcons[i].enabled = false;
			}
			else
			{
				this.productIcons[i].enabled = true;
				ScrollItem scrollByCode = DataHolder.Instance.mainItemsDefine.getScrollByCode(itemInBM.code);
				this.productIcons[i].sprite = scrollByCode.productIcon;
			}
			this.soldOuts[i].SetActive(this.data.status[i] == 1);
			this.buyBtns[i].SetActive(this.data.status[i] == 0);
		}
	}

	private void checkFreeDown()
	{
		base.StopAllCoroutines();
		if (DatePassHelper.getSecPassed("BLACKMARTKET") == -1)
		{
			DatePassHelper.saveNowToPref("BLACKMARTKET", DatePassHelper.DateFormat.ddMMyyyyhhmmss);
		}
		int start = 3600 - DatePassHelper.getSecPassed("BLACKMARTKET");
		base.StartCoroutine(this.countDown(start));
	}

	private IEnumerator countDown(int start)
	{
		int c = start;
		while (c > 0)
		{
			this.countDownText.text = "Free " + DatePassHelper.splitSecondToString(c);
			c--;
			yield return new WaitForSeconds(1f);
		}
		c = 0;
		this.countDownText.text = "Free " + DatePassHelper.splitSecondToString(c);
		this.data.initItem();
		this.setUI();
		DatePassHelper.saveNowToPref("BLACKMARTKET", DatePassHelper.DateFormat.ddMMyyyyhhmmss);
		this.checkFreeDown();
		yield return null;
		yield break;
	}

	public void refresh()
	{
		try
		{
			DataHolder.Instance.playerData.addRuby(-50);
			this.data.initItem();
			this.setUI();
			DatePassHelper.saveNowToPref("BLACKMARTKET", DatePassHelper.DateFormat.ddMMyyyyhhmmss);
			this.checkFreeDown();
		}
		catch (Exception ex)
		{
			if (ex.Message.Equals("NOT_ENOUGHT_RUBY"))
			{
				this.notEnoughtRuby.SetActive(true);
			}
		}
	}

	public void buy(int id)
	{
		try
		{
			BlackMarketData.ItemInBM itemInBM = this.data.itemToBuys[id];
			DataHolder.Instance.playerData.addRuby(-itemInBM.price);
			this.data.status[id] = 1;
			this.soldOuts[id].SetActive(this.data.status[id] == 1);
			this.buyBtns[id].SetActive(this.data.status[id] == 0);
			this.data.save();
			itemInBM.reward(1);

		}
		catch (Exception ex)
		{
			if (ex.Message.Equals("NOT_ENOUGHT_RUBY"))
			{
				this.notEnoughtRuby.SetActive(true);
			}
		}
	}

	public Image[] iconItems;

	public Text[] numbers;

	public Image[] productIcons;

	public Text[] priceTexts;

	public GameObject[] soldOuts;

	public GameObject[] buyBtns;

	public BlackMarketData data;

	public Text countDownText;

	public GameObject notEnoughtRuby;
}
