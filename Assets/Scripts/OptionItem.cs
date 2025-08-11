using System;
using UnityEngine;

[Serializable]
public class OptionItem
{
	public TypeBonus getType()
	{
		return this.type;
	}

	public IncreaTarget getTarget()
	{
		return this.increTarget;
	}

	public string getTypeString()
	{
		if (this.increTarget == IncreaTarget.ATK)
		{
			return "Attack";
		}
		if (this.increTarget == IncreaTarget.DEF)
		{
			return "Defense";
		}
		if (this.increTarget == IncreaTarget.SPEED)
		{
			return "Speed";
		}
		if (this.increTarget == IncreaTarget.HP)
		{
			return "Hp";
		}
		if (this.increTarget == IncreaTarget.CRITDAMAGE)
		{
			return "Crit Damage";
		}
		return string.Empty;
	}

	public string getOpDesRichText(int curLevel)
	{
		if (this.increTarget == IncreaTarget.ATK)
		{
			return string.Concat(new string[]
			{
				"Additional attack <color=white>",
				this.getOldString(0),
				"</color> <color=#28FF00FF>(",
				this.getBonusString(curLevel),
				")</color>"
			});
		}
		if (this.increTarget == IncreaTarget.DEF)
		{
			return string.Concat(new string[]
			{
				"Additional defense <color=white>",
				this.getOldString(0),
				"</color> <color=#28FF00FF>(",
				this.getBonusString(curLevel),
				")</color>"
			});
		}
		if (this.increTarget == IncreaTarget.SPEED)
		{
			return string.Concat(new string[]
			{
				"Additional speed <color=white>",
				this.getOldString(0),
				"</color> <color=#28FF00FF>(",
				this.getBonusString(curLevel),
				")</color>"
			});
		}
		if (this.increTarget == IncreaTarget.HP)
		{
			return string.Concat(new string[]
			{
				"Additional max HP <color=white>",
				this.getOldString(0),
				"</color> <color=#28FF00FF>(",
				this.getBonusString(curLevel),
				")</color>"
			});
		}
		if (this.increTarget == IncreaTarget.CRITDAMAGE)
		{
			return "Increase <color=#28FF00FF>" + this.getOldString(curLevel) + "</color> crit damage";
		}
		return string.Empty;
	}

	public string getOldString(int curLevel)
	{
		if (this.type == TypeBonus.ADD)
		{
			return "+" + (this.value + (float)curLevel * this.increaPerlevel).ToString();
		}
		if (this.type == TypeBonus.PERCENT)
		{
			return "+" + (this.value + (float)curLevel * this.increaPerlevel).ToString() + "%";
		}
		return string.Empty;
	}

	public string getNewString(int curLevel)
	{
		int num = curLevel + 1;
		if (this.type == TypeBonus.ADD)
		{
			return "+" + (this.value + (float)num * this.increaPerlevel).ToString();
		}
		if (this.type == TypeBonus.PERCENT)
		{
			return "+" + (this.value + (float)num * this.increaPerlevel).ToString() + "%";
		}
		return string.Empty;
	}

	private string getBonusString(int curLevel)
	{
		float num = this.value + (float)curLevel * this.increaPerlevel - this.value;
		if (this.type == TypeBonus.ADD)
		{
			return "+" + num;
		}
		if (this.type == TypeBonus.PERCENT)
		{
			return "+" + num + "%";
		}
		return string.Empty;
	}

	public string getOpDes(int curLevel)
	{
		return this.getOldString(curLevel) + " " + this.getTypeString();
	}

	public int getValue(int curLevel)
	{
		return (int)(this.value + (float)curLevel * this.increaPerlevel);
	}

	public float value;

	[SerializeField]
	private TypeBonus type;

	[SerializeField]
	private IncreaTarget increTarget;

	public float increaPerlevel;
}
