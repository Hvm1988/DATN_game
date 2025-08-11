using System;
using System.Collections;
using UnityEngine;

public class PopupsBase : MonoBehaviour
{
	public virtual void OnEnable()
	{
		this._animator.Play("popupOpen", 0, 0f);
	}

	public virtual void onClose()
	{
		this._animator.Play("popupClose");
		base.StartCoroutine(this.disable());
		this.audioClick();
	}

	private IEnumerator disable()
	{
		yield return new WaitForSeconds(0.3f);
		this.parrent.SetActive(false);
		yield break;
	}

	public void audioClick()
	{
		if (GameConfig.soundVolume > 0f)
		{
			if (this._audio.volume != GameConfig.soundVolume)
			{
				this._audio.volume = GameConfig.soundVolume;
			}
			this._audio.Play();
		}
	}

	public Animator _animator;

	public GameObject parrent;

	public AudioSource _audio;
}
