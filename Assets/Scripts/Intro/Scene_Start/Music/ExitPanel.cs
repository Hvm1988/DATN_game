using UnityEngine;

public class ExitPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;   // Kéo Panel cần hiện vào đây
 public void HidePanel()
    {
        panel.SetActive(false);
    }    
}
