using System;
using AudienceNetwork;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(RectTransform))]
public class NativeAdTest : MonoBehaviour
{
	private void Awake()
	{
		this.Log("Native ad ready to load.");
	}

	private void OnDestroy()
	{
		if (this.nativeAd)
		{
			this.nativeAd.Dispose();
		}
		UnityEngine.Debug.Log("NativeAdTest was destroyed!");
	}

	public void LoadAd()
	{
		if (this.nativeAd != null)
		{
			this.nativeAd.Dispose();
		}
		this.nativeAd = new NativeAd("YOUR_PLACEMENT_ID");
		this.nativeAd.RegisterGameObject(base.gameObject);
		this.nativeAd.NativeAdDidLoad = delegate()
		{
			this.nativeAd.RegisterGameObjectsForInteraction((RectTransform)this.mediaView.transform, (RectTransform)this.callToActionButton.transform, (RectTransform)this.iconImage.transform, null);
			this.Log("Native ad loaded.");
			this.adChoices.SetAd(this.nativeAd);
			this.advertiserName.text = this.nativeAd.AdvertiserName;
			this.socialContext.text = this.nativeAd.SocialContext;
			this.body.text = this.nativeAd.Body;
			this.sponsored.text = this.nativeAd.SponsoredTranslation;
			this.callToActionButton.GetComponentInChildren<Text>().text = this.nativeAd.CallToAction;
		};
		this.nativeAd.NativeAdDidDownloadMedia = delegate()
		{
			this.Log("Native ad media downloaded");
		};
		this.nativeAd.NativeAdDidFailWithError = delegate(string error)
		{
			this.Log("Native ad failed to load with error: " + error);
		};
		this.nativeAd.NativeAdWillLogImpression = delegate()
		{
			this.Log("Native ad logged impression.");
		};
		this.nativeAd.NativeAdDidClick = delegate()
		{
			this.Log("Native ad clicked.");
		};
		this.nativeAd.LoadAd();
		this.Log("Native ad loading...");
	}

	private void Log(string s)
	{
		this.status.text = s;
		UnityEngine.Debug.Log(s);
	}

	public void NextScene()
	{
		SceneManager.LoadScene("NativeBannerAdScene");
	}

	private NativeAd nativeAd;

	[Header("Text:")]
	public Text advertiserName;

	public Text socialContext;

	public Text body;

	public Text sponsored;

	public Text status;

	[Header("Images:")]
	public GameObject mediaView;

	public GameObject iconImage;

	[Header("Buttons:")]
	public Button callToActionButton;

	[Header("Ad Choices:")]
	[SerializeField]
	private AdChoices adChoices;
}
