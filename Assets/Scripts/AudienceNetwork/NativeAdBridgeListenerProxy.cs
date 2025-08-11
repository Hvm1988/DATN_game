using System;
using UnityEngine;

namespace AudienceNetwork
{
	internal class NativeAdBridgeListenerProxy : AndroidJavaProxy
	{
		public NativeAdBridgeListenerProxy(NativeAdBase nativeAd, AndroidJavaObject bridgedNativeAd) : base("com.facebook.ads.NativeAdListener")
		{
			this.nativeAd = nativeAd;
			this.bridgedNativeAd = bridgedNativeAd;
		}

		private void onError(AndroidJavaObject ad, AndroidJavaObject error)
		{
			string errorMessage = error.Call<string>("getErrorMessage", new object[0]);
			this.nativeAd.executeOnMainThread(delegate
			{
				if (this.nativeAd.NativeAdDidFailWithError != null)
				{
					this.nativeAd.NativeAdDidFailWithError(errorMessage);
				}
			});
		}

		private void onAdLoaded(AndroidJavaObject ad)
		{
			this.nativeAd.executeOnMainThread(delegate
			{
				this.nativeAd.loadAdFromData();
			});
		}

		private void onAdClicked(AndroidJavaObject ad)
		{
			this.nativeAd.executeOnMainThread(delegate
			{
				if (this.nativeAd.NativeAdDidClick != null)
				{
					this.nativeAd.NativeAdDidClick();
				}
			});
		}

		private void onLoggingImpression(AndroidJavaObject ad)
		{
			this.nativeAd.executeOnMainThread(delegate
			{
				if (this.nativeAd.NativeAdWillLogImpression != null)
				{
					this.nativeAd.NativeAdWillLogImpression();
				}
			});
		}

		private void onMediaDownloaded(AndroidJavaObject ad)
		{
			this.nativeAd.executeOnMainThread(delegate
			{
				if (this.nativeAd.NativeAdDidDownloadMedia != null)
				{
					this.nativeAd.NativeAdDidDownloadMedia();
				}
			});
		}

		private NativeAdBase nativeAd;

		protected AndroidJavaObject bridgedNativeAd;
	}
}
