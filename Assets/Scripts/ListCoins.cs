using System;
using System.Collections.Generic;
using UnityEngine;

public class ListCoins : MonoBehaviour
{
	private void Awake()
	{
		ListCoins.ins = this;
	}

	public void showCoin(Vector3 vec)
	{
		this.rd = UnityEngine.Random.Range(2, 5);
		for (int i = 0; i < this.rd; i++)
		{
			this.coin = this.coins[0];
			this.coins.RemoveAt(0);
			this.coin.OnShow(vec);
			this.coins.Add(this.coin);
		}
	}

	public List<Coin> coins;

	private int rd;

	public static ListCoins ins;

	private Coin coin;
}
