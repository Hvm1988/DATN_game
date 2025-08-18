using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class BGMusicFromPrefs : MonoBehaviour
{
    [Header("AudioSource nhạc nền (kéo SoundManager.bgAudioSource vào)")]
    public AudioSource target;

    [Header("4 bài mặc định đúng thứ tự như Intro")]
    public AudioClip[] builtInClips; // 0..3

    const string K_TYPE = "bgm.type";   // 0=built-in, 1=custom
    const string K_INDEX = "bgm.index";  // index built-in
    const string K_PATH = "bgm.custom"; // file name trong persistentDataPath

    void Awake()
    {
        if (!target && SoundManager.Instance) target = SoundManager.Instance.bgAudioSource;
    }

    void Start()
    {
        if (!target) { Debug.LogWarning("BGMusicFromPrefs: target=null"); return; }
        target.loop = true; target.playOnAwake = false; target.spatialBlend = 0f;

        int type = PlayerPrefs.GetInt(K_TYPE, 0);
        if (type == 0)
        {
            int i = Mathf.Clamp(PlayerPrefs.GetInt(K_INDEX, 0), 0, Mathf.Max(0, builtInClips.Length - 1));
            if (builtInClips != null && builtInClips.Length > 0 && builtInClips[i])
            {
                target.clip = builtInClips[i];
                target.Play();
            }
            SoundManager.Instance?.ApplyVolumes();
        }
        else
        {
            string rel = PlayerPrefs.GetString(K_PATH, "");
            if (!string.IsNullOrEmpty(rel))
            {
                string full = Path.Combine(Application.persistentDataPath, rel);
                if (File.Exists(full)) { StartCoroutine(LoadAndPlay(full)); return; }
            }
            FallbackToBuiltIn();
        }
    }

    IEnumerator LoadAndPlay(string full)
    {
        string url = "file://" + full.Replace("\\", "/");
        AudioType t = AudioType.WAV;
        var ext = Path.GetExtension(full).ToLowerInvariant();
        if (ext == ".mp3") t = AudioType.MPEG; else if (ext == ".ogg") t = AudioType.OGGVORBIS;

        using (var req = UnityWebRequestMultimedia.GetAudioClip(url, t))
        {
            yield return req.SendWebRequest();
#if UNITY_2020_3_OR_NEWER
            if (req.result != UnityWebRequest.Result.Success) { FallbackToBuiltIn(); yield break; }
#else
            if (req.isNetworkError || req.isHttpError) { FallbackToBuiltIn(); yield break; }
#endif
            target.clip = DownloadHandlerAudioClip.GetContent(req);
            target.Play();
            SoundManager.Instance?.ApplyVolumes();
        }
    }

    void FallbackToBuiltIn()
    {
        if (builtInClips != null && builtInClips.Length > 0 && builtInClips[0])
        {
            target.clip = builtInClips[0];
            target.Play();
        }
        SoundManager.Instance?.ApplyVolumes();
    }
}