using System;
using UnityEngine;

public class BoxGameStart : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals("hero") && base.name.Equals("boxGameStart"))
		{
			this._gameManager.startGame();
		}
	}

	public GameManager _gameManager;
}
