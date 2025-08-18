using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Sources")]
    public AudioSource bgAudioSource; // NHẠC NỀN
    public AudioSource audioSource;   // SFX

    [Header("Clips SFX")]
    public AudioClip payGold, payRuby, popUpOpen, payPurchaser, buttonClick, back, unlock;

    public float volume; // SFX volume cache

    void Awake() { Instance = this; }

    void OnEnable() { Setting.onChangeVolume += onChangeVolume; }
    void OnDisable() { Setting.onChangeVolume -= onChangeVolume; }

    void Start()
    {
        if (!PlayerPrefs.HasKey("MUSIC_VOLUME")) PlayerPrefs.SetFloat("MUSIC_VOLUME", 1f);
        if (!PlayerPrefs.HasKey("SOUND_VOLUME")) PlayerPrefs.SetFloat("SOUND_VOLUME", 1f);
        onChangeVolume();
    }

    public void ApplyVolumes()
    {
        volume = PlayerPrefs.GetFloat("SOUND_VOLUME", 1f);
        if (audioSource) audioSource.volume = volume;
        if (bgAudioSource) bgAudioSource.volume = PlayerPrefs.GetFloat("MUSIC_VOLUME", 1f);
    }

    void onChangeVolume() => ApplyVolumes();

    public void playAudioWithRefresh(string name)
    {
        volume = PlayerPrefs.GetFloat("SOUND_VOLUME", 1f);
        playAudio(name);
    }

    public void playAudio(string name)
    {
        switch (name)
        {
            case "PayGold": audioSource.PlayOneShot(payGold, volume); break;
            case "PayRuby": audioSource.PlayOneShot(payRuby, volume); break;
            case "PopUpOpen": audioSource.PlayOneShot(popUpOpen, volume * 1.3f); break;
            case "PayPurchaser": audioSource.PlayOneShot(payPurchaser, volume); break;
            case "ButtonClick": audioSource.PlayOneShot(buttonClick, volume * 0.8f); break;
            case "Back": audioSource.PlayOneShot(back, volume); break;
            case "Unlock": audioSource.PlayOneShot(unlock, volume * 2f); break;
        }
    }
}

