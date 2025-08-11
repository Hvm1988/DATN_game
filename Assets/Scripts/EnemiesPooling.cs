using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPooling : MonoBehaviour
{
	private void Awake()
	{
		if (EnemiesPooling.instance == null)
		{
			EnemiesPooling.instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public Enemies getEnemy(string code)
	{
		for (int i = 0; i < this.listEnemy.Count; i++)
		{
			if (this.listEnemy[i].code.Equals(code))
			{
				return this.listEnemy[i];
			}
		}
		return null;
	}

	public void removeObj(Enemies obj)
	{
		this.listEnemy.Remove(obj);
	}

	public void disableAll()
	{
		for (int i = 0; i < this.listEnemy.Count; i++)
		{
			this.listEnemy[i]._animations.playAnimation(this.listEnemy[i]._animations.idle, true);
			this.listEnemy[i].gameObject.SetActive(false);
		}
	}

	public void resetItemDrop()
	{
		for (int i = 0; i < this.listEnemy.Count; i++)
		{
			this.listEnemy[i].hadItem = false;
			this.listEnemy[i].materialCodeDrop = 0;
			this.listEnemy[i].materialNumDrop = 0;
		}
	}

	public static EnemiesPooling instance;

	public List<Enemies> listEnemy;
}
