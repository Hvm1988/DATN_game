using System;
using UnityEngine;

public class AchievementWrapper : MonoBehaviour
{
	private void OnEnable()
	{
		this.init();
	}

	public void init()
	{
		for (int i = 0; i < this.achievements.Length; i++)
		{
			string code = DataHolder.Instance.achievementDefine.achievements[i].code;
			this.achievements[i].init(code, this);
		}
	}

	public void setUI()
	{
		for (int i = 0; i < this.achievements.Length; i++)
		{
			this.achievements[i].setUI();
		}
	}

	public AchievementSlot[] achievements;

	public RectTransform view;
}
