using System;
using UnityEngine;

public class EnemiesListJson : MonoBehaviour
{
	private void Start()
	{
		UnityEngine.Debug.Log("_______ " + JsonUtility.ToJson(this.listEnemies));
	}

	public EnemiesDefine listEnemies;
}
