using System;
using System.Collections.Generic;
using UnityEngine;

namespace AudienceNetwork
{
	internal class NativeAdBridge
	{
		private static NativeAdBridge createInstance()
		{
			if (Application.platform != RuntimePlatform.OSXEditor)
			{
				return new NativeAdBridgeAndroid();
			}
			return new NativeAdBridge();
		}

		public virtual int Create(string placementId, NativeAdBase nativeAd)
		{
			this.nativeAds.Add(nativeAd);
			return this.nativeAds.Count - 1;
		}

		public virtual int Load(int uniqueId)
		{
			NativeAdBase nativeAdBase = this.nativeAds[uniqueId];
			nativeAdBase.loadAdFromData();
			return uniqueId;
		}

		public virtual int RegisterGameObjectsForInteraction(int uniqueId, Rect mediaViewRect, Rect iconViewRect, Rect ctaViewRect)
		{
			return -1;
		}

		public virtual bool IsValid(int uniqueId)
		{
			return true;
		}

		public virtual string GetAdvertiserName(int uniqueId)
		{
			return "Facebook";
		}

		public virtual string GetHeadline(int uniqueId)
		{
			return "A Facebook Ad";
		}

		public virtual string GetLinkDescription(int uniqueId)
		{
			return "Facebook.com";
		}

		public virtual string GetSponsoredTranslation(int uniqueId)
		{
			return "Sponsored Translation";
		}

		public virtual string GetAdTranslation(int uniqueId)
		{
			return "Ad Translation";
		}

		public virtual string GetPromotedTranslation(int uniqueId)
		{
			return "Promoted Translation";
		}

		public virtual string GetBody(int uniqueId)
		{
			return "Your ad integration works. Woohoo!";
		}

		public virtual string GetCallToAction(int uniqueId)
		{
			return "Install Now";
		}

		public virtual string GetSocialContext(int uniqueId)
		{
			return "Available on the App Store";
		}

		public virtual string GetAdChoicesImageURL(int uniqueId)
		{
			return "https://www.facebook.com/images/ad_network/ad_choices.png";
		}

		public virtual string GetAdChoicesText(int uniqueId)
		{
			return "AdChoices";
		}

		public virtual string GetAdChoicesLinkURL(int uniqueId)
		{
			return "https://m.facebook.com/ads/ad_choices/";
		}

		public virtual void Release(int uniqueId)
		{
		}

		public virtual void OnLoad(int uniqueId, FBNativeAdBridgeCallback callback)
		{
		}

		public virtual void OnImpression(int uniqueId, FBNativeAdBridgeCallback callback)
		{
		}

		public virtual void OnClick(int uniqueId, FBNativeAdBridgeCallback callback)
		{
		}

		public virtual void OnError(int uniqueId, FBNativeAdBridgeErrorCallback callback)
		{
		}

		public virtual void OnFinishedClick(int uniqueId, FBNativeAdBridgeCallback callback)
		{
		}

		public virtual void OnMediaDownloaded(int uniqueId, FBNativeAdBridgeCallback callback)
		{
		}

		public static NativeAdBridge Instance = NativeAdBridge.createInstance();

		private List<NativeAdBase> nativeAds = new List<NativeAdBase>();
	}
}
