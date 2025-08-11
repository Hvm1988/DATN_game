using System;
using UnityEngine;

public class AutoDeactive : MonoBehaviour
{
	public void deactive()
	{
		base.gameObject.SetActive(false);
	}
}
