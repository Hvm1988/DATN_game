using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackupData : NetworkAble
{
	public void save()
	{
		//if (PlayerPrefs.GetString("USERFBID").Equals(string.Empty))
		//{
		//	this.statusHolder.SetActive(false);
		//	this.loginFBHolder.SetActive(true);
		//	YFB.ins.loginCallBack = new Action(this.save);
		//	return;
		//}
		//this.loginFBHolder.SetActive(false);
		//this.isRequestDone = false;
		//base.StartCoroutine(base.timeOut(new Action(this.save)));
		//this.status.text = "Progressing ...";
		//this.okBtn.gameObject.SetActive(false);
		//this.tryAgain.gameObject.SetActive(false);
		//this.cancel.gameObject.SetActive(false);
		//this.statusHolder.SetActive(true);
		//Dictionary<string, string> dictionary = new Dictionary<string, string>();
		//dictionary.Add("game_id", "dragon-fight2");
		//dictionary.Add("device_id", PlayerPrefs.GetString("USERFBID"));
		//dictionary.Add("dataSave", JsonUtility.ToJson(DataHolder.Instance.playerData));
		//string url = "http://";
		//base.StartCoroutine(base.callAPI(url, dictionary, new Action<string>(this.onSaveSuccess), new Action<string>(this.onSaveFail)));
	}

	public void load()
	{
		//if (PlayerPrefs.GetString("USERFBID").Equals(string.Empty))
		//{
		//	this.statusHolder.SetActive(false);
		//	this.loginFBHolder.SetActive(true);
		//	YFB.ins.loginCallBack = new Action(this.save);
		//	return;
		//}
		//this.loginFBHolder.SetActive(false);
		//this.isRequestDone = false;
		//base.StartCoroutine(base.timeOut(new Action(this.save)));
		//this.status.text = "Progressing ...";
		//this.okBtn.gameObject.SetActive(false);
		//this.tryAgain.gameObject.SetActive(false);
		//this.cancel.gameObject.SetActive(false);
		//this.statusHolder.SetActive(true);
		//Dictionary<string, string> dictionary = new Dictionary<string, string>();
		//dictionary.Add("game_id", "dragon-fight2");
		//dictionary.Add("device_id", PlayerPrefs.GetString("USERFBID"));
		//string url = "http://";
		//base.StartCoroutine(base.callAPI(url, dictionary, new Action<string>(this.onLoadSuccess), new Action<string>(this.onLoadFail)));
	}

	private void onSaveSuccess(string result)
	{
		UnityEngine.Debug.Log("onSaveSuccess: " + result);
		if (result.Contains("success"))
		{
			this.status.text = "Save to cloud success !";
			this.okBtn.gameObject.SetActive(true);
			this.tryAgain.gameObject.SetActive(false);
			this.cancel.gameObject.SetActive(false);
			base.StopCoroutine(base.timeOut(null));
		}
		else
		{
			this.onSaveFail(string.Empty);
		}
		base.StopCoroutine(base.timeOut(null));
	}

	private void onSaveFail(string result)
	{
		try
		{
			this.status.text = "Failed, do you want to try again ?";
			this.okBtn.gameObject.SetActive(false);
			this.tryAgain.gameObject.SetActive(true);
			this.cancel.gameObject.SetActive(true);
			this.tryAgain.onClick.RemoveAllListeners();
			this.tryAgain.onClick.AddListener(delegate()
			{
				this.save();
			});
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
		}
		base.StopCoroutine(base.timeOut(null));
	}

	private void onLoadSuccess(string result)
	{
		UnityEngine.Debug.Log(result);
		try
		{
			DataHolder.Instance.playerData.loadFromJson(result);
			this.status.text = "Load from cloud success !";
			this.okBtn.gameObject.SetActive(true);
			this.tryAgain.gameObject.SetActive(false);
			this.cancel.gameObject.SetActive(false);
			base.StopCoroutine(base.timeOut(null));
		}
		catch (Exception ex)
		{
			this.onLoadFail("error");
		}
	}

	private void onLoadFail(string result)
	{
		this.status.text = "Failed, do you want to try again ?";
		this.okBtn.gameObject.SetActive(false);
		this.tryAgain.gameObject.SetActive(true);
		this.cancel.gameObject.SetActive(true);
		this.tryAgain.onClick.RemoveAllListeners();
		this.tryAgain.onClick.AddListener(delegate()
		{
			this.load();
		});
		base.StopCoroutine(base.timeOut(null));
	}

	public const string gameID = "dragon-fight2";

	public GameObject loginFBHolder;

	public Text status;

	public Button okBtn;

	public Button tryAgain;

	public Button cancel;

	public GameObject statusHolder;
}
