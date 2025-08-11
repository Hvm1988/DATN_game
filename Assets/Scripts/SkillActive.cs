using System;

[Serializable]
public class SkillActive : Skill
{
	public override string getDetailSkill(int level)
	{
		return this.detailSkill.Replace("#VALUE", "<color=#33FF00FF>" + this.getDamage(level).ToString() + "</color>");
	}

	public string getCoolDown(int level)
	{
		return "CoolDown: <color=#33FF00FF>" + this.coolDowns[level] + "</color> sec";
	}

	public int getDamage(int level)
	{
		return this.damages[level];
	}

	public int[] damages;

	public int[] coolDowns;
}
