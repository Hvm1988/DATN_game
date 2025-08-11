using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
	private void OnEnable()
	{
		DataHolder.watchedVideoAddStat = false;
		if (!GameSave.isNextGame)
		{
			this.curLevelStack = PlayerPrefs.GetInt("LAST-STACK-LEVEL-ID");
			this.setDifficul(PlayerPrefs.GetInt("LAST-DIFFICUL"));
			this.setActiveMap();
		}
	}

	public void selectLevel(int level)
	{
		DataHolder.selectedMap = this.curLevelStack / 2 + 1;
		DataHolder.selectedlevel = level + 1;
		this.prePlay.SetActive(true);
	}

	public void nextMap()
	{
		if (this.curLevelStack < this.levelsStack.Length - 1)
		{
			this.curLevelStack++;
		}
		else
		{
			this.curLevelStack = 0;
		}
		this.setActiveMap();
	}

	public void prevMap()
	{
		if (this.curLevelStack > 0)
		{
			this.curLevelStack--;
		}
		else
		{
			this.curLevelStack = this.levelsStack.Length - 1;
		}
		this.setActiveMap();
	}

	public void setActiveMap()
	{
		foreach (GameObject gameObject in this.levelsStack)
		{
			gameObject.SetActive(false);
		}
		PlayerPrefs.SetInt("LAST-STACK-LEVEL-ID", this.curLevelStack);
		this.levelsStack[this.curLevelStack].SetActive(true);
		int num = this.curLevelStack / 2;
		this.titleTxt.text = SelectLevel.titles[num];
		this.bgChapterImg.sprite = this.chapterSprites[num];
		this.setBtn();
		if (DataHolder.difficult > 0 && HighScore.getInstance().getRecord((num + 1).ToString() + "-1", DataHolder.difficult - 1) == null)
		{
			this.setDifficul(0);
		}
	}

	public void setBtn()
	{
		this.nextBtn.SetActive(this.curLevelStack < this.levelsStack.Length - 1);
		this.backBtn.SetActive(this.curLevelStack > 0);
	}

	public void selectMode(int id)
	{
	}

	public void setDifficul(int value)
	{
		UnityEngine.Debug.Log(DataHolder.difficult);
		int num = this.curLevelStack / 2 + 1;
		if (value == 0)
		{
			DataHolder.difficult = value;
			foreach (Chapter chapter in this.chapters)
			{
				chapter.setUI();
			}
			this.setDifficulIcon();
			return;
		}
		HighScoreLevel record = HighScore.getInstance().getRecord(num + "-20", value - 1);
		if (record != null)
		{
			DataHolder.difficult = value;
			foreach (Chapter chapter2 in this.chapters)
			{
				chapter2.setUI();
			}
			this.setDifficulIcon();
		}
		else
		{
			SoundManager.Instance.playAudio("ButtonClick");
			if (value != 0)
			{
				if (value != 1)
				{
					if (value == 2)
					{
						this.notiferPanel.init("You must complete \"Normal\" mode !");
					}
				}
				else
				{
					this.notiferPanel.init("You must complete \"Easy mode\" !");
				}
			}
			this.notiferPanel.gameObject.SetActive(true);
		}
		PlayerPrefs.SetInt("LAST-DIFFICUL", DataHolder.difficult);
	}

	private void setDifficulIcon()
	{
		for (int i = 0; i < this.difficultIcons.Length; i++)
		{
			if (i == DataHolder.difficult)
			{
				this.difficultIcons[i].sprite = this.difficultSprites[0];
			}
			else
			{
				this.difficultIcons[i].sprite = this.difficultSprites[1];
			}
		}
		this.titleTxt.color = this.difficultColors[DataHolder.difficult];
	}

	public GameObject prePlay;

	public NotifierSelecmap notiferPanel;

	public int curLevelStack;

	public GameObject[] levelsStack;

	public GameObject nextBtn;

	public GameObject backBtn;

	public Text titleTxt;

	public Chapter[] chapters;

	private static string[] titles = new string[]
	{
		"CHAPTER 1",
		"CHAPTER 2"
	};

	public Color[] difficultColors;

	[SerializeField]
	private Sprite[] chapterSprites;

	public Image bgChapterImg;

	public Sprite[] difficultSprites;

	public Image[] difficultIcons;
}
