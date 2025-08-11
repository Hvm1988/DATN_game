using System;
using UnityEngine;

namespace AudienceNetwork
{
	public sealed class NativeBannerAd : NativeAdBase, IDisposable
	{
		public NativeBannerAd(string placementId) : base(placementId)
		{
			this.nativeAdType = NativeAdType.NativeBannerAd;
			this.uniqueId = NativeBannerAdBridge.Instance.Create(placementId, this);
		}

		public int RegisterGameObjectsForInteraction(RectTransform iconViewRectTransform, RectTransform ctaRectTransform, Camera camera = null)
		{
			return base.baseRegisterGameObjectsForInteraction(null, ctaRectTransform, iconViewRectTransform, camera);
		}

		internal override NativeAdBridge NativeAdBridgeInstance()
		{
			return NativeBannerAdBridge.Instance;
		}
	}
}
