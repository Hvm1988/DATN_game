using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter : MonoBehaviour
{
	private void Start()
	{
		this.setUI();
	}

	public void setUI()
	{
		for (int i = 0; i < this.levels.Count; i++)
		{
			HighScoreLevel record = HighScore.getInstance().getRecord(this.chapterName + "-" + (i + 1).ToString(), DataHolder.difficult);
			this.levels[i].setUI(record, this.darkLineSprite, this.lightLineSprite, i + 1, this.chapterName);
		}
	}

	public List<Chapter.Level> levels;

	public string chapterName;

	public Sprite lightLineSprite;

	public Sprite darkLineSprite;

	[Serializable]
	public class Level
	{
		public void setUI(HighScoreLevel data, Sprite darkSprite, Sprite lightSprite, int level, string chapterName)
		{
			foreach (GameObject gameObject in this.stars)
			{
				gameObject.SetActive(false);
			}
			if (this.prevLine != null)
			{
				this.prevLine.sprite = darkSprite;
			}
			this.textLevel.SetActive(false);
			this.locked.SetActive(true);
			this.selected.SetActive(false);
			HighScoreLevel record = HighScore.getInstance().getRecord(chapterName + "-" + (level - 1).ToString(), DataHolder.difficult);
			if (chapterName.Equals("2") && level == 1)
			{
				record = HighScore.getInstance().getRecord("1-20", DataHolder.difficult);
			}
			if (record != null)
			{
				if (record.numStar > 0)
				{
					this.textLevel.SetActive(true);
					this.locked.SetActive(false);
					this.selected.SetActive(true);
					if (this.prevLine != null)
					{
						this.prevLine.sprite = lightSprite;
					}
				}
			}
			else if (chapterName.Equals("1") && level == 1)
			{
				this.textLevel.SetActive(true);
				this.locked.SetActive(false);
				this.selected.SetActive(true);
			}
			this.btn.interactable = !this.locked.activeSelf;
			if (data == null)
			{
				return;
			}
			this.locked.SetActive(false);
			this.textLevel.SetActive(true);
			this.btn.interactable = true;
			this.selected.SetActive(false);
			if (data.numStar > 0)
			{
				for (int j = 0; j < data.numStar; j++)
				{
					this.stars[j].SetActive(true);
				}
			}
		}

		public string name;

		public GameObject[] stars;

		public Image prevLine;

		public GameObject selected;

		public GameObject locked;

		public GameObject textLevel;

		public Button btn;
	}
}
