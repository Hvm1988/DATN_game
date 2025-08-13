using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class BmgManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    void Start()
    {
        panel.SetActive(false);
    }    
    public void ShowPanel()
    {
        panel.SetActive(true);
    }    
}