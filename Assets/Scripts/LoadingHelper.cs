using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingHelper : MonoBehaviour
{
	private void Start()
	{
		base.StartCoroutine(this.loadSceneCor());
	}

	private IEnumerator timeOutCor()
	{
		yield return new WaitForSeconds(15f);
		if (this.loadedFirebase)
		{
			yield break;
		}
		UnityEngine.Debug.Log("timeOutCor");
		this.dataHolder.timeOutFireBase();
		try
		{
			this.sceneAO.allowSceneActivation = true;
		}
		catch (Exception ex)
		{
			SceneManager.LoadScene(this.sceneName);
		}
		base.StopAllCoroutines();
		yield break;
	}

	private IEnumerator loadSceneCor()
	{
		this.sceneAO = SceneManager.LoadSceneAsync(this.sceneName);
		this.sceneAO.allowSceneActivation = false;
		while (!this.sceneAO.isDone)
		{
			this.loadingBar.fillAmount = this.sceneAO.progress;
			this.loadingStatus.text = "Loading " + (int)(this.loadingBar.fillAmount * 100f) + "%";
			if (this.sceneAO.progress >= 0.9f)
			{
				this.loadingBar.fillAmount = 1f;
				this.loadingStatus.text = "Loading " + (int)(this.loadingBar.fillAmount * 100f) + "%";
				this.sceneAO.allowSceneActivation = true;
			}
			yield return null;
		}
		yield break;
	}

	public void updateLoadingStatus(string str)
	{
		this.loadingStatus.text = "----" + str;
	}

	public Image loadingBar;

	public Text loadingStatus;

	public DataHolder dataHolder;

	private AsyncOperation sceneAO;

	public string sceneName;

	private bool loadedFirebase;
}
