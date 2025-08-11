using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Game Data/Player Define")]
public class PlayerDefine : DataModel
{
	public override void initFirstTime()
	{
	}

	public override void loadFromFireBase()
	{
		
	}

	public int getATK(int curlevel)
	{
		double x = this.increDamagePerLevel / 100.0 + 1.0;
		float num = (float)Math.Pow(x, (double)curlevel);
		return (int)((float)this.baseDamage * num);
	}

	public int getHP(int curlevel)
	{
		double x = this.increHpPerLevel / 100.0 + 1.0;
		float num = (float)Math.Pow(x, (double)curlevel);
		return (int)((float)this.baseHp * num);
	}

	public int getDEF(int curlevel)
	{
		double x = this.increDefPerLevel / 100.0 + 1.0;
		float num = (float)Math.Pow(x, (double)curlevel);
		return (int)((float)this.baseDef * num);
	}

	public int getEXP(int curlevel)
	{
		return this.getValueStat(curlevel, 3);
	}

	private int getValueStat(int level, int type)
	{
		string text = this.playerStats[level];
		string[] array = text.Split(new char[]
		{
			','
		});
		return int.Parse(array[type]);
	}

	public string[] playerStats;

	public int baseDef;

	public int baseDamage;

	public int baseHp;

	public double increDefPerLevel;

	public double increDamagePerLevel;

	public double increHpPerLevel;

	public float[] iapGiftPoint;
}
