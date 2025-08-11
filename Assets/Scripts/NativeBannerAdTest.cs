using System;
using AudienceNetwork;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(RectTransform))]
public class NativeBannerAdTest : MonoBehaviour
{
	private void Awake()
	{
		this.Log("Native banner ad ready to load.");
	}

	private void OnDestroy()
	{
		if (this.nativeBannerAd)
		{
			this.nativeBannerAd.Dispose();
		}
		UnityEngine.Debug.Log("NativeBannerAdTest was destroyed!");
	}

	public void LoadAd()
	{
		if (this.nativeBannerAd != null)
		{
			this.nativeBannerAd.Dispose();
		}
		this.nativeBannerAd = new NativeBannerAd("YOUR_PLACEMENT_ID");
		this.nativeBannerAd.RegisterGameObject(base.gameObject);
		this.nativeBannerAd.NativeAdDidLoad = delegate()
		{
			this.nativeBannerAd.RegisterGameObjectsForInteraction((RectTransform)this.iconImage.transform, (RectTransform)this.callToActionButton.transform, null);
			this.Log("Native banner ad loaded.");
			this.adChoices.SetAd(this.nativeBannerAd);
			this.advertiserName.text = this.nativeBannerAd.AdvertiserName;
			this.sponsored.text = this.nativeBannerAd.SponsoredTranslation;
			this.callToActionButton.GetComponentInChildren<Text>().text = this.nativeBannerAd.CallToAction;
		};
		this.nativeBannerAd.NativeAdDidDownloadMedia = delegate()
		{
			this.Log("Native banner ad media downloaded");
		};
		this.nativeBannerAd.NativeAdDidFailWithError = delegate(string error)
		{
			this.Log("Native banner ad failed to load with error: " + error);
		};
		this.nativeBannerAd.NativeAdWillLogImpression = delegate()
		{
			this.Log("Native banner ad logged impression.");
		};
		this.nativeBannerAd.NativeAdDidClick = delegate()
		{
			this.Log("Native banner ad clicked.");
		};
		this.nativeBannerAd.LoadAd();
		this.Log("Native banner ad loading...");
	}

	private void Log(string s)
	{
		this.status.text = s;
		UnityEngine.Debug.Log(s);
	}

	public void NextScene()
	{
		SceneManager.LoadScene("InterstitialAdScene");
	}

	private NativeBannerAd nativeBannerAd;

	[Header("Text:")]
	public Text advertiserName;

	public Text sponsored;

	public Text status;

	[Header("Images:")]
	public GameObject iconImage;

	[Header("Buttons:")]
	public Button callToActionButton;

	[Header("Ad Choices:")]
	public AdChoices adChoices;
}
