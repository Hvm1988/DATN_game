using System;
using AudienceNetwork;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RewardedVideoAdTest : MonoBehaviour
{
	public void LoadRewardedVideo()
	{
		this.statusLabel.text = "Loading rewardedVideo ad...";
		this.rewardedVideoAd = new RewardedVideoAd("YOUR_PLACEMENT_ID");
		RewardedVideoAd rewardedVideoAd = new RewardedVideoAd("YOUR_PLACEMENT_ID", new RewardData
		{
			UserId = "USER_ID",
			Currency = "REWARD_ID"
		});
		this.rewardedVideoAd.Register(base.gameObject);
		this.rewardedVideoAd.RewardedVideoAdDidLoad = delegate()
		{
			UnityEngine.Debug.Log("RewardedVideo ad loaded.");
			this.isLoaded = true;
			this.didClose = false;
			this.statusLabel.text = "Ad loaded. Click show to present!";
		};
		this.rewardedVideoAd.RewardedVideoAdDidFailWithError = delegate(string error)
		{
			UnityEngine.Debug.Log("RewardedVideo ad failed to load with error: " + error);
			this.statusLabel.text = "RewardedVideo ad failed to load. Check console for details.";
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
			this.didClose = true;
			if (this.rewardedVideoAd != null)
			{
				this.rewardedVideoAd.Dispose();
			}
		};
		this.rewardedVideoAd.rewardedVideoAdActivityDestroyed = delegate()
		{
			if (!this.didClose)
			{
				UnityEngine.Debug.Log("Rewarded video activity destroyed without being closed first.");
				UnityEngine.Debug.Log("Game should resume. User should not get a reward.");
			}
		};
		this.rewardedVideoAd.LoadAd();
	}

	public void ShowRewardedVideo()
	{
		if (this.isLoaded)
		{
			this.rewardedVideoAd.Show();
			this.isLoaded = false;
			this.statusLabel.text = string.Empty;
		}
		else
		{
			this.statusLabel.text = "Ad not loaded. Click load to request an ad.";
		}
	}

	private void OnDestroy()
	{
		if (this.rewardedVideoAd != null)
		{
			this.rewardedVideoAd.Dispose();
		}
		UnityEngine.Debug.Log("RewardedVideoAdTest was destroyed!");
	}

	public void NextScene()
	{
		SceneManager.LoadScene("NativeAdScene");
	}

	private RewardedVideoAd rewardedVideoAd;

	private bool isLoaded;

	private bool didClose;

	public Text statusLabel;
}
