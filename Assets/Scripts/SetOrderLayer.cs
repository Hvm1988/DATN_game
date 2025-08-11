using System;
using UnityEngine;

public class SetOrderLayer : MonoBehaviour
{
	private void Start()
	{
		base.GetComponent<TrailRenderer>().sortingOrder = 12;
	}

	private void Update()
	{
	}
}
