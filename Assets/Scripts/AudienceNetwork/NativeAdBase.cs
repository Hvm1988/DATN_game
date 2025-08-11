using System;
using UnityEngine;

namespace AudienceNetwork
{
	public abstract class NativeAdBase
	{
		public NativeAdBase(string placementId)
		{
			this.PlacementId = placementId;
			this.NativeAdBridgeInstance().OnLoad(this.uniqueId, this.NativeAdDidLoad);
			this.NativeAdBridgeInstance().OnImpression(this.uniqueId, this.NativeAdWillLogImpression);
			this.NativeAdBridgeInstance().OnClick(this.uniqueId, this.NativeAdDidClick);
			this.NativeAdBridgeInstance().OnError(this.uniqueId, this.NativeAdDidFailWithError);
			this.NativeAdBridgeInstance().OnFinishedClick(this.uniqueId, this.NativeAdDidFinishHandlingClick);
			this.NativeAdBridgeInstance().OnMediaDownloaded(this.uniqueId, this.NativeAdDidDownloadMedia);
		}

		public string PlacementId { get; private set; }

		public string AdvertiserName { get; private set; }

		public string Headline { get; private set; }

		public string LinkDescription { get; private set; }

		public string SponsoredTranslation { get; private set; }

		public string AdTranslation { get; private set; }

		public string PromotedTranslation { get; private set; }

		public string Body { get; private set; }

		public string CallToAction { get; private set; }

		public string SocialContext { get; private set; }

		public string AdChoicesImageURL { get; private set; }

		public string AdChoicesText { get; private set; }

		public string AdChoicesLinkURL { get; private set; }

		internal virtual NativeAdBridge NativeAdBridgeInstance()
		{
			return NativeAdBridge.Instance;
		}

		~NativeAdBase()
		{
			this.Dispose(false);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool iAmBeingCalledFromDisposeAndNotFinalize)
		{
			if (this.handler)
			{
				this.handler.removeFromParent();
			}
			this.NativeAdBridgeInstance().Release(this.uniqueId);
		}

		public override string ToString()
		{
			return string.Format("[NativeAd: PlacementId={0}, AdvertiserName={1}, Body={2}", this.PlacementId, this.AdvertiserName, this.Body);
		}

		public virtual void LoadAd()
		{
			this.NativeAdBridgeInstance().Load(this.uniqueId);
		}

		protected int baseRegisterGameObjectsForInteraction(RectTransform mediaViewRectTransform, RectTransform ctaRectTransform, RectTransform iconViewRectTransform = null, Camera camera = null)
		{
			if (camera == null)
			{
				camera = Camera.main;
			}
			Rect gameObjectRect = this.getGameObjectRect(mediaViewRectTransform, camera);
			Rect gameObjectRect2 = this.getGameObjectRect(iconViewRectTransform, camera);
			Rect gameObjectRect3 = this.getGameObjectRect(ctaRectTransform, camera);
			return this.RegisterGameObjectsForInteraction(gameObjectRect, gameObjectRect2, gameObjectRect3);
		}

		protected virtual int RegisterGameObjectsForInteraction(Rect mediaViewRect, Rect iconViewRect, Rect ctaViewRect)
		{
			return this.NativeAdBridgeInstance().RegisterGameObjectsForInteraction(this.uniqueId, mediaViewRect, iconViewRect, ctaViewRect);
		}

		private Rect getGameObjectRect(RectTransform rectTransform, Camera camera)
		{
			if (rectTransform == null)
			{
				return Rect.zero;
			}
			Vector3[] array = new Vector3[4];
			Canvas canvas = this.getCanvas(this.handler.gameObject);
			rectTransform.GetWorldCorners(array);
			Vector3 position = array[0];
			Vector3 position2 = array[2];
			Vector3 vector = camera.pixelRect.min;
			Vector3 vector2 = camera.pixelRect.max;
			if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
			{
				position = camera.WorldToScreenPoint(position);
				position2 = camera.WorldToScreenPoint(position2);
			}
			return new Rect(Mathf.Round(position.x), Mathf.Floor(vector2.y - position2.y), Mathf.Ceil(position2.x - position.x), Mathf.Round(position2.y - position.y));
		}

		private Canvas getCanvas(GameObject gameObject)
		{
			if (gameObject.GetComponent<Canvas>() != null)
			{
				return gameObject.GetComponent<Canvas>();
			}
			if (gameObject.transform.parent != null)
			{
				return this.getCanvas(gameObject.transform.parent.gameObject);
			}
			return null;
		}

		public virtual bool IsValid()
		{
			return this.isLoaded && this.NativeAdBridgeInstance().IsValid(this.uniqueId);
		}

		public void RegisterGameObject(GameObject gameObject)
		{
			this.createHandler(gameObject);
		}

