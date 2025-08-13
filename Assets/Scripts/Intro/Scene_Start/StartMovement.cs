using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMovement : MonoBehaviour
{
    [SerializeField] private string _sceneName = "Loading_intro";

    public void OnClickStart()
    {
        SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
    }    
}