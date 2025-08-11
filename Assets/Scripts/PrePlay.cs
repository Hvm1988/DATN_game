using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class PrePlay : NetworkAble
{
	private void OnEnable()
	{
		this.init();
        this.getScoreData(string.Concat(new object[]
        {
            DataHolder.selectedMap,
            "-",
            DataHolder.selectedlevel,
            "-",
            DataHolder.difficult
        }));
        for (int i = 0; i < this.modeTitles.Length; i++)
		{
			if (i == DataHolder.difficult)
			{
				this.modeTitles[i].SetActive(true);
			}
			else
			{
				this.modeTitles[i].SetActive(false);
			}
		}
	}

	private void init()
	{
		this.levelData = (Resources.Load(string.Concat(new object[]
		{
			"GameData/LevelData/",
			DataHolder.selectedMap,
			"-",
			DataHolder.selectedlevel
		})) as LevelData);
		if (this.levelData == null)
		{
			return;
		}
		HighScoreLevel record = HighScore.getInstance().getRecord(DataHolder.selectedMap + "-" + DataHolder.selectedlevel, DataHolder.difficult);
		if (record == null)
		{
			this.skipButton.interactable = false;
		}
		else
		{
			this.skipButton.interactable = (record.numStar > 0);
		}
		for (int i = 0; i < this.levelData.scroll.Length; i++)
		{
			string code = GetScrollItemById.getInstance()[this.levelData.scroll[i]];
			ScrollItem scrollByCode = DataHolder.Instance.mainItemsDefine.getScrollByCode(code);
			this.scrollIcons[i].sprite = scrollByCode.productIcon;
			this.scrollNames[i].text = DataHolder.Instance.mainItemsDefine.getMainByCode(scrollByCode.codeProduct).name;
		}
		List<int> list = new List<int>();
		for (int j = 0; j < this.levelData.materialTurn1.Length; j += 2)
		{
			if (!list.Contains(this.levelData.materialTurn1[j]))
			{
				list.Add(this.levelData.materialTurn1[j]);
			}
		}
		for (int k = 0; k < this.levelData.materialTurn2.Length; k += 2)
		{
			if (!list.Contains(this.levelData.materialTurn2[k]))
			{
				list.Add(this.levelData.materialTurn2[k]);
			}
		}
		for (int l = 0; l < this.levelData.materialTurn3.Length; l += 2)
		{
			if (!list.Contains(this.levelData.materialTurn3[l]))
			{
				list.Add(this.levelData.materialTurn3[l]);
			}
		}
		for (int m = 0; m < list.Count; m++)
		{
			ResourceItem resourceItem = DataHolder.Instance.mainItemsDefine.resourceItem[list[m] - 1];
			this.materialIcons[m].sprite = resourceItem.icon;
			this.materialNames[m].text = resourceItem.name;
		}
		this.goldBonus.text = this.levelData.gold + string.Empty;
		this.expBonus.text = this.levelData.exp + string.Empty;
		this.name.text = this.levelData.name;
		this.des.text = this.levelData.description;
	}

	public void play()
	{
		if (DataHolder.Instance.inventory.getFreeSlotResource() < 3)
		{
			this.checkSlotFrom = "PLAY";
			this.outSlotItems.SetActive(true);
			return;
		}
		try
		{
			DataHolder.Instance.playerData.addEnergy(-10);
			DataHolder.Instance.playerData.increaPlayCount();
			this.canvasLoading.SetActive(true);
		}
		catch (Exception ex)
		{
			if (ex.Message.Equals("NOT_ENOUGHT_ENERGY"))
			{
				this.notEnoughtStamina.SetActive(true);
			}
		}
	}

	public void onIgnore()
	{
		this.outSlotItems.SetActive(false);
		if (this.checkSlotFrom.Equals("PLAY"))
		{
			try
			{
				DataHolder.Instance.playerData.addEnergy(-10);
				this.canvasLoading.SetActive(true);
			}
			catch (Exception ex)
			{
				if (ex.Message.Equals("NOT_ENOUGHT_ENERGY"))
				{
					this.notEnoughtStamina.SetActive(true);
				}
			}
		}
		if (this.checkSlotFrom.Equals("SKIP"))
		{
			this.onSkipClick(false);
		}
	}

	public void watchVideo()
	{
		AdsController.Instance.showRewardVideo(delegate
		{
			DataHolder.watchedVideoAddStat = true;
			this.watchVideoBtn.SetActive(false);
		}, delegate
		{
			this.vidNotReady.SetActive(true);
		});
	}

	public void getScoreData(string level_id)
	{
		//for (int i = 0; i < this.nameHightScoreTxts.Length; i++)
		//{
		//	this.nameHightScoreTxts[i].text = "Loading ...";
		//	this.timeHightScoreTxts[i].text = string.Empty;
		//}
		//Dictionary<string, string> datas = new Dictionary<string, string>
		//{
		//	{
		//		"level_id",
		//		level_id
		//	}
		//};
		//string url = "http://";
		//base.StartCoroutine(base.callAPI(url, datas, new Action<string>(this.onGetHiScoreSuccess), delegate(string result)
		//{
		//	UnityEngine.Debug.Log("Fail");
		//}));
	}

	private void onGetHiScoreSuccess(string result)
	{
		PrePlay.HightScoreDataSave[] array = JsonMapper.ToObject<PrePlay.HightScoreDataSave[]>(result);
		for (int i = 0; i < this.nameHightScoreTxts.Length; i++)
		{
			if (i <= array.Length - 1)
			{
				this.nameHightScoreTxts[i].text = array[i].name;
				this.timeHightScoreTxts[i].text = array[i].getTime();
			}
			else
			{
				this.nameHightScoreTxts[i].text = "--";
				this.timeHightScoreTxts[i].text = "--";
			}
		}
	}

	public void onSkipClick(bool checkSlot = true)
	{
		if (checkSlot && DataHolder.Instance.inventory.getFreeSlotResource() < 3)
		{
			this.checkSlotFrom = "SKIP";
			this.outSlotItems.SetActive(true);
			return;
		}
		try
		{
			DataHolder.Instance.playerData.addSkip(-1);
			LevelUpPopup.instance.cacheOldLevel = DataHolder.Instance.playerData.level;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			List<string[]> list = new List<string[]>();
			list.Add(this.levelData.turn1);
			list.Add(this.levelData.turn2);
			list.Add(this.levelData.turn3);
			list.Add(this.levelData.boss);
			for (int i = 0; i < list.Count; i++)
			{
				string[] array = list[i];
				if (array.Length != 0)
				{
					for (int j = 0; j < array.Length; j += 3)
					{
						string enemyName = array[j];
						int level = int.Parse(array[j + 2]);
						int exp = DataHolder.Instance.enemiesDefine.getEnemies(enemyName).getExp(level);
						num += exp;
						num2 += UnityEngine.Random.Range(2, 25);
						int num4 = UnityEngine.Random.Range(1, 101);
						if (num4 < GameConfig.percentRubyDropBase)
						{
							num3++;
						}
					}
				}
			}
			GameSave.resetItemEat();
			for (int k = 0; k < this.levelData.materialTurn1.Length; k += 2)
			{
				GameSave.itemsEat[this.levelData.materialTurn1[k] - 1] += this.levelData.materialTurn1[k + 1];
			}
			for (int l = 0; l < this.levelData.materialTurn2.Length; l += 2)
			{
				GameSave.itemsEat[this.levelData.materialTurn2[l] - 1] += this.levelData.materialTurn2[l + 1];
			}
			for (int m = 0; m < this.levelData.materialTurn3.Length; m += 2)
			{
				GameSave.itemsEat[this.levelData.materialTurn3[m] - 1] += this.levelData.materialTurn3[m + 1];
			}
			string item = GetScrollItemById.getInstance()[this.levelData.scroll[UnityEngine.Random.Range(0, this.levelData.scroll.Length)]];
			this.gameResult.itemEat.Add(item);
			GameSave.getExp = num;
			GameSave.getCoin = num2;
			GameSave.getRuby = num3;
			DataHolder.Instance.playerData.addExp(num);
			this.gameResult.show(true);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			if (ex.Message.Equals("NOT_ENOUGHT_SKIP"))
			{
				this.notEnoughtSkip.SetActive(true);
			}
		}
	}

	public Image[] scrollIcons;

	public Text[] scrollNames;

	public Image[] materialIcons;

	public Text[] materialNames;

	public Text goldBonus;

	public Text expBonus;

	public GameObject canvasLoading;

	public LevelData levelData;

	public new Text name;

	public Text des;

	public GameObject notEnoughtStamina;

	public GameObject notEnoughtSkip;

	public GameObject vidNotReady;

	public GameObject watchVideoBtn;

	public Text[] nameHightScoreTxts;

	public Text[] timeHightScoreTxts;

	public GameObject outSlotItems;

	public GameResult gameResult;

	public Button skipButton;

	private string checkSlotFrom = string.Empty;

	public GameObject[] modeTitles;

	[Serializable]
	public class PrePlayLevelData
	{
		public string name;

		public string description;

		public int gold;

		public int exp;

		public string[] marterial;

		public string[] scroll;
	}

	[Serializable]
	public class HightScoreDataSave
	{
		public string getTime()
		{
			int num = int.Parse(this.time);
			int num2 = num / 60;
			int num3 = num - num2 * 60;
			return string.Concat(new object[]
			{
				num2,
				"m",
				num3,
				"s"
			});
		}

		public string name;

		public string level_id;

		public string time;

		public string created;

		public string modified;
	}
}
