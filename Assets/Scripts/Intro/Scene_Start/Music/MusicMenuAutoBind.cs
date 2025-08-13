using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class MusicMenuAutoBind : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] AudioSource main;          // AudioSource chính (ở nút icon)
    [SerializeField] Transform buttonsRoot;     // cha chứa 4 button nhạc có sẵn
    [SerializeField] AudioClip[] builtInClips;  // 4 bài mặc định (cùng thứ tự với button)
    [SerializeField] Button btnPick;            // nút "Tải nhạc"
    [SerializeField] int defaultIndex = 0;      // bài mặc định cho người mới

    const string K_TYPE = "bgm.type";     // 0=built-in, 1=custom
    const string K_INDEX = "bgm.index";   // index built-in
    const string K_PATH = "bgm.custom";   // tên file custom trong persistentDataPath
    enum T { BuiltIn = 0, Custom = 1 }

    void Awake()
    {
        // bind 4 button built-in theo thứ tự
        var btns = buttonsRoot.GetComponentsInChildren<Button>(true);
        for (int i = 0; i < btns.Length && i < builtInClips.Length; i++)
        {
            int k = i; btns[i].onClick.AddListener(() => PlayBuiltIn(k, save: true));
        }
        if (btnPick) btnPick.onClick.AddListener(OnClickPick);
    }

    void Start()
    {
        // phát lại lựa chọn đã lưu, nếu không có → phát bài mặc định
        var type = (T)PlayerPrefs.GetInt(K_TYPE, (int)T.BuiltIn);
        if (type == T.Custom && PlayerPrefs.HasKey(K_PATH))
        {
            string rel = PlayerPrefs.GetString(K_PATH);
            string full = Path.Combine(Application.persistentDataPath, rel);
            if (File.Exists(full)) { StartCoroutine(LoadAndPlayFromPath(full)); return; }
        }

        int idx = PlayerPrefs.HasKey(K_INDEX)
            ? Mathf.Clamp(PlayerPrefs.GetInt(K_INDEX), 0, builtInClips.Length - 1)
            : Mathf.Clamp(defaultIndex, 0, builtInClips.Length - 1);

        PlayBuiltIn(idx, save: !PlayerPrefs.HasKey(K_INDEX)); // lần đầu thì lưu mặc định
    }

    // ===== built-in =====
    void PlayBuiltIn(int i, bool save)
    {
        if (!main || i < 0 || i >= builtInClips.Length || !builtInClips[i]) return;
        main.loop = true;
        main.clip = builtInClips[i];
        main.Play();
        if (save)
        {
            PlayerPrefs.SetInt(K_TYPE, (int)T.BuiltIn);
            PlayerPrefs.SetInt(K_INDEX, i);
            PlayerPrefs.Save();
        }
    }

    // ===== custom pick & persist =====
    public void OnClickPick()
    {
#if UNITY_EDITOR
        string p = UnityEditor.EditorUtility.OpenFilePanel("Chọn nhạc", "", "mp3,wav,ogg");
        if (!string.IsNullOrEmpty(p)) SetCustomFromFullPath(p);

#elif (UNITY_STANDALONE || UNITY_WSA) && SFB
    var paths = SFB.StandaloneFileBrowser.OpenFilePanel("Chọn nhạc", "", "mp3,wav,ogg", false);
    if (paths != null && paths.Length > 0) SetCustomFromFullPath(paths[0]);

#elif SIMPLE_FILE_BROWSER
    SimpleFileBrowser.FileBrowser.ShowLoadDialog(
        (path)=> SetCustomFromFullPath(path),
        null, SimpleFileBrowser.FileBrowser.PickMode.Files,
        false, null, null, "Chọn nhạc", "Chọn");

#elif (UNITY_ANDROID || UNITY_IOS) && NATIVE_FILE_PICKER
    NativeFilePicker.PickFile((path)=> { if (path != null) SetCustomFromFullPath(path); },
                              new string[]{ "audio/*" });

#else
    Debug.LogWarning("Chưa có file picker. Cài SFB/SimpleFileBrowser (PC) hoặc NativeFilePicker (mobile) và thêm define tương ứng.");
#endif
    }
    void SetCustomFromFullPath(string fullPath)
    {
        try
        {
            string ext = Path.GetExtension(fullPath).ToLowerInvariant();
            string destName = "user_bgm" + ext;
            string dest = Path.Combine(Application.persistentDataPath, destName);

            File.Copy(fullPath, dest, true);
            Debug.Log($"[BGM] Copied to: {dest}");

            PlayerPrefs.SetInt("bgm.type", 1);      // Custom
            PlayerPrefs.SetString("bgm.custom", destName);
            PlayerPrefs.Save();
        }
        catch (System.Exception e)
        {
            Debug.LogError("[BGM] Copy failed: " + e);
        }
    }

    IEnumerator LoadAndPlayFromPath(string full)
    {
        string url = "file://" + full.Replace("\\", "/");
        AudioType aType = AudioType.UNKNOWN;
        string ext = Path.GetExtension(full).ToLowerInvariant();
        if (ext == ".mp3") aType = AudioType.MPEG; else if (ext == ".wav") aType = AudioType.WAV; else if (ext == ".ogg") aType = AudioType.OGGVORBIS;

        using (var req = UnityWebRequestMultimedia.GetAudioClip(url, aType))
        {
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success) { Debug.LogError(req.error); yield break; }
            var clip = DownloadHandlerAudioClip.GetContent(req);
            clip.name = "UserBGM";
            main.loop = true;
            main.clip = clip;
            main.Play();
        }
    }
}
