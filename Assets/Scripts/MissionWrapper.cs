using System;
using UnityEngine;

public class MissionWrapper : MonoBehaviour
{
	private void OnEnable()
	{
		this.view.localPosition = new Vector3(600f, this.view.localPosition.y, 0f);
		this.init();
	}

	private void init()
	{
		for (int i = 0; i < this.missionSlots.Length; i++)
		{
			string code = DataHolder.Instance.missionData.missions[i].code;
			this.missionSlots[i].init(code, this);
		}
	}

	public void setUI()
	{
		for (int i = 0; i < this.missionSlots.Length; i++)
		{
			this.missionSlots[i].setUI(false);
		}
	}

	public MissionSlot[] missionSlots;

	public RectTransform view;
}
