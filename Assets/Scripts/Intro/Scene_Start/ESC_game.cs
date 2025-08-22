using UnityEngine;

public class ESC_game : MonoBehaviour
{
    public void QuitGame()
    {
        // Thoát app khi build
        Application.Quit();

#if UNITY_EDITOR
        // Khi đang Play trong Editor thì dừng
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
