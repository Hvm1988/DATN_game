using System;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
	private void Awake()
	{
		if (DataHolder.selectedMap == 0)
		{
			DataHolder.selectedMap = 1;
		}
		if (DataHolder.selectedMap == 1)
		{
			for (int i = 0; i < this.bg1.Length; i++)
			{
				this.bg1[i].SetActive(true);
			}
		}
		else if (DataHolder.selectedMap == 2)
		{
			for (int j = 0; j < this.bg1.Length; j++)
			{
				this.bg2[j].SetActive(true);
			}
		}
	}

	public GameObject[] bg1;

	public GameObject[] bg2;
}
