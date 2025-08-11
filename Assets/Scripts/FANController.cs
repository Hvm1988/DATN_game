using System;
using AudienceNetwork;
using UnityEngine;

public class FANController : MonoBehaviour
{
	private void Awake()
	{
		this.LoadInterstitial();
		this.LoadRewardedVideo();
	}

	public void LoadInterstitial()
	{
		this.interstitialAd = new InterstitialAd("210467796349702_210475233015625");
		this.interstitialAd.Register(base.gameObject);
		this.interstitialAd.InterstitialAdDidLoad = delegate()
		{
			UnityEngine.Debug.Log("Interstitial ad loaded.");
			this.isLoadedInterstitial = true;
			this.didCloseInterstitial = false;
		};
		this.interstitialAd.InterstitialAdDidFailWithError = delegate(string error)
		{
			UnityEngine.Debug.Log("Interstitial ad failed to load with error: " + error);
		};
		this.interstitialAd.InterstitialAdWillLogImpression = delegate()
		{
			UnityEngine.Debug.Log("Interstitial ad logged impression.");
		};
		this.interstitialAd.InterstitialAdDidClick = delegate()
		{
			UnityEngine.Debug.Log("Interstitial ad clicked.");
		};
		this.interstitialAd.interstitialAdDidClose = delegate()
		{
			UnityEngine.Debug.Log("Interstitial ad did close.");
			this.didCloseInterstitial = true;
			if (this.interstitialAd != null)
			{
				this.interstitialAd.Dispose();
				this.LoadInterstitial();
			}
		};
		this.interstitialAd.interstitialAdActivityDestroyed = delegate()
		{
			if (!this.didCloseInterstitial)
			{
				UnityEngine.Debug.Log("Interstitial activity destroyed without being closed first.");
				UnityEngine.Debug.Log("Game should resume.");
			}
		};
		this.interstitialAd.LoadAd();
	}

	public void ShowInterstitial()
	{
		if (this.isLoadedInterstitial)
		{
			this.interstitialAd.Show();
			this.isLoadedInterstitial = false;
		}
	}

	private void OnDestroy()
	{
		if (this.interstitialAd != null)
		{
			this.interstitialAd.Dispose();
		}
		UnityEngine.Debug.Log("InterstitialAdTest was destroyed!");
		if (this.rewardedVideoAd != null)
		{
			this.rewardedVideoAd.Dispose();
		}
		UnityEngine.Debug.Log("RewardedVideoAdTest was destroyed!");
	}

	public void LoadRewardedVideo()
	{
		this.rewardedVideoAd = new RewardedVideoAd("YOUR_PLACEMENT_ID");
		RewardData rewardData = new RewardData();
		rewardData.UserId = "USER_ID";
		rewardData.Currency = "REWARD_ID";
		this.rewardedVideoAd.Register(base.gameObject);
		this.rewardedVideoAd.RewardedVideoAdDidLoad = delegate()
		{
			UnityEngine.Debug.Log("RewardedVideo ad loaded.");
			this.isLoadedRewardVideo = true;
			this.didCloseRewardVideo = false;
		};
		this.rewardedVideoAd.RewardedVideoAdDidFailWithError = delegate(string error)
		{
			UnityEngine.Debug.Log("RewardedVideo ad failed to load with error: " + error);
		};
		this.rewardedVideoAd.RewardedVideoAdWillLogImpression = delegate()
		{
			UnityEngine.Debug.Log("RewardedVideo ad logged impression.");
		};
		this.rewardedVideoAd.RewardedVideoAdDidClick = delegate()
		{
			UnityEngine.Debug.Log("RewardedVideo ad clicked.");
		};
		this.rewardedVideoAd.RewardedVideoAdDidSucceed = delegate()
		{
			UnityEngine.Debug.Log("Rewarded video ad validated by server");
		};
		this.rewardedVideoAd.RewardedVideoAdDidFail = delegate()
		{
			UnityEngine.Debug.Log("Rewarded video ad not validated, or no response from server");
		};
		this.rewardedVideoAd.rewardedVideoAdDidClose = delegate()
		{
			UnityEngine.Debug.Log("Rewarded video ad did close.");
			this.didCloseRewardVideo = true;
			if (this.rewardedVideoAd != null)
			{
				this.rewardedVideoAd.Dispose();
			}
			if (this.callBackReward != null)
			{
				this.callBackReward();
			}
			this.LoadRewardedVideo();
		};
		this.rewardedVideoAd.rewardedVideoAdActivityDestroyed = delegate()
		{
			if (!this.didCloseRewardVideo)
			{
				UnityEngine.Debug.Log("Rewarded video activity destroyed without being closed first.");
				UnityEngine.Debug.Log("Game should resume. User should not get a reward.");
			}
		};
		this.rewardedVideoAd.LoadAd();
	}

	public void ShowRewardedVideo(Action callBackReward)
	{
		if (this.isLoadedRewardVideo)
		{
			UnityEngine.Debug.Log("callBackFAN");
			this.callBackReward = callBackReward;
			this.rewardedVideoAd.Show();
			this.isLoadedRewardVideo = false;
		}
	}

	private InterstitialAd interstitialAd;

	private bool didCloseInterstitial;

	public bool isLoadedInterstitial;

	private RewardedVideoAd rewardedVideoAd;

	public bool isLoadedRewardVideo;

	private bool didCloseRewardVideo;

	public Action callBackReward;
}
