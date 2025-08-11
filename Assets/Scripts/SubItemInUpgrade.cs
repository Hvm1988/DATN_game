using System;
using UnityEngine;
using UnityEngine.UI;

public class SubItemInUpgrade : MonoBehaviour
{
	private void OnEnable()
	{
		this.init();
		this.setUI();
	}

	public void init()
	{
		this.item = DataHolder.Instance.playerData.getItemByType(this.typeItem);
	}

	public void setUI()
	{
		if (this.item == null)
		{
			this.icon.enabled = false;
			this.type.enabled = false;
			this.oldStat.enabled = false;
			this.newStat.enabled = false;
			this.buyGold.gameObject.SetActive(false);
			this.buyRuby.gameObject.SetActive(false);
			this.level.enabled = false;
			this.buyGold.interactable = false;
			this.buyRuby.interactable = false;
			this.goldCost.text = "---";
			this.rubyCost.text = "---";
		}
		else
		{
			MainItem mainByCode = DataHolder.Instance.mainItemsDefine.getMainByCode(this.item.code);
			this.icon.enabled = true;
			this.type.enabled = true;
			this.oldStat.enabled = true;
			this.newStat.enabled = true;
			this.buyGold.gameObject.SetActive(true);
			this.buyRuby.gameObject.SetActive(true);
			this.level.enabled = true;
			this.buyGold.interactable = true;
			this.buyRuby.interactable = true;
			if (this.item.level == 10)
			{
				this.newStat.enabled = false;
				this.buyGold.gameObject.SetActive(false);
				this.buyRuby.gameObject.SetActive(false);
			}
			else
			{
				this.newStat.enabled = true;
				this.rubyCost.text = CustomInt.toString(this.item.getRubyCostUpgadeNextLevel()) + string.Empty;
				int goldCostUpgadeNextLevel = this.item.getGoldCostUpgadeNextLevel();
				if (goldCostUpgadeNextLevel == -1)
				{
					this.buyGold.gameObject.SetActive(false);
				}
				else
				{
					this.goldCost.text = CustomInt.toString(goldCostUpgadeNextLevel) + string.Empty;
				}
				this.newStat.text = mainByCode.optionItem[0].getNewString(this.item.level);
			}
			this.icon.sprite = mainByCode.icon;
			this.type.text = mainByCode.optionItem[0].getTypeString();
			this.oldStat.text = mainByCode.optionItem[0].getOldString(this.item.level);
			this.level.text = "+" + this.item.level;
			if (this.item.canUpgradeWithGold())
			{
				this.buyGold.interactable = true;
				this.buyGold.image.color = new Color(1f, 1f, 1f, 1f);
				this.goldCost.color = new Color(1f, 1f, 1f, 1f);
			}
			else
			{
				this.buyGold.interactable = false;
				this.buyGold.image.color = new Color(1f, 1f, 1f, 0.9f);
				this.goldCost.color = new Color(1f, 1f, 1f, 0.9f);
			}
			if (this.item.canUpgradeWithRuby())
			{
				this.buyRuby.interactable = true;
				this.buyRuby.image.color = new Color(1f, 1f, 1f, 1f);
				this.rubyCost.color = new Color(1f, 1f, 1f, 1f);
			}
			else
			{
				this.buyRuby.interactable = false;
				this.buyRuby.image.color = new Color(1f, 1f, 1f, 0.9f);
				this.rubyCost.color = new Color(1f, 1f, 1f, 0.9f);
			}
		}
	}

	public void upgradeGold()
	{
		try
		{
			this.item.upgradeGold(null);
			UpgradeUIController.Instance.SetUI();
			this.successEffect.SetActive(true);
		}
		catch (Exception ex)
		{
			if (ex.Message.Equals("FAIL"))
			{
				this.failEffect.SetActive(false);
			}
		}
	}

	public void upgradeRuby()
	{
		try
		{
			this.item.upgradeRuby(null);
			this.successEffect.SetActive(true);
			UpgradeUIController.Instance.SetUI();
		}
		catch (Exception ex)
		{
			if (ex.Message.Equals("FAIL"))
			{
				this.failEffect.SetActive(true);
			}
		}
	}

	public MainItemType typeItem;

	public Image icon;

	public Text type;

	public Text oldStat;

	public Text newStat;

	public Button buyGold;

	public Button buyRuby;

	public MainItemInven item;

	public Text goldCost;

	public Text rubyCost;

	public Text level;

	public GameObject failEffect;

	public GameObject successEffect;
}
