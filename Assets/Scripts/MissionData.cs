using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionData", menuName = "Game Data/Mission Data")]
public class MissionData : DataModel
{
	public override void initFirstTime()
	{
		foreach (MissionData.MissionSave missionSave in this.missions)
		{
			missionSave.done = 0;
			missionSave.status = -1;
		}
	}

	public override void loadFromFireBase()
	{
		throw new NotImplementedException();
	}

	public void addDone(Mission mission, string code, int value)
	{
		this.getMission(code).addDone(mission, value);
		if (MissionNotifer.Instance != null)
		{
			MissionNotifer.Instance.setUI();
		}
		this.save();
	}

	public MissionData.MissionSave getMission(string code)
	{
		foreach (MissionData.MissionSave missionSave in this.missions)
		{
			if (missionSave.code.Equals(code))
			{
				return missionSave;
			}
		}
		return null;
	}

	public void reward(string code)
	{
		this.getMission(code).status = 1;
		if (MissionNotifer.Instance != null)
		{
			MissionNotifer.Instance.setUI();
		}
		this.save();
	}

	public MissionData.MissionSave[] missions;

	[Serializable]
	public class MissionSave
	{
		public bool canReward(Mission mission)
		{
			if (mission == null)
			{
				mission = DataHolder.Instance.missionDefine.getMission(this.code);
			}
			return this.done >= mission.number && this.status != 1;
		}

		public void addDone(Mission mission, int value)
		{
			if (this.status != -1)
			{
				return;
			}
			if (mission == null)
			{
				mission = DataHolder.Instance.missionDefine.getMission(this.code);
			}
			this.done += value;
			if (this.done >= mission.number)
			{
				this.done = mission.number;
				this.status = 0;
				if (!this.code.Equals("COMPLETE-ALL"))
				{
					DataHolder.Instance.missionData.addDone(mission, "COMPLETE-ALL", 1);
				}

			}
		}

		public string code;

		public int done;

		public int status;
	}
}
