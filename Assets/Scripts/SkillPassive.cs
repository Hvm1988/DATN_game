using System;

[Serializable]
public class SkillPassive : Skill
{
	public override string getDetailSkill(int level)
	{
		return this.detailSkill.Replace("#VALUE", "<color=#33FF00FF>" + this.getEffectValue(level) + "%</color>");
	}

	public int getEffectValue(int level)
	{
		return this.effectValue[level];
	}

	public int[] effectValue;

	public AddType addType;

	public string specialDetail;
}
