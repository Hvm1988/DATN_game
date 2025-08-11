using System;
using UnityEngine;

public abstract class Notifier : MonoBehaviour
{
	public abstract void setUI();

	private void Start()
	{
		this.refresh();
	}

	public void refresh()
	{
		this.setUI();
	}

	public GameObject redNote;

	public int counter;
}
