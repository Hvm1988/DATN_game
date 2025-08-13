using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class MusicMenu4Buttons : MonoBehaviour
{
    public AudioSource main;   // AudioSource chính
    public AudioClip clip;     // bài của nút này

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (!main || !clip) return;
            main.loop = true;
            main.clip = clip;
            main.Play();
        });
    }
}