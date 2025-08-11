using System;
using AudienceNetwork;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdViewTest : MonoBehaviour
{
	private void OnDestroy()
	{
		if (this.adView)
		{
			this.adView.Dispose();
		}
		UnityEngine.Debug.Log("AdViewTest was destroyed!");
	}

	private void Awake()
	{
		this.currentDeviceOrientation = Input.deviceOrientation;
	}

	public void LoadBanner()
	{
		if (this.adView)
		{
			this.adView.Dispose();
		}
		this.statusLabel.text = "Loading Banner...";
		this.adView = new AdView("YOUR_PLACEMENT_ID", AdSize.BANNER_HEIGHT_50);
		this.adView.Register(base.gameObject);
		this.currentAdViewPosition = AdPosition.CUSTOM;
		this.adView.AdViewDidLoad = delegate()
		{
			UnityEngine.Debug.Log("Banner loaded.");
			this.adView.Show(100.0);
			this.statusLabel.text = "Banner loaded";
		};
		this.adView.AdViewDidFailWithError = delegate(string error)
		{
			UnityEngine.Debug.Log("Banner failed to load with error: " + error);
			this.statusLabel.text = "Banner failed to load with error: " + error;
		};
		this.adView.AdViewWillLogImpression = delegate()
		{
			UnityEngine.Debug.Log("Banner logged impression.");
			this.statusLabel.text = "Banner logged impression.";
		};
		this.adView.AdViewDidClick = delegate()
		{
			UnityEngine.Debug.Log("Banner clicked.");
			this.statusLabel.text = "Banner clicked.";
		};
		this.adView.LoadAd();
	}

	public void NextScene()
	{
		SceneManager.LoadScene("RewardedVideoAdScene");
	}

	public void ChangePosition()
	{
		AdPosition adPosition = this.currentAdViewPosition;
		if (adPosition != AdPosition.TOP)
		{
			if (adPosition != AdPosition.BOTTOM)
			{
				if (adPosition == AdPosition.CUSTOM)
				{
					this.setAdViewPosition(AdPosition.TOP);
				}
			}
			else
			{
				this.setAdViewPosition(AdPosition.CUSTOM);
			}
		}
		else
		{
			this.setAdViewPosition(AdPosition.BOTTOM);
		}
	}

	public void Update()
	{
		if (Input.deviceOrientation != this.currentDeviceOrientation)
		{
			this.setAdViewPosition(this.currentAdViewPosition);
			this.currentDeviceOrientation = Input.deviceOrientation;
		}
	}

	private void setAdViewPosition(AdPosition adPosition)
	{
		if (adPosition != AdPosition.TOP)
		{
			if (adPosition != AdPosition.BOTTOM)
			{
				if (adPosition == AdPosition.CUSTOM)
				{
					this.adView.Show(100.0);
					this.currentAdViewPosition = AdPosition.CUSTOM;
				}
			}
			else
			{
				this.adView.Show(AdPosition.BOTTOM);
				this.currentAdViewPosition = AdPosition.BOTTOM;
			}
		}
		else
		{
			this.adView.Show(AdPosition.TOP);
			this.currentAdViewPosition = AdPosition.TOP;
		}
	}

	private AdView adView;

	private AdPosition currentAdViewPosition;

	private DeviceOrientation currentDeviceOrientation;

	public Text statusLabel;
}
