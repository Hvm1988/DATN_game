using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AudienceNetwork
{
	public class AdChoices : MonoBehaviour
	{
		private void Awake()
		{
			this.canvasGroup.alpha = 0f;
			this.canvasGroup.interactable = false;
		}

		public void SetAd(NativeAdBase nativeAd)
		{
			this.text.text = nativeAd.AdChoicesText;
			this.linkURL = nativeAd.AdChoicesLinkURL;
			this.imageUrl = nativeAd.AdChoicesImageURL;
			this.canvasGroup.alpha = 1f;
			this.canvasGroup.interactable = true;
			base.StartCoroutine(this.LoadAdChoicesImage());
		}

		public IEnumerator LoadAdChoicesImage()
		{
			Texture2D texture = new Texture2D(4, 4, TextureFormat.RGBA32, false);
			WWW www = new WWW(this.imageUrl);
			yield return www;
			www.LoadImageIntoTexture(texture);
			if (texture)
			{
				this.image.sprite = Sprite.Create(texture, new Rect(0f, 0f, (float)texture.width, (float)texture.height), new Vector2(0.5f, 0.5f));
			}
			yield break;
		}

		public void AdChoicesTapped()
		{
			Application.OpenURL(this.linkURL);
		}

		[Header("Ad Choices:")]
		[SerializeField]
		public Image image;

		public Text text;

		public CanvasGroup canvasGroup;

		private string imageUrl;

		private string linkURL;
	}
}
