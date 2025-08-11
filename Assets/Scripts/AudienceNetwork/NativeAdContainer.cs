using System;
using UnityEngine;

namespace AudienceNetwork
{
	internal class NativeAdContainer
	{
		internal NativeAdContainer(NativeAdBase nativeAd)
		{
			this.nativeAd = nativeAd;
		}

		internal NativeAdBase nativeAd { get; set; }

		internal FBNativeAdBridgeCallback onLoad { get; set; }

		internal FBNativeAdBridgeCallback onImpression { get; set; }

		internal FBNativeAdBridgeCallback onClick { get; set; }

		internal FBNativeAdBridgeErrorCallback onError { get; set; }

		internal FBNativeAdBridgeCallback onFinishedClick { get; set; }

		internal FBNativeAdBridgeCallback onMediaDownload { get; set; }

		public static implicit operator bool(NativeAdContainer obj)
		{
			return !object.ReferenceEquals(obj, null);
		}

		internal AndroidJavaProxy listenerProxy;

		internal AndroidJavaObject bridgedNativeAd;

		internal AndroidJavaObject context;

		internal AndroidJavaObject mediaView;

		internal AndroidJavaObject iconView;

		internal AndroidJavaObject ctaView;
	}
}
