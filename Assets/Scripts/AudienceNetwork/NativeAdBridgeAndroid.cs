using System;
using System.Collections.Generic;
using AudienceNetwork.Utility;
using UnityEngine;

namespace AudienceNetwork
{
	internal class NativeAdBridgeAndroid : NativeAdBridge
	{
		protected AndroidJavaObject nativeAdForNativeAdId(int uniqueId)
		{
			NativeAdContainer nativeAdContainer = null;
			bool flag = NativeAdBridgeAndroid.nativeAds.TryGetValue(uniqueId, out nativeAdContainer);
			if (flag)
			{
				return nativeAdContainer.bridgedNativeAd;
			}
			return null;
		}

		protected NativeAdContainer nativeAdContainerForNativeAdId(int uniqueId)
		{
			NativeAdContainer result = null;
			bool flag = NativeAdBridgeAndroid.nativeAds.TryGetValue(uniqueId, out result);
			if (flag)
			{
				return result;
			}
			return null;
		}

		protected string getStringForNativeAdId(int uniqueId, string method)
		{
			AndroidJavaObject androidJavaObject = this.nativeAdForNativeAdId(uniqueId);
			if (androidJavaObject != null)
			{
				return androidJavaObject.Call<string>(method, new object[0]);
			}
			return null;
		}

		public override int Create(string placementId, NativeAdBase nativeAd)
		{
			AdUtility.prepare();
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getApplicationContext", new object[0]);
			string className = string.Empty;
			if (nativeAd.nativeAdType == NativeAdType.NativeAd)
			{
				className = "com.facebook.ads.NativeAd";
			}
			else if (nativeAd.nativeAdType == NativeAdType.NativeBannerAd)
			{
				className = "com.facebook.ads.NativeBannerAd";
			}
			AndroidJavaObject androidJavaObject2 = new AndroidJavaObject(className, new object[]
			{
				androidJavaObject,
				placementId
			});
			NativeAdBridgeListenerProxy nativeAdBridgeListenerProxy = new NativeAdBridgeListenerProxy(nativeAd, androidJavaObject2);
			androidJavaObject2.Call("setAdListener", new object[]
			{
				nativeAdBridgeListenerProxy
			});
			NativeAdContainer nativeAdContainer = new NativeAdContainer(nativeAd);
			nativeAdContainer.bridgedNativeAd = androidJavaObject2;
			nativeAdContainer.listenerProxy = nativeAdBridgeListenerProxy;
			nativeAdContainer.context = androidJavaObject;
			int num = NativeAdBridgeAndroid.lastKey;
			NativeAdBridgeAndroid.nativeAds.Add(num, nativeAdContainer);
			NativeAdBridgeAndroid.lastKey++;
			return num;
		}

		public override int Load(int uniqueId)
		{
			AdUtility.prepare();
			AndroidJavaObject androidJavaObject = this.nativeAdForNativeAdId(uniqueId);
			if (androidJavaObject != null)
			{
				androidJavaObject.Call("loadAd", new object[0]);
			}
			return uniqueId;
		}

		public override int RegisterGameObjectsForInteraction(int uniqueId, Rect mediaViewRect, Rect iconViewRect, Rect ctaViewRect)
		{
			NativeAdContainer nativeAdContainer = this.nativeAdContainerForNativeAdId(uniqueId);
			AndroidJavaObject nativeAd = nativeAdContainer.bridgedNativeAd;
			if (nativeAd != null)
			{
				AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				AndroidJavaObject context = nativeAdContainer.context;
				activity.Call("runOnUiThread", new object[]
				{
					new AndroidJavaRunnable(delegate()
					{
						AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("android.R$id");
						AndroidJavaObject androidJavaObject = activity.Call<AndroidJavaObject>("findViewById", new object[]
						{
							androidJavaClass2.GetStatic<int>("content")
						});
						AndroidJavaObject androidJavaObject2 = this.createViewFromRect(iconViewRect, "com.facebook.ads.AdIconView", context);
						nativeAdContainer.iconView = androidJavaObject2;
						AndroidJavaObject androidJavaObject3 = this.createViewFromRect(ctaViewRect, "android/view/View", context);
						nativeAdContainer.ctaView = androidJavaObject3;
						androidJavaObject.Call("addView", new object[]
						{
							androidJavaObject2
						});
						androidJavaObject.Call("addView", new object[]
						{
							androidJavaObject3
						});
						AndroidJavaObject androidJavaObject4 = new AndroidJavaObject("java.util.ArrayList", new object[0]);
						androidJavaObject4.Call<bool>("add", new object[]
						{
							androidJavaObject3
						});
						if (mediaViewRect != Rect.zero)
						{
							AndroidJavaObject androidJavaObject5 = this.createViewFromRect(mediaViewRect, "com.facebook.ads.MediaView", context);
							nativeAdContainer.mediaView = androidJavaObject5;
							androidJavaObject.Call("addView", new object[]
							{
								androidJavaObject5
							});
							nativeAd.Call("registerViewForInteraction", new object[]
							{
								androidJavaObject,
								androidJavaObject5,
								androidJavaObject2,
								androidJavaObject4
							});
						}
						else
						{
							nativeAd.Call("registerViewForInteraction", new object[]
							{
								androidJavaObject,
								androidJavaObject2,
								androidJavaObject4
							});
						}
					})
				});
			}
			return uniqueId;
		}

