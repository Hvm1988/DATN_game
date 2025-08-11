using System;

public class SkillNotifer : Notifier
{
	private void Awake()
	{
		SkillNotifer.Instance = this;
	}

	public void Start()
	{
		foreach (SkillSave skillSave in DataHolder.Instance.skillData.skillSaves)
		{
			skillSave.calCondition();
		}
		DataHolder.Instance.skillData.save();
		this.setUI();
	}

	public override void setUI()
	{
		this.counter = 0;
		foreach (SkillSave skillSave in DataHolder.Instance.skillData.skillSaves)
		{
			if (skillSave.canUpgradeGold() || skillSave.canUpgradeRuby())
			{
				this.counter++;
			}
		}
		this.redNote.SetActive(this.counter > 0);
	}

	public static SkillNotifer Instance;
}
