using System;
using UnityEngine;

namespace AudienceNetwork
{
	internal class NativeBannerAdBridge : NativeAdBridge
	{
		private static NativeAdBridge createInstance()
		{
			if (Application.platform != RuntimePlatform.OSXEditor)
			{
				return new NativeAdBridgeAndroid();
			}
			return new NativeAdBridge();
		}

		public new static NativeAdBridge Instance = NativeBannerAdBridge.createInstance();
	}
}
