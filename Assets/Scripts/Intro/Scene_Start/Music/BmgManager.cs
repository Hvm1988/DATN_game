using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class BmgManager : MonoBehaviour
{
    public static BmgManager I;

    [Header("Audio")]
    [SerializeField] private AudioSource source;           // gắn trong Inspector
    [SerializeField] private List<AudioClip> builtInClips; // 4 clip mặc định
    [SerializeField] private float fadeDuration = 0.35f;

    const string K_TYPE = "bgm.type";      // 0=built-in, 1=custom
    const string K_INDEX = "bgm.index";    // index bài built-in
    const string K_PATH = "bgm.custom";    // tên file custom trong persistentDataPath

    enum BgmType { BuiltIn = 0, Custom = 1 }

    void Awake()
    {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);

        if (!source) source = gameObject.AddComponent<AudioSource>();
        source.loop = true; source.playOnAwake = false;

        LoadAndPlaySaved();  // phát theo lựa chọn đã lưu (mặc định bài 0)
    }

    void LoadAndPlaySaved()
    {
        var type = (BgmType)PlayerPrefs.GetInt(K_TYPE, (int)BgmType.BuiltIn);
        if (type == BgmType.Custom && PlayerPrefs.HasKey(K_PATH))
        {
            string rel = PlayerPrefs.GetString(K_PATH);
            string full = Path.Combine(Application.persistentDataPath, rel);
            StartCoroutine(LoadAndPlayFromPath(full));
        }
        else
        {
            int idx = Mathf.Clamp(PlayerPrefs.GetInt(K_INDEX, 0), 0, builtInClips.Count - 1);
            SetBuiltIn(idx, instant: true);
        }
    }

    // Gọi từ button để chuyển bài mặc định
    public void CycleNextBuiltIn()
    {
        if (builtInClips == null || builtInClips.Count == 0) return;
        int next = (PlayerPrefs.GetInt(K_INDEX, 0) + 1) % builtInClips.Count;
        SetBuiltIn(next);
    }

    public void SetBuiltIn(int index, bool instant = false)
    {
        if (index < 0 || index >= builtInClips.Count) return;
        PlayerPrefs.SetInt(K_TYPE, (int)BgmType.BuiltIn);
        PlayerPrefs.SetInt(K_INDEX, index);
        PlayerPrefs.Save();
        Play(builtInClips[index], instant);
    }

    void Play(AudioClip clip, bool instant = false)
    {
        StopAllCoroutines();
        StartCoroutine(FadeTo(clip, instant ? 0f : fadeDuration));
    }

    IEnumerator FadeTo(AudioClip clip, float dur)
    {
        float startVol = source.volume;
        float t = 0;
        while (t < dur * 0.5f)
        {
            t += Time.unscaledDeltaTime;
            source.volume = Mathf.Lerp(startVol, 0f, t / (dur * 0.5f));
            yield return null;
        }
        source.clip = clip;
        if (clip && !source.isPlaying) source.Play();
        t = 0;
        while (t < dur * 0.5f)
        {
            t += Time.unscaledDeltaTime;
            source.volume = Mathf.Lerp(0f, startVol, t / (dur * 0.5f));
            yield return null;
        }
    }

    // ==== MP3 tùy người chơi (tùy chọn) ====
    // Gọi hàm này sau khi có đường dẫn file (từ file picker).
    public void SetCustomFromFullPath(string fullPath)
    {
        if (!File.Exists(fullPath)) { Debug.LogError("File not found: " + fullPath); return; }

        string ext = Path.GetExtension(fullPath).ToLowerInvariant();     // .mp3/.wav/.ogg
        string destName = "user_bgm" + ext;
        string dest = Path.Combine(Application.persistentDataPath, destName);

        File.Copy(fullPath, dest, true);  // copy vào persistentDataPath để “sống” cùng game

        PlayerPrefs.SetInt(K_TYPE, (int)BgmType.Custom);
        PlayerPrefs.SetString(K_PATH, destName);
        PlayerPrefs.Save();

        StartCoroutine(LoadAndPlayFromPath(dest));
    }

    IEnumerator LoadAndPlayFromPath(string full)
    {
        string url = "file://" + full.Replace("\\", "/");
        AudioType aType = GetAudioTypeByExt(Path.GetExtension(full));

        using (var req = UnityWebRequestMultimedia.GetAudioClip(url, aType))
        {
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Load audio failed: " + req.error);
                yield break;
            }
            var clip = DownloadHandlerAudioClip.GetContent(req);
            clip.name = "UserBGM";
            Play(clip);
        }
    }

    AudioType GetAudioTypeByExt(string ext)
    {
        ext = ext.ToLowerInvariant();
        if (ext == ".mp3") return AudioType.MPEG;
        if (ext == ".wav") return AudioType.WAV;
        if (ext == ".ogg") return AudioType.OGGVORBIS;
        return AudioType.UNKNOWN; // vẫn thử được trên 1 số nền tảng
    }

    // Nút "Chọn MP3" (cần plugin file picker, xem bên dưới)
    public void PickAndSetCustomMP3()
    {
#if SFB   // nếu dùng StandaloneFileBrowser (gkngkc/StandaloneFileBrowser)
        var paths = SFB.StandaloneFileBrowser.OpenFilePanel("Chọn file nhạc", "", 
            new[] { new SFB.ExtensionFilter("Audio", "mp3", "wav", "ogg") }, false);
        if (paths != null && paths.Length > 0) SetCustomFromFullPath(paths[0]);
#elif SIMPLE_FILE_BROWSER   // nếu dùng yasirkula/SimpleFileBrowser
        SimpleFileBrowser.FileBrowser.ShowLoadDialog(
            (path) => SetCustomFromFullPath(path), 
            null, SimpleFileBrowser.FileBrowser.PickMode.Files,
            false, null, null, "Chọn MP3", "Chọn");
#else
        Debug.Log("Cần tích hợp file picker (StandaloneFileBrowser hoặc SimpleFileBrowser). Sau đó bật macro SFB hoặc SIMPLE_FILE_BROWSER.");
#endif
    }
}