		private void createHandler(GameObject gameObject)
		{
			this.handler = gameObject.AddComponent<AdHandler>();
		}

		internal virtual void loadAdFromData()
		{
			if (this.handler == null)
			{
				throw new InvalidOperationException("Native ad was loaded before it was registered. Ensure RegisterGameObjectForImpression () are called.");
			}
			int num = this.uniqueId;
			this.AdvertiserName = this.NativeAdBridgeInstance().GetAdvertiserName(num);
			this.Headline = this.NativeAdBridgeInstance().GetHeadline(num);
			this.LinkDescription = this.NativeAdBridgeInstance().GetLinkDescription(num);
			this.SponsoredTranslation = this.NativeAdBridgeInstance().GetSponsoredTranslation(num);
			this.AdTranslation = this.NativeAdBridgeInstance().GetAdTranslation(num);
			this.PromotedTranslation = this.NativeAdBridgeInstance().GetPromotedTranslation(num);
			this.Body = this.NativeAdBridgeInstance().GetBody(num);
			this.CallToAction = this.NativeAdBridgeInstance().GetCallToAction(num);
			this.SocialContext = this.NativeAdBridgeInstance().GetSocialContext(num);
			this.CallToAction = this.NativeAdBridgeInstance().GetCallToAction(num);
			this.AdChoicesImageURL = this.NativeAdBridgeInstance().GetAdChoicesImageURL(num);
			this.AdChoicesText = this.NativeAdBridgeInstance().GetAdChoicesText(num);
			this.AdChoicesLinkURL = this.NativeAdBridgeInstance().GetAdChoicesLinkURL(num);
			this.isLoaded = true;
			if (this.NativeAdDidLoad != null)
			{
				this.handler.executeOnMainThread(delegate
				{
					this.NativeAdDidLoad();
				});
			}
		}

		internal void executeOnMainThread(Action action)
		{
			if (this.handler)
			{
				this.handler.executeOnMainThread(action);
			}
		}

		public static implicit operator bool(NativeAdBase obj)
		{
			return !object.ReferenceEquals(obj, null);
		}

		public FBNativeAdBridgeCallback NativeAdDidLoad
		{
			internal get
			{
				return this.nativeAdDidLoad;
			}
			set
			{
				this.nativeAdDidLoad = value;
				this.NativeAdBridgeInstance().OnLoad(this.uniqueId, this.nativeAdDidLoad);
			}
		}

		public FBNativeAdBridgeCallback NativeAdWillLogImpression
		{
			internal get
			{
				return this.nativeAdWillLogImpression;
			}
			set
			{
				this.nativeAdWillLogImpression = value;
				this.NativeAdBridgeInstance().OnImpression(this.uniqueId, this.nativeAdWillLogImpression);
			}
		}

		public FBNativeAdBridgeErrorCallback NativeAdDidFailWithError
		{
			internal get
			{
				return this.nativeAdDidFailWithError;
			}
			set
			{
				this.nativeAdDidFailWithError = value;
				this.NativeAdBridgeInstance().OnError(this.uniqueId, this.nativeAdDidFailWithError);
			}
		}

		public FBNativeAdBridgeCallback NativeAdDidClick
		{
			internal get
			{
				return this.nativeAdDidClick;
			}
			set
			{
				this.nativeAdDidClick = value;
				this.NativeAdBridgeInstance().OnClick(this.uniqueId, this.nativeAdDidClick);
			}
		}

		public FBNativeAdBridgeCallback NativeAdDidFinishHandlingClick
		{
			internal get
			{
				return this.nativeAdDidFinishHandlingClick;
			}
			set
			{
				this.nativeAdDidFinishHandlingClick = value;
				this.NativeAdBridgeInstance().OnFinishedClick(this.uniqueId, this.nativeAdDidFinishHandlingClick);
			}
		}

		public FBNativeAdBridgeCallback NativeAdDidDownloadMedia
		{
			internal get
			{
				return this.nativeAdDidDownloadMedia;
			}
			set
			{
				this.nativeAdDidDownloadMedia = value;
				this.NativeAdBridgeInstance().OnMediaDownloaded(this.uniqueId, this.nativeAdDidDownloadMedia);
			}
		}

		internal int uniqueId;

		protected bool isLoaded;

		protected AdHandler handler;

		internal NativeAdType nativeAdType;

		private FBNativeAdBridgeCallback nativeAdDidLoad;

		private FBNativeAdBridgeCallback nativeAdWillLogImpression;

		private FBNativeAdBridgeErrorCallback nativeAdDidFailWithError;

		private FBNativeAdBridgeCallback nativeAdDidClick;

		private FBNativeAdBridgeCallback nativeAdDidFinishHandlingClick;

		private FBNativeAdBridgeCallback nativeAdDidDownloadMedia;
	}
}
