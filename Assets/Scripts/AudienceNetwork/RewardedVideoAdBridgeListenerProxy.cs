using System;
using UnityEngine;

namespace AudienceNetwork
{
	internal class RewardedVideoAdBridgeListenerProxy : AndroidJavaProxy
	{
		public RewardedVideoAdBridgeListenerProxy(RewardedVideoAd rewardedVideoAd, AndroidJavaObject bridgedRewardedVideoAd) : base("com.facebook.ads.S2SRewardedVideoAdExtendedListener")
		{
			this.rewardedVideoAd = rewardedVideoAd;
			this.bridgedRewardedVideoAd = bridgedRewardedVideoAd;
		}

		private void onError(AndroidJavaObject ad, AndroidJavaObject error)
		{
			string error2 = error.Call<string>("getErrorMessage", new object[0]);
			if (this.rewardedVideoAd.RewardedVideoAdDidFailWithError != null)
			{
				this.rewardedVideoAd.RewardedVideoAdDidFailWithError(error2);
			}
		}

		private void onAdLoaded(AndroidJavaObject ad)
		{
			if (this.rewardedVideoAd.RewardedVideoAdDidLoad != null)
			{
				this.rewardedVideoAd.RewardedVideoAdDidLoad();
			}
		}

		private void onAdClicked(AndroidJavaObject ad)
		{
			if (this.rewardedVideoAd.RewardedVideoAdDidClick != null)
			{
				this.rewardedVideoAd.RewardedVideoAdDidClick();
			}
		}

		private void onRewardedVideoDisplayed(AndroidJavaObject ad)
		{
			if (this.rewardedVideoAd.RewardedVideoAdWillLogImpression != null)
			{
				this.rewardedVideoAd.RewardedVideoAdWillLogImpression();
			}
		}

		private void onRewardedVideoClosed()
		{
			if (this.rewardedVideoAd.RewardedVideoAdDidClose != null)
			{
				this.rewardedVideoAd.RewardedVideoAdDidClose();
			}
		}

		private void onRewardedVideoCompleted()
		{
			if (this.rewardedVideoAd.RewardedVideoAdComplete != null)
			{
				this.rewardedVideoAd.RewardedVideoAdComplete();
			}
		}

		private void onRewardServerSuccess()
		{
			if (this.rewardedVideoAd.RewardedVideoAdDidSucceed != null)
			{
				this.rewardedVideoAd.RewardedVideoAdDidSucceed();
			}
		}

		private void onRewardServerFailed()
		{
			if (this.rewardedVideoAd.RewardedVideoAdDidFail != null)
			{
				this.rewardedVideoAd.RewardedVideoAdDidFail();
			}
		}

		private void onLoggingImpression(AndroidJavaObject ad)
		{
			if (this.rewardedVideoAd.RewardedVideoAdWillLogImpression != null)
			{
				this.rewardedVideoAd.RewardedVideoAdWillLogImpression();
			}
		}

		private void onRewardedVideoActivityDestroyed()
		{
			if (this.rewardedVideoAd.RewardedVideoAdActivityDestroyed != null)
			{
				this.rewardedVideoAd.RewardedVideoAdActivityDestroyed();
			}
		}

		private RewardedVideoAd rewardedVideoAd;

		private AndroidJavaObject bridgedRewardedVideoAd;
	}
}
