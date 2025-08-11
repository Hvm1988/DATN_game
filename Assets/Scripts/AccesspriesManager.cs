using System;
using UnityEngine;
using UnityEngine.UI;

public class AccesspriesManager : MonoBehaviour
{
	public void openItemTab()
	{
		this.itemBtn.onClick.Invoke();
	}

	public void openMatTab()
	{
		this.matBtn.onClick.Invoke();
	}

	public GameObject notEnougtRuby;

	public Button itemBtn;

	public Button matBtn;
}
