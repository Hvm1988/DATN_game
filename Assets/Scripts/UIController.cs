using System;
using System.Collections;
using UnityEngine;

public class UIController : MonoBehaviour
{
	public static UIController Instance
	{
		get
		{
			if (UIController.instance == null)
			{
				UIController.instance = UnityEngine.Object.FindObjectOfType<UIController>();
			}
			return UIController.instance;
		}
	}

	private void Start()
	{
		if (DataHolder.Instance.playerData.name.Equals(string.Empty))
		{
			this.enterName.SetActive(true);
		}
		if (GameSave.isNextGame)
		{
			this.selectLevel.gameObject.SetActive(true);
			int num = DataHolder.selectedMap - 1;
			int num2 = DataHolder.selectedlevel - 1;
			int num3 = num * 20 + num2;
			num3 /= 10;
			this.selectLevel.curLevelStack = num3;
			this.selectLevel.setActiveMap();
			this.selectLevel.selectLevel(num2);
			this.selectLevel.setDifficul(DataHolder.difficult);
		}
		if (DataHolder.showUpgradeItem)
		{
			this.upgradeItem.SetActive(true);
			DataHolder.showUpgradeItem = false;
			return;
		}
		if (DataHolder.showUpgradeSkill)
		{
			this.upgradeSkill.SetActive(true);
			DataHolder.showUpgradeSkill = false;
			return;
		}
		if (!DataHolder.Instance.tutorialData.hasTutorial())
		{
			this.checkRate();
		}
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			this.quitGame.SetActive(true);
		}
	}

	public void checkRate()
	{
		if (DataHolder.Instance.playerData.canShowRate())
		{
			RateUs.ins.onShow();
		}
		if (DataHolder.Instance.willShow3Ruby)
		{
			this.bonus3Ruby.SetActive(true);
			DataHolder.Instance.playerData.addRuby(3);
			DataHolder.Instance.willShow3Ruby = false;
		}
	}

	public void showStarterPack()
	{
		this.starterPack.SetActive(true);
	}

	public void showStarterPackAfterTut()
	{
		base.StartCoroutine(this.showStarterPackCor());
	}

	private IEnumerator showStarterPackCor()
	{
		yield return new WaitUntil(() => !this.upgradePop.activeSelf);
		yield return new WaitForSeconds(0f);
		this.starterPack.SetActive(true);
		yield break;
	}

	public void onExtendClickFromPrePlay()
	{
		this.accessories.SetActive(true);
		this.accessories.GetComponent<AccesspriesManager>().openMatTab();
	}

	private IEnumerator waitToCloseAccessoriCor(GameObject[] activePanelAfters)
	{
		yield return new WaitUntil(() => !this.accessories.activeSelf);
		yield return new WaitForSeconds(0.2f);
		for (int i = 0; i < activePanelAfters.Length; i++)
		{
			activePanelAfters[i].SetActive(true);
		}
		yield break;
	}

	private static UIController instance;

	public GameObject eventPop;

	public GameObject starterPack;

	public GameObject upgradePop;

	public SelectLevel selectLevel;

	public OutOfSlotItem outSlotItem;

	public GameObject accessories;

	public GameObject prePlay;

	public GameObject enterName;

	public GameObject bonus3Ruby;

	public GameObject quitGame;

	public GameObject upgradeItem;

	public GameObject upgradeSkill;
}