		protected AndroidJavaObject createViewFromRect(Rect rect, string type, AndroidJavaObject context)
		{
			AndroidJavaObject androidJavaObject = new AndroidJavaObject(type, new object[]
			{
				context
			});
			androidJavaObject.Call("setX", new object[]
			{
				rect.x
			});
			androidJavaObject.Call("setY", new object[]
			{
				rect.y
			});
			AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("android/view/ViewGroup$LayoutParams", new object[]
			{
				(int)rect.width,
				(int)rect.height
			});
			androidJavaObject.Call("setLayoutParams", new object[]
			{
				androidJavaObject2
			});
			return androidJavaObject;
		}

		public override bool IsValid(int uniqueId)
		{
			AndroidJavaObject androidJavaObject = this.nativeAdForNativeAdId(uniqueId);
			return androidJavaObject != null && androidJavaObject.Call<bool>("isAdLoaded", new object[0]);
		}

		public override string GetAdvertiserName(int uniqueId)
		{
			return this.getStringForNativeAdId(uniqueId, "getAdvertiserName");
		}

		public override string GetHeadline(int uniqueId)
		{
			return this.getStringForNativeAdId(uniqueId, "getAdHeadline");
		}

		public override string GetLinkDescription(int uniqueId)
		{
			return this.getStringForNativeAdId(uniqueId, "getAdLinkDescription");
		}

		public override string GetSponsoredTranslation(int uniqueId)
		{
			return this.getStringForNativeAdId(uniqueId, "getSponsoredTranslation");
		}

		public override string GetAdTranslation(int uniqueId)
		{
			return this.getStringForNativeAdId(uniqueId, "getAdTranslation");
		}

		public override string GetPromotedTranslation(int uniqueId)
		{
			return this.getStringForNativeAdId(uniqueId, "getPromotedTranslation");
		}

		public override string GetBody(int uniqueId)
		{
			return this.getStringForNativeAdId(uniqueId, "getAdBodyText");
		}

		public override string GetCallToAction(int uniqueId)
		{
			return this.getStringForNativeAdId(uniqueId, "getAdCallToAction");
		}

		public override string GetSocialContext(int uniqueId)
		{
			return this.getStringForNativeAdId(uniqueId, "getAdSocialContext");
		}

		public override string GetAdChoicesImageURL(int uniqueId)
		{
			return this.getStringForNativeAdId(uniqueId, "getAdChoicesImageUrl");
		}

		public override string GetAdChoicesText(int uniqueId)
		{
			return this.getStringForNativeAdId(uniqueId, "getAdChoicesText");
		}

		public override string GetAdChoicesLinkURL(int uniqueId)
		{
			return this.getStringForNativeAdId(uniqueId, "getAdChoicesLinkUrl");
		}

		private string getId(int uniqueId)
		{
			AndroidJavaObject androidJavaObject = this.nativeAdForNativeAdId(uniqueId);
			if (androidJavaObject != null)
			{
				return androidJavaObject.Call<string>("getId", new object[0]);
			}
			return null;
		}

		public override void Release(int uniqueId)
		{
			NativeAdContainer nativeAdContainer = this.nativeAdContainerForNativeAdId(uniqueId);
			AndroidJavaObject nativeAd = nativeAdContainer.bridgedNativeAd;
			NativeAdBridgeAndroid.nativeAds.Remove(uniqueId);
			if (nativeAd != null)
			{
				AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				@static.Call("runOnUiThread", new object[]
				{
					new AndroidJavaRunnable(delegate()
					{
						AndroidJavaObject androidJavaObject = nativeAdContainer.ctaView.Call<AndroidJavaObject>("getParent", new object[0]);
						nativeAd.Call("destroy", new object[0]);
						androidJavaObject.Call("removeView", new object[]
						{
							nativeAdContainer.mediaView
						});
						androidJavaObject.Call("removeView", new object[]
						{
							nativeAdContainer.iconView
						});
						androidJavaObject.Call("removeView", new object[]
						{
							nativeAdContainer.ctaView
						});
					})
				});
			}
		}

		public override void OnLoad(int uniqueId, FBNativeAdBridgeCallback callback)
		{
		}

		public override void OnImpression(int uniqueId, FBNativeAdBridgeCallback callback)
		{
		}

		public override void OnClick(int uniqueId, FBNativeAdBridgeCallback callback)
		{
		}

		public override void OnError(int uniqueId, FBNativeAdBridgeErrorCallback callback)
		{
		}

		public override void OnFinishedClick(int uniqueId, FBNativeAdBridgeCallback callback)
		{
		}

		public override void OnMediaDownloaded(int uniqueId, FBNativeAdBridgeCallback callback)
		{
		}

		protected static Dictionary<int, NativeAdContainer> nativeAds = new Dictionary<int, NativeAdContainer>();

		protected static int lastKey = 0;
	}
}
