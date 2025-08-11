using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePausePopup : MonoBehaviour
{
	public void pauseGame()
	{
		Time.timeScale = 0f;
	}

	public void gameResume()
	{
		base.gameObject.SetActive(false);
	}

	public void btnResume()
	{
		Time.timeScale = 1f;
		this._animator.Play("close");
		this._gamemanager.setVolumeCharacter(GameConfig.soundVolume);
	}

	public void onSoundChange()
	{
		GameConfig.soundVolume = this.sliderSound.value;
		PlayerPrefs.SetFloat("SOUND_VOLUME", GameConfig.soundVolume);
	}

	public void onMusicChange()
	{
		GameConfig.musicVolume = this.sliderMusic.value;
		PlayerPrefs.SetFloat("MUSIC_VOLUME", GameConfig.musicVolume);
		this.bgAudio.volume = GameConfig.musicVolume;
	}

	public Animator _animator;

	public Slider sliderSound;

	public Slider sliderMusic;

	public AudioSource bgAudio;

	public GameManager _gamemanager;
}
