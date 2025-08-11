using System;
using UnityEngine;

[Serializable]
public class EnemyDefine
{
	public int getExp(int level)
	{
		float num = Mathf.Pow(this.increase, (float)level);
		return (int)((float)this.exp * num);
	}

	public string _name;

	public int hp;

	public int damage;

	public int defense;

	public int exp;

	public float speed;

	public float increase;
}
