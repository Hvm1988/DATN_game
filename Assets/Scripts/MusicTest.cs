using System;
using UnityEngine;
using UnityEngine.UI;

public class MusicTest : MonoBehaviour
{
	public void onShow(string nameAudioclip, AudioSource audioSource, AudioClip audioClip, bool hadBoss)
	{
		this.audioClip = audioClip;
		this.txtName.text = nameAudioclip;
		this.audioSource = audioSource;
		this.hadBoss = hadBoss;
	}

	public void listen()
	{
		this.audioSource.clip = this.audioClip;
		this.audioSource.Play();
		this.bg.volume = 0f;
	}

	public void setup()
	{
		if (this.hadBoss)
		{
			this._gameManager._audioBossComing = this.audioClip;
			this.audioSource.Stop();
			this.bg.volume = 1f;
		}
		else
		{
			this.audioSource.Stop();
			this.bg.clip = this.audioClip;
			this.bg.Play();
			this.bg.volume = 1f;
		}
	}

	public Text txtName;

	private AudioClip audioClip;

	private AudioSource audioSource;

	public AudioSource bg;

	private bool hadBoss;

	public GameManager _gameManager;
}
