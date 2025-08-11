using System;
using System.Collections.Generic;
//using Facebook.Unity;
using LitJson;
using UnityEngine;

public class YFB : MonoBehaviour
{
	private void Awake()
	{
		YFB.ins = this;
		this.FBInit();
	}

	public void FBInit()
	{
		//if (!FB.IsInitialized)
		//{
		//	UnityEngine.Debug.Log("Init FB");
		//	FB.Init(new InitDelegate(this.InitCallback), new HideUnityDelegate(this.OnHideUnity), null);
		//}
		//else
		//{
		//	FB.ActivateApp();
		//}
	}

	private void OnHideUnity(bool isGameShown)
	{
		if (!isGameShown)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	private void InitCallback()
	{
		//if (!FB.IsInitialized)
		//{
		//	UnityEngine.Debug.Log("Failed to Initialize the Facebook SDK");
		//}
	}

	public void login()
	{
		//List<string> permissions = new List<string>
		//{
		//	"public_profile",
		//	"email",
		//	"user_friends"
		//};
		//FB.LogInWithReadPermissions(permissions, new FacebookDelegate<ILoginResult>(this.AuthCallback));
	}

	//private void AuthCallback(ILoginResult result)
	//{
	//	if (FB.IsLoggedIn)
	//	{
	//		AccessToken currentAccessToken = AccessToken.CurrentAccessToken;
	//		YFB.accessToken = currentAccessToken.ToString();
	//		foreach (string text in currentAccessToken.Permissions)
	//		{
	//		}
	//		this.getMe();
	//	}
	//	else
	//	{
	//		UnityEngine.Debug.Log("User cancelled login");
	//	}
	//}

	public void getMe()
	{
		//FB.API("me/?fields=id,name,picture.type(large)", HttpMethod.GET, delegate(IGraphResult result)
		//{
		//	JsonData jsonData = JsonMapper.ToObject(result.RawResult);
		//	PlayerPrefs.SetString("USERFBID", jsonData["id"].ToString());
		//	if (this.loginCallBack != null)
		//	{
		//		UnityEngine.Debug.Log("Login call back");
		//		this.loginCallBack();
		//	}
		//	if (this.loginCallBack != null)
		//	{
		//		this.loginCallBack();
		//	}
		//}, null);
	}

	public static string accessToken;

	public static YFB ins;

	public Action loginCallBack;
}
