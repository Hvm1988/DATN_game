using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private void Awake()
	{
		SoundManager.Instance = this;
	}

	private void OnEnable()
	{
		Setting.onChangeVolume += this.onChangeVolume;
	}

	private void OnDisable()
	{
		Setting.onChangeVolume -= this.onChangeVolume;
	}

	private void Start()
	{
		if (!PlayerPrefs.HasKey("MUSIC_VOLUME"))
		{
			PlayerPrefs.SetFloat("MUSIC_VOLUME", 1f);
		}
		if (!PlayerPrefs.HasKey("SOUND_VOLUME"))
		{
			PlayerPrefs.SetFloat("SOUND_VOLUME", 1f);
		}
		this.onChangeVolume();
	}

	private void onChangeVolume()
	{
		this.volume = PlayerPrefs.GetFloat("SOUND_VOLUME");
		this.bgAudioSource.volume = PlayerPrefs.GetFloat("MUSIC_VOLUME");
	}

	public void playAudioWithRefresh(string name)
	{
		this.volume = PlayerPrefs.GetFloat("SOUND_VOLUME");
		this.playAudio(name);
	}

	public void playAudio(string name)
	{
		switch (name)
		{
		case "PayGold":
			this.audioSource.PlayOneShot(this.payGold, this.volume);
			break;
		case "PayRuby":
			this.audioSource.PlayOneShot(this.payRuby, this.volume);
			break;
		case "PopUpOpen":
			this.audioSource.PlayOneShot(this.popUpOpen, this.volume * 1.3f);
			break;
		case "PayPurchaser":
			this.audioSource.PlayOneShot(this.payPurchaser, this.volume);
			break;
		case "ButtonClick":
			this.audioSource.PlayOneShot(this.buttonClick, this.volume * 0.8f);
			break;
		case "Back":
			this.audioSource.PlayOneShot(this.back, this.volume);
			break;
		case "Unlock":
			this.audioSource.PlayOneShot(this.unlock, this.volume * 2f);
			break;
		}
	}

	public static SoundManager Instance;

	public AudioSource bgAudioSource;

	public AudioSource audioSource;

	public AudioClip payGold;

	public AudioClip payRuby;

	public AudioClip popUpOpen;

	public AudioClip payPurchaser;

	public AudioClip buttonClick;

	public AudioClip back;

	public AudioClip unlock;

	public float volume;
}
