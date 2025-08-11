using System;

[Serializable]
public class HighScoreLevel
{
	public HighScoreLevel(string levelName, int time, int numStar, int difficult)
	{
		this.levelName = levelName;
		this.time = time;
		this.numStar = numStar;
		this.difficult = difficult;
	}

	public string levelName;

	public int time;

	public int numStar;

	public int difficult;
}
