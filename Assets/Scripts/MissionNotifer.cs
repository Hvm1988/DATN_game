using System;

public class MissionNotifer : Notifier
{
	private void Awake()
	{
		MissionNotifer.Instance = this;
	}

	public override void setUI()
	{
		this.counter = 0;
		foreach (MissionData.MissionSave missionSave in DataHolder.Instance.missionData.missions)
		{
			if (missionSave.canReward(null))
			{
				this.counter++;
			}
		}
		foreach (AchievementData.AchievementSave achievementSave in DataHolder.Instance.achievementData.achievements)
		{
			if (achievementSave.canReward(null))
			{
				this.counter++;
			}
		}
		this.redNote.SetActive(this.counter > 0);
	}

	public static MissionNotifer Instance;
}
