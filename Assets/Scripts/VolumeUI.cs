using UnityEngine;

public class VolumeUI : MonoBehaviour
{
    // Gọi từ sự kiện OnValueChanged của Slider
    public void SetMusic(float v01)
    {
        PlayerPrefs.SetFloat("MUSIC_VOLUME", Mathf.Clamp01(v01));
        PlayerPrefs.Save();
        Setting1.RaiseChangeVolume();
    }

    public void SetSfx(float v01)
    {
        PlayerPrefs.SetFloat("SOUND_VOLUME", Mathf.Clamp01(v01));
        PlayerPrefs.Save();
        Setting1.RaiseChangeVolume();
    }
}
