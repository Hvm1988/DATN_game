using System;
using AudienceNetwork;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterstitialAdTest : MonoBehaviour
{
	private void Awake()
	{
		this.LoadInterstitial();
	}

	public void LoadInterstitial()
	{
		this.statusLabel.text = "Loading interstitial ad...";
		this.interstitialAd = new InterstitialAd("YOUR_PLACEMENT_ID");
		this.interstitialAd.Register(base.gameObject);
		this.interstitialAd.InterstitialAdDidLoad = delegate()
		{
			UnityEngine.Debug.Log("Interstitial ad loaded.");
			this.isLoaded = true;
			this.didClose = false;
			this.statusLabel.text = "Ad loaded. Click show to present!";
		};
		this.interstitialAd.InterstitialAdDidFailWithError = delegate(string error)
		{
			UnityEngine.Debug.Log("Interstitial ad failed to load with error: " + error);
			this.statusLabel.text = "Interstitial ad failed to load. Check console for details.";
		};
		this.interstitialAd.InterstitialAdWillLogImpression = delegate()
		{
			UnityEngine.Debug.Log("Interstitial ad logged impression.");
		};
		this.interstitialAd.InterstitialAdDidClick = delegate()
		{
			UnityEngine.Debug.Log("Interstitial ad clicked.");
		};
		this.interstitialAd.interstitialAdDidClose = delegate()
		{
			UnityEngine.Debug.Log("Interstitial ad did close.");
			this.didClose = true;
			if (this.interstitialAd != null)
			{
				this.interstitialAd.Dispose();
			}
		};
		this.interstitialAd.interstitialAdActivityDestroyed = delegate()
		{
			if (!this.didClose)
			{
				UnityEngine.Debug.Log("Interstitial activity destroyed without being closed first.");
				UnityEngine.Debug.Log("Game should resume.");
			}
		};
		this.interstitialAd.LoadAd();
	}

	public void ShowInterstitial()
	{
		if (this.isLoaded)
		{
			this.interstitialAd.Show();
			this.isLoaded = false;
			this.statusLabel.text = string.Empty;
		}
		else
		{
			this.statusLabel.text = "Ad not loaded. Click load to request an ad.";
		}
	}

	private void OnDestroy()
	{
		if (this.interstitialAd != null)
		{
			this.interstitialAd.Dispose();
		}
		UnityEngine.Debug.Log("InterstitialAdTest was destroyed!");
	}

	public void NextScene()
	{
		SceneManager.LoadScene("AdViewScene");
	}

	private InterstitialAd interstitialAd;

	private bool isLoaded;

	private bool didClose;

	public Text statusLabel;
}
