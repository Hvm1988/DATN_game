using System;
using UnityEngine;

public class ResourceImage : MonoBehaviour
{
	private void Awake()
	{
		ResourceImage.ins = this;
	}

	public static ResourceImage ins;

	public Sprite[] numberWhite;

	public Sprite[] numberRed;

	public Sprite[] numberGreen;

	public Sprite[] numberGold;

	public Sprite[] numberPink;

	public Sprite[] itemsImage;
}
