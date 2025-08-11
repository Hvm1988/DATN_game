using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
	public static event Setting.OnChangeVolume onChangeVolume;

	private void Start()
	{
		this.setUI();
	}

	private void setUI()
	{
		this.musicSlider.value = PlayerPrefs.GetFloat("MUSIC_VOLUME");
		this.soundSlider.value = PlayerPrefs.GetFloat("SOUND_VOLUME");
	}

	public void save()
	{
		PlayerPrefs.SetFloat("MUSIC_VOLUME", this.musicSlider.value);
		PlayerPrefs.SetFloat("SOUND_VOLUME", this.soundSlider.value);
		if (Setting.onChangeVolume != null)
		{
			Setting.onChangeVolume();
		}
	}

	public Slider musicSlider;

	public Slider soundSlider;

	public delegate void OnChangeVolume();
}
