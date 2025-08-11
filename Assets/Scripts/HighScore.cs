using System;
using UnityEngine;

public class HighScore : MonoBehaviour
{
	public static HighScore getInstance()
	{
		if (HighScore._highScore == null)
		{
			HighScore._highScore = new HighScore();
		}
		return HighScore._highScore;
	}

	public HighScoreLevel getRecord(string levelName, int difficult)
	{
		if (HighScore.data == null || HighScore.data.listHighScore.Count == 0)
		{
			if (PlayerPrefs.GetString("highScore").Equals(string.Empty) || PlayerPrefs.GetString("highScore") == null)
			{
				string value = "{\"listHighScore\":[{\"levelName\":\"1-1\",\"time\":0,\"numStar\":0,\"difficult\":0}]}";
				PlayerPrefs.SetString("highScore", value);
			}
			HighScore.data = JsonUtility.FromJson<HighScoreData>(PlayerPrefs.GetString("highScore"));
		}
		for (int i = 0; i < HighScore.data.listHighScore.Count; i++)
		{
			if (HighScore.data.listHighScore[i].levelName.Equals(levelName) && HighScore.data.listHighScore[i].difficult == difficult)
			{
				return HighScore.data.listHighScore[i];
			}
		}
		return null;
	}

	public void setRecord(string levelName, int time, int numStar, int difficult)
	{
		if (this.find(levelName, difficult) != null)
		{
			this.find(levelName, difficult).time = time;
			this.find(levelName, difficult).numStar = numStar;
		}
		else
		{
			HighScoreLevel item = new HighScoreLevel(levelName, time, numStar, difficult);
			HighScore.data.listHighScore.Add(item);
		}
		PlayerPrefs.SetString("highScore", JsonUtility.ToJson(HighScore.data).ToString());
	}

	private HighScoreLevel find(string levelName, int difficult)
	{
		for (int i = 0; i < HighScore.data.listHighScore.Count; i++)
		{
			if (HighScore.data.listHighScore[i].levelName.Equals(levelName) && HighScore.data.listHighScore[i].difficult == difficult)
			{
				return HighScore.data.listHighScore[i];
			}
		}
		return null;
	}

	public void dataLog()
	{
		for (int i = 0; i < HighScore.data.listHighScore.Count; i++)
		{
			UnityEngine.Debug.Log("name_______" + HighScore.data.listHighScore[i].levelName);
			UnityEngine.Debug.Log("star_______" + HighScore.data.listHighScore[i].numStar);
		}
	}

	private static HighScore _highScore;

	private static HighScoreData data;
}
