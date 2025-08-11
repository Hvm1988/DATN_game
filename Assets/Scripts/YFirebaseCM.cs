using System;
using UnityEngine;

public class YFirebaseCM : MonoBehaviour
{
    public static YFirebaseCM THIS;
    private void Awake()
	{
        if (YFirebaseCM.THIS == null)
        {
            YFirebaseCM.THIS = this;
            UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
        } else
        {
            UnityEngine.Object.Destroy(base.gameObject);
        }
	}

	public void Start()
	{
		//FirebaseMessaging.TokenReceived += this.OnTokenReceived;
		//FirebaseMessaging.MessageReceived += this.OnMessageReceived;
	}

	//public void OnTokenReceived(object sender, TokenReceivedEventArgs token)
	//{
	//	UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
	//}

	//public void OnMessageReceived(object sender, MessageReceivedEventArgs e)
	//{
	//	UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
	//}
}
