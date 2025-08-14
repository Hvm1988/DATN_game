using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickAudio : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;

    AudioSource src; Button btn;

    void Awake()
    {
        btn = GetComponent<Button>();
        src = gameObject.GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.loop = false;
        src.spatialBlend = 0f;           // 2D
        src.outputAudioMixerGroup = null;
        btn.onClick.AddListener(() => {
            Debug.Log("Button clicked");
            if (clip != null) src.PlayOneShot(clip, volume);
            else Debug.LogWarning("No clip assigned");
        });
    }
}
