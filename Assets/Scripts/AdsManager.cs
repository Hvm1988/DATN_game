using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdsManager : MonoBehaviour
{

	private void Awake()
	{
		if (AdsManager.adsManager == null)
		{
			AdsManager.adsManager = this;
		}

		AdsManager.adsManager = this;
		this.RequestInterstitial();

		//Rewa reded Video
		this.RequestRewardBasedVideo();
		//Rewa reded Video
	}
	//
	//
	//
	//
	//Intersti
	public void RequestInterstitial()
	{
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
		string adUnitId = "unexpected_platform";
#endif

		// Initialize an InterstitialAd.
		this.interstitial = new InterstitialAd(adUnitId);

		// Called when an ad request has successfully loaded.
		this.interstitial.OnAdLoaded += HandleOnAdLoaded;
		// Called when an ad request failed to load.
		this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		// Called when an ad is shown.
		this.interstitial.OnAdOpening += HandleOnAdOpened;
		// Called when the ad is closed.
		this.interstitial.OnAdClosed += HandleOnAdClosed;
		// Called when the ad click caused the user to leave the application.

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		this.interstitial.LoadAd(request);
	}

	public void ShowInterstitial()
	{
		if (this.interstitial != null && this.interstitial.IsLoaded())
		{
			UnityEngine.Debug.Log("call show ads");
			this.interstitial.Show();
		}
		else
		{
			UnityEngine.Debug.LogError("Interstitial is not ready yet.");
		}
	}

	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received");
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{

	}

	public void HandleOnAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received");
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("Ads HandleInterstitialClosed Full-Ads event received");
		this.RequestInterstitial();
		if (this.onDoneInterstitial != null)
		{
			this.onDoneInterstitial();
		}
	}

	public void HandleOnAdLeavingApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLeavingApplication event received");
	}


	//
	//
	//
	//
	//Intersti

	//
	//
	//Banner

	public void RequestAdsBanner(bool top)
	{

#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
		string adUnitId = "unexpected_platform";
#endif

		if (top)
		{
			this.adsBanner = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
		}
		else
		{
			this.adsBanner = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
		}

		// Called when an ad request has successfully loaded.
		this.adsBanner.OnAdLoaded += this.HandleAdLoaded;
		// Called when an ad request failed to load.
		this.adsBanner.OnAdFailedToLoad += this.HandleAdFailedToLoad;
		// Called when the user returned from the app after an ad click.
		this.adsBanner.OnAdClosed += this.HandleAdClosed;
		// Called when the ad click caused the user to leave the application.

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();

		// Load the banner with the request.
		this.adsBanner.LoadAd(request);
	}

	public void ShowBanner()
	{
		if (this.adsBanner != null)
		{
			this.adsBanner.Show();
		}
		else
		{
			UnityEngine.Debug.LogError("Interstitial is not ready yet.");
		}
	}

	public void closeBanner()
	{
		if (this.adsBanner != null)
		{
			this.adsBanner.Hide();
		}
	}

	public void destroyBanner()
	{
		if (this.adsBanner != null)
		{
			this.adsBanner.Destroy();
		}
	}

	public void HandleAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("Ads HandleAdLoaded Banner event received.");
	}

	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
	}

	public void HandleAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("Ads HandleAdOpened  Banner event received");
	}

	private void HandleAdClosing(object sender, EventArgs args)
	{
		MonoBehaviour.print("Ads HandleAdClosing  Banner event received");
	}

	public void HandleAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("Ads HandleAdClosed  Banner event received");
		this.RequestAdsBanner(true);
	}

	public void HandleAdLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("Ads HandleAdLeftApplication  Banner event received");
	}

	//
	//
	//
	//
	//Banner

	//
	//
	//
	//
	//Rewa reded Video
	private void RequestRewardBasedVideo()
	{
		string adUnitId;
#if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
		adUnitId = "unexpected_platform";
#endif

		this.rewardedAd = new RewardedAd(adUnitId);

		// Called when an ad request has successfully loaded.
		this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
		// Called when an ad request failed to load.
		// Called when an ad is shown.
		this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
		// Called when an ad request failed to show.
		this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
		// Called when the user should be rewarded for interacting with the ad.
		this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
		// Called when the ad is closed.
		this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the rewarded ad with the request.
		this.rewardedAd.LoadAd(request);
	}

	public void showRewardVideo(Action callBackReward)
	{
		if (this.rewardedAd.IsLoaded())
		{
			this.onDoneRewardVideo = callBackReward;
			this.checkReward = false;
			this.rewardedAd.Show();
		}
	}
	public void showRewardVideo2()
	{
		if (this.rewardedAd.IsLoaded())
		{
			this.rewardedAd.Show();
		}
	}



	public void HandleRewardedAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdLoaded event received");
	}

	public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardedAdFailedToLoad event received with message: "
							 + args.Message);
	}

	public void HandleRewardedAdOpening(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdOpening event received");
	}

	public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardedAdFailedToShow event received with message: "
							 + args.Message);
	}

	public void HandleRewardedAdClosed(object sender, EventArgs args)
	{
		if (AdNumbers == 1)
		{
			MonoBehaviour.print("has been closed.");
			this.RequestRewardBasedVideo();
			if (this.checkReward && this.onDoneRewardVideo != null)
			{
				this.onDoneRewardVideo();
				UnityEngine.Debug.Log("end ads");
			}
		}
		if (AdNumbers == 3)
		{
			MonoBehaviour.print("has been closed.");
			SpecialGift.spe.watchVideoSuccess();
			this.RequestRewardBasedVideo();
		}
		else
		{
			if (GameManager.Instance != null)
			{
                this.RequestRewardBasedVideo();
                GameManager.Instance.watchVideoSuccess();
            }
		}

	}

	public void HandleUserEarnedReward(object sender, Reward args)
	{
		if (AdNumbers == 1)
		{
			this.checkReward = true;

		}
		else
		{

		}
	}

	//
	//
	//
	//
	//Rewa reded Video
	public static int AdNumbers;

	public static AdsManager adsManager;

	public InterstitialAd interstitial;

	public RewardedAd rewardedAd;

	public bool checkReward;

	public Action onDoneInterstitial;

	public Action onDoneRewardVideo;

	private BannerView adsBanner;
}