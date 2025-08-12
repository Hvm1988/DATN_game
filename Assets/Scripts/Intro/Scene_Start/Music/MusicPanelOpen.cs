using UnityEngine;

public class MusicPanelOpen : MonoBehaviour
{
    [SerializeField] private GameObject panel;   // Kéo Panel cần hiện vào đây
    [SerializeField] private bool hideOnStart = true;

    void Start()
    {
        if (panel && hideOnStart) panel.SetActive(false);
    }

    // Gọi từ Button OnClick
    public void Show() { if (panel) panel.SetActive(true); }
    public void Hide() { if (panel) panel.SetActive(false); }
    public void Toggle() { if (panel) panel.SetActive(!panel.activeSelf); }
}

