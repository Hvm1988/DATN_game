using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemiesDefine")]
public class EnemiesDefine : DataModel
{
	public override void initFirstTime()
	{
	}

	public override void loadFromFireBase()
	{
	
	}

	public EnemyDefine getEnemies(string enemyName)
	{
		for (int i = 0; i < this.list.Count; i++)
		{
			if (this.list[i]._name.Equals(enemyName))
			{
				return this.list[i];
			}
		}
		return null;
	}

	public List<EnemyDefine> list;
}
