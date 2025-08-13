using UnityEngine;

public class MusicPanelOpen : MonoBehaviour
{
    [SerializeField] AudioSource mainSource;

    void Awake()
    {
        if (!mainSource) mainSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        mainSource.spatialBlend = 0f;   // 2D
        mainSource.loop = true;
        mainSource.volume = 1f;
        AudioListener.pause = false;
    }

    public void Play(AudioClip clip)
    {
        if (!clip) { Debug.LogWarning("No clip"); return; }
        mainSource.Stop();
        mainSource.clip = clip;
        mainSource.Play();
        Debug.Log($"Playing: {clip.name}");
    }

    [ContextMenu("Test Play (uses current clip)")]
    void TestPlay() { if (mainSource.clip) mainSource.Play(); }
}

