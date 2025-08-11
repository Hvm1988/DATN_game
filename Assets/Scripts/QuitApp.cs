using System;
using UnityEngine;
using UnityEngine.UI;

public class QuitApp : MonoBehaviour
{
	private void OnEnable()
	{
		if (DataHolder.Instance.playerData.clickedRate)
		{
			this.rateObjs.SetActive(false);
			this.quitObjs.SetActive(true);
			this.messageTxt.text = "See you again !";
		}
		else
		{
			this.rateObjs.SetActive(true);
			this.quitObjs.SetActive(false);
			this.messageTxt.text = "Have fun?If you like our game, please rate us";
		}
	}

	public void quitApp()
	{
		Application.Quit();
	}

	private const string rateString = "Have fun?If you like our game, please rate us";

	private const string quitString = "See you again !";

	public Text messageTxt;

	public GameObject rateObjs;

	public GameObject quitObjs;
}
