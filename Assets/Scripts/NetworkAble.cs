using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkAble : MonoBehaviour
{
	public IEnumerator callAPI(string URL, Dictionary<string, string> datas, Action<string> onSuccess, Action<string> onFail)
	{
		this.isRequestDone = false;
		WWWForm body = new WWWForm();
		foreach (KeyValuePair<string, string> keyValuePair in datas)
		{
			body.AddField(keyValuePair.Key, keyValuePair.Value);
		}
		UnityWebRequest request = UnityWebRequest.Post(URL, body);
		yield return request.Send();
		this.isRequestDone = true;
		if (request.isNetworkError)
		{
			UnityEngine.Debug.Log("error");
			if (onFail != null)
			{
				onFail("Error");
			}
		}
		else if (onSuccess != null)
		{
			onSuccess(request.downloadHandler.text);
		}
		yield break;
	}

	public IEnumerator timeOut(Action callback)
	{
		yield return new WaitForSeconds(10f);
		if (!this.isRequestDone && callback != null)
		{
			callback();
		}
		yield break;
	}

	public bool isRequestDone;
}
