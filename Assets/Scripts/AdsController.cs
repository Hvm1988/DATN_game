using System;
using UnityEngine;

public class AdsController : MonoBehaviour
{
	private void Awake()
	{
		if (AdsController.Instance == null)
		{
			AdsController.Instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void showAds()
	{
		if (this.adsManager.interstitial != null && this.adsManager.interstitial.IsLoaded())
		{
			this.adsManager.ShowInterstitial();
			DataHolder.Instance.playerData.addRuby(3);
			DataHolder.Instance.willShow3Ruby = true;
		}
		//else if (this.fANController.isLoadedInterstitial)
		//{
		//	this.fANController.ShowInterstitial();
		//	DataHolder.Instance.willShow3Ruby = true;
		//	DataHolder.Instance.playerData.addRuby(3);
		//}
	}

	public void showRewardVideo(Action callBackReward, Action callBackFail)
	{
		//if (this.fANController.isLoadedRewardVideo)
		//{
		//	this.fANController.ShowRewardedVideo(callBackReward);
		//	return;
		//}
		/*
		if (UnityAds.unityAds.isReady())
		{
			UnityAds.unityAds.showRewardedAd(delegate
			{
				if (callBackReward != null)
				{
					callBackReward();
				}
			}, null);
			return;
		}
		*/
		if (this.adsManager.rewardedAd.IsLoaded())
		{
			this.adsManager.showRewardVideo(callBackReward);
			return;
		}
		if (callBackFail != null)
		{
			callBackFail();
		}
	}


	public static AdsController Instance;

	public AdsManager adsManager;

	//public FANController fANController;
}
