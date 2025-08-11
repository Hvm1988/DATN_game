using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteChange : MonoBehaviour
{
	public void change(int id)
	{
		if (this.button == null)
		{
			this.button = base.GetComponent<Button>();
		}
		this.button.image.sprite = this.sprites[id];
	}

	public Sprite[] sprites;

	public Button button;
}
