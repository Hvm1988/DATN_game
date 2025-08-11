using System;
using UnityEngine;

namespace AudienceNetwork
{
	public sealed class NativeAd : NativeAdBase, IDisposable
	{
		public NativeAd(string placementId) : base(placementId)
		{
			this.nativeAdType = NativeAdType.NativeAd;
			this.uniqueId = this.NativeAdBridgeInstance().Create(placementId, this);
		}

		public int RegisterGameObjectsForInteraction(RectTransform mediaViewRectTransform, RectTransform ctaRectTransform, RectTransform iconViewRectTransform = null, Camera camera = null)
		{
			return base.baseRegisterGameObjectsForInteraction(mediaViewRectTransform, ctaRectTransform, iconViewRectTransform, camera);
		}
	}
}
