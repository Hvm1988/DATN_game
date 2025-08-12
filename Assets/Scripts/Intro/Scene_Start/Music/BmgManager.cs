using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class BmgManager : MonoBehaviour
{
    public static BmgManager I;

    [Header("Audio")]
    [SerializeField] private AudioSource source;            // gắn trong Inspector (hoặc để trống)
    [SerializeField] private List<AudioClip> builtInClips;  // các bài mặc định
    [SerializeField, Range(0, 1f)] private float defaultVolume = 0.6f;
    [SerializeField] private float fadeDuration = 0.35f;

    const string K_TYPE = "bgm.type";     // 0=built-in, 1=custom
    const string K_INDEX = "bgm.index";    // index bài built-in (-1 = Không phát)
    const string K_PATH = "bgm.custom";   // tên file custom trong persistentDataPath
    const string K_VOL = "bgm.vol";
    public float GetVolume() => source ? source.volume : 0.6f;

    enum BgmType { BuiltIn = 0, Custom = 1 }

    private AudioClip lastUserClip;        // clip custom đã load để chủ động Destroy khi thay

    void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this; DontDestroyOnLoad(gameObject);

        if (!source) source = gameObject.AddComponent<AudioSource>();
        source.loop = true;
        source.playOnAwake = false;
        source.volume = PlayerPrefs.GetFloat(K_VOL, defaultVolume);

        LoadAndApplySavedChoice();
    }

    // ======= KHỞI TẠO THEO LỰA CHỌN ĐÃ LƯU =======
    void LoadAndApplySavedChoice()
    {
        var type = (BgmType)PlayerPrefs.GetInt(K_TYPE, (int)BgmType.BuiltIn);
        int savedIndex = PlayerPrefs.GetInt(K_INDEX, 0);

        if (type == BgmType.Custom && PlayerPrefs.HasKey(K_PATH))
        {
            string rel = PlayerPrefs.GetString(K_PATH);
            string full = Path.Combine(Application.persistentDataPath, rel);
            if (File.Exists(full)) { StartCoroutine(LoadAndPlayFromPath(full)); return; }

            // File custom bị mất -> quay về built-in
            PlayerPrefs.SetInt(K_TYPE, (int)BgmType.BuiltIn);
            PlayerPrefs.Save();
        }

        // Built-in
        if (savedIndex == -1) { Stop(); return; } // Không phát
        if (builtInClips != null && builtInClips.Count > 0)
        {
            savedIndex = Mathf.Clamp(savedIndex, 0, builtInClips.Count - 1);
            SetBuiltIn(savedIndex, instant: true);
        }
    }

    // ======= API CHO UI =======

    // Dropdown: value=0 -> Không phát, còn lại: value-1 là index built-in
    public void SelectFromDropdown(int value, bool autoplay = true)
    {
        if (value == 0) SetBuiltIn(-1, false);     // Không phát
        else SetBuiltIn(value - 1, autoplay);
    }

    // Nút “next” nếu bạn vẫn muốn cho người chơi tự chuyển (không auto)
    public void CycleNextBuiltIn()
    {
        if (builtInClips == null || builtInClips.Count == 0) return;
        int cur = PlayerPrefs.GetInt(K_INDEX, -1);
        int next = (cur + 1 + builtInClips.Count) % builtInClips.Count;
        SetBuiltIn(next);
    }

    // Chọn bài built-in theo index; index=-1 => Không phát
    public void SetBuiltIn(int index, bool instant = false)
    {
        StopAllCoroutines();

        PlayerPrefs.SetInt(K_TYPE, (int)BgmType.BuiltIn);
        PlayerPrefs.SetInt(K_INDEX, index);
        PlayerPrefs.Save();

        if (index == -1) { Stop(); return; }           // Không phát
        if (index < 0 || index >= builtInClips.Count) return;

        // Đổi từ custom -> built-in: dọn clip custom cũ
        if (lastUserClip) { Destroy(lastUserClip); lastUserClip = null; }

        Play(builtInClips[index], instant);
    }

    public void SetVolume(float v)
    {
        v = Mathf.Clamp01(v);
        source.volume = v;
        PlayerPrefs.SetFloat(K_VOL, v);
        PlayerPrefs.Save();
    }

    public int GetSavedIndex() => PlayerPrefs.GetInt(K_INDEX, -1); // -1 = Không phát
    public IReadOnlyList<AudioClip> Clips => builtInClips;

    // ======= CORE PHÁT NHẠC (KHÔNG AUTO ĐỔI) =======

    void Play(AudioClip clip, bool instant = false)
    {
        StopAllCoroutines();
        StartCoroutine(FadeTo(clip, instant ? 0f : fadeDuration));
    }

    IEnumerator FadeTo(AudioClip clip, float dur)
    {
        float startVol = source.volume;
        float half = Mathf.Max(0.0001f, dur * 0.5f);

        // Fade-out
        for (float t = 0; t < half; t += Time.unscaledDeltaTime)
        {
            source.volume = Mathf.Lerp(startVol, 0f, t / half);
            yield return null;
        }
        source.volume = 0f;

        // Switch clip
        source.clip = clip;
        if (clip) source.Play(); else source.Stop();

        // Fade-in về lại volume cũ
        for (float t = 0; t < half; t += Time.unscaledDeltaTime)
        {
            source.volume = Mathf.Lerp(0f, startVol, t / half);
            yield return null;
        }
        source.volume = startVol;
    }

    public void Stop()
    {
        StopAllCoroutines();
        source.Stop();
        source.clip = null;
    }

    // ======= CUSTOM MP3 (OFFLINE) =======

    public void SetCustomFromFullPath(string fullPath)
    {
        if (!File.Exists(fullPath)) { Debug.LogError("File not found: " + fullPath); return; }

        string ext = Path.GetExtension(fullPath).ToLowerInvariant(); // .mp3/.wav/.ogg
        string destName = "user_bgm" + ext;
        string dest = Path.Combine(Application.persistentDataPath, destName);
        File.Copy(fullPath, dest, true);

        PlayerPrefs.SetInt(K_TYPE, (int)BgmType.Custom);
        PlayerPrefs.SetString(K_PATH, destName);
        PlayerPrefs.Save();

        StopAllCoroutines();
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

            // Dọn clip custom cũ
            if (lastUserClip) { Destroy(lastUserClip); lastUserClip = null; }

            var clip = DownloadHandlerAudioClip.GetContent(req);
            clip.name = "UserBGM";
            lastUserClip = clip;

            Play(clip);
        }
    }

    AudioType GetAudioTypeByExt(string ext)
    {
        ext = ext.ToLowerInvariant();
        if (ext == ".mp3") return AudioType.MPEG;
        if (ext == ".wav") return AudioType.WAV;
        if (ext == ".ogg") return AudioType.OGGVORBIS;
        return AudioType.UNKNOWN;
    }

    public void PickAndSetCustomMP3()
    {
#if SFB   // gkngkc/StandaloneFileBrowser
        var paths = SFB.StandaloneFileBrowser.OpenFilePanel(
            "Chọn file nhạc", "", new[] { new SFB.ExtensionFilter("Audio", "mp3", "wav", "ogg") }, false);
        if (paths != null && paths.Length > 0) SetCustomFromFullPath(paths[0]);
#elif SIMPLE_FILE_BROWSER   // yasirkula/SimpleFileBrowser
        SimpleFileBrowser.FileBrowser.ShowLoadDialog(
            (path) => SetCustomFromFullPath(path),
            null, SimpleFileBrowser.FileBrowser.PickMode.Files,
            false, null, null, "Chọn MP3", "Chọn");
#else
        Debug.Log("Cần tích hợp file picker (SFB hoặc SimpleFileBrowser) và bật macro SFB/SIMPLE_FILE_BROWSER.");
#endif
    }
}
