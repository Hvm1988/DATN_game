using System;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
	private void OnEnable()
	{
		this.setUI();
	}

	private void setUI()
	{
		this.name_txt.text = DataHolder.Instance.playerData.name;
		this.firstDate_txt.text = DataHolder.Instance.playerData.firstDate;
		this.level_txt.text = "Lvl: " + (DataHolder.Instance.playerData.level + 1);
		this.exp_txt.text = DataHolder.Instance.playerData.getExpString();
		this.totalKill_txt.text = DataHolder.Instance.playerData.totalKillMons + string.Empty;
		this.bestCombo_txt.text = DataHolder.Instance.playerData.bestCombo + string.Empty;
		this.hightesPassLevel_txt.text = this.getLastedLevel();
		this.totalStar_txt.text = this.getTotalStar() + string.Empty;
		this.totalLevel_txt.text = DataHolder.Instance.playerData.totalLevelPassed + string.Empty;
	}

	private string getLastedLevel()
	{
		string result = "---";
		for (int i = 0; i < 3; i++)
		{
			for (int j = 1; j < 5; j++)
			{
				for (int k = 1; k < 16; k++)
				{
					HighScoreLevel record = HighScore.getInstance().getRecord(j + "-" + k, i);
					if (record != null)
					{
						result = j + "-" + k;
					}
				}
			}
		}
		return result;
	}

	private int getTotalStar()
	{
		int num = 0;
		for (int i = 0; i < 3; i++)
		{
			for (int j = 1; j < 5; j++)
			{
				for (int k = 1; k < 16; k++)
				{
					HighScoreLevel record = HighScore.getInstance().getRecord(j + "-" + k, i);
					if (record == null)
					{
						return num;
					}
					num += record.numStar;
				}
			}
		}
		return num;
	}

	public Text name_txt;

	public Text firstDate_txt;

	public Text level_txt;

	public Text exp_txt;

	public Text hightesPassLevel_txt;

	public Text totalLevel_txt;

	public Text totalStar_txt;

	public Text totalKill_txt;

	public Text bestCombo_txt;
}
