using System;
using UnityEngine;
using UnityEngine.UI;

public class IAPStackGift : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnEnable()
	{
		IAPBuy.onBuySuccessIAP += this.setUI;
		this.setUI();
	}

	private void OnDisable()
	{
		IAPBuy.onBuySuccessIAP -= this.setUI;
	}

	public void setUI()
	{
		float[] iapGiftPoint = DataHolder.Instance.playerDefine.iapGiftPoint;
		float totalIAPPurchared = DataHolder.Instance.playerData.totalIAPPurchared;
		this.totalIAP.text = totalIAPPurchared + "$";
		this.fillBar.fillAmount = totalIAPPurchared / iapGiftPoint[iapGiftPoint.Length - 1];
		for (int i = 0; i < this.vipIcons.Length; i++)
		{
			if (iapGiftPoint[i] <= totalIAPPurchared)
			{
				this.vipIcons[i].sprite = this.hightLightSprites[i];
			}
			else
			{
				this.vipIcons[i].sprite = this.darkSprites[i];
			}
		}
		int firstLockRewardIAP = DataHolder.Instance.playerData.getFirstLockRewardIAP();
		if (firstLockRewardIAP == -1)
		{
			this.nextIAP.enabled = false;
		}
		else
		{
			this.nextIAP.enabled = true;
			this.nextIAP.text = iapGiftPoint[firstLockRewardIAP] + "$";
			this.nextIAP.rectTransform.position = new Vector3(this.gifts[firstLockRewardIAP].rectTransform.position.x, this.nextIAP.rectTransform.position.y, this.nextIAP.rectTransform.position.z);
		}
	}

	public void selectAVip(int id)
	{
		for (int i = 0; i < this.selectedIcon.Length; i++)
		{
			if (i == id)
			{
				this.selectedIcon[i].enabled = true;
			}
			else
			{
				this.selectedIcon[i].enabled = false;
			}
		}
	}

	public Image fillBar;

	public Image[] gifts;

	public Text totalIAP;

	public Text nextIAP;

	public Image[] vipIcons;

	public Sprite[] hightLightSprites;

	public Sprite[] darkSprites;

	public Image[] glows;

	public Image[] selectedIcon;
}
