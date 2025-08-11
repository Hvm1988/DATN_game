using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMovement : MonoBehaviour
{
    [SerializeField] private string _sceneName = "Loading_Intro";

    public void OnClickStart()
    {
        SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
    }    
}