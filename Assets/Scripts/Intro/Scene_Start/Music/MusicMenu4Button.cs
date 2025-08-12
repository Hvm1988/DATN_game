using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicMenu4Buttons : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] AudioSource mainSource;   // AudioSource nằm trên nút icon mở menu
    [SerializeField] Button[] buttons;         // 4 button trong panel (theo thứ tự)
    [SerializeField] AudioClip[] clips;        // 4 clip tương ứng

    [Header("UI state")]
    [SerializeField] Color normalColor = new Color(1, 1, 1, 0.6f);
    [SerializeField] Color selectedColor = new Color(1f, 0.9f, 0.2f, 1f);

    const string K = "bgm.selected.index";
    int current = -1;
    string[] originalTexts;

    void Start()
    {
        // lưu text gốc để không bị cộng "▶" lặp
        originalTexts = new string[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            int k = i;
            if (buttons[i]) buttons[i].onClick.AddListener(() => Select(k));
            var t = buttons[i].GetComponentInChildren<TMP_Text>();
            originalTexts[i] = t ? t.text : "";
        }

        int saved = PlayerPrefs.GetInt(K, -1);
        if (saved >= 0 && saved < clips.Length) Select(saved, autoplay: false);
        else RefreshVisuals();
    }

    public void Select(int index) => Select(index, true);

    public void Select(int index, bool autoplay)
    {
        if (!mainSource || index < 0 || index >= clips.Length || clips[index] == null) return;

        current = index;
        mainSource.clip = clips[index];
        mainSource.loop = true;
        if (autoplay) mainSource.Play();

        PlayerPrefs.SetInt(K, current);
        PlayerPrefs.Save();

        RefreshVisuals();
    }

    void RefreshVisuals()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            var g = buttons[i].targetGraphic as Graphic;
            if (g) g.color = (i == current) ? selectedColor : normalColor;

            var t = buttons[i].GetComponentInChildren<TMP_Text>();
            if (t) t.text = (i == current ? "▶ " : "") + originalTexts[i];
        }
    }

    // Lấy index bài đang chọn (nếu cần chỗ khác dùng)
    public int GetCurrentIndex() => current;
}
