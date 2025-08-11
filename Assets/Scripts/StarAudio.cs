using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarAudio : MonoBehaviour
{
	private void audioPlay(AudioClip _audioClip)
	{
		if (GameConfig.soundVolume > 0f)
		{
			if (this._audio.volume != GameConfig.soundVolume)
			{
				this._audio.volume = GameConfig.soundVolume;
			}
			this._audio.clip = _audioClip;
			this._audio.Play();
		}
	}

	public void playAudio(int num)
	{
		if (num == 1)
		{
			this._audio.clip = this.star1;
		}
		else if (num == 2)
		{
			this._audio.clip = this.star2;
		}
		else if (num == 3)
		{
			this._audio.clip = this.star3;
		}
		this._audio.Play();
	}

	public void activeClick()
	{
		GameResult.ins.can_click = true;
		GameResult.ins.checklevelUp();
		if (PlayerPrefs.GetInt("gameResultTutorial") == 0 && !SceneManager.GetActiveScene().name.Equals("MainMenu"))
		{
			GameResult.ins.showTutorial();
		}
	}

	public AudioSource _audio;

	public AudioClip star1;

	public AudioClip star2;

	public AudioClip star3;
}
