using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialData", menuName = "Game Data/Tutorial data")]
public class TutorialData : DataModel
{
	public override void initFirstTime()
	{
		foreach (TutorialData.Tutorial tutorial in this.tutorials)
		{
			tutorial.status = 0;
		}
	}

	public override void loadFromFireBase()
	{
	}

	public void setTutorial(TutorialManager.Type type, int value)
	{
		for (int i = 0; i < this.tutorials.Count; i++)
		{
			if (this.tutorials[i].type == type)
			{
				this.tutorials[i].status = value;
				break;
			}
		}

		this.save();
	}

	public bool doneAllTutorial()
	{
		for (int i = 0; i < this.tutorials.Count; i++)
		{
			if (this.tutorials[i].status == 0)
			{
				return false;
			}
		}
		return true;
	}

	public bool hasTutorial()
	{
		for (int i = 0; i < this.tutorials.Count; i++)
		{
			TutorialData.Tutorial tutorial = this.tutorials[i];
			if (tutorial.type != TutorialManager.Type.SKILL)
			{
				if (tutorial.status == 0)
				{
					return true;
				}
			}
			else if (DataHolder.Instance.playerData.level >= 2 && tutorial.status == 0)
			{
				return true;
			}
		}
		return false;
	}

	public List<TutorialData.Tutorial> tutorials;

	[Serializable]
	public class Tutorial
	{
		public TutorialManager.Type type;

		public int status;
	}
}
