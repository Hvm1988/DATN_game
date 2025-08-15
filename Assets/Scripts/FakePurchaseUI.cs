using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FakePurchaseUI : MonoBehaviour
{
    public static FakePurchaseUI I;
    public GameObject panel;
    public Text titleTxt, priceTxt;
    public Button btnConfirm, btnCancel;

    Action _onOk, _onCancel;

    void Awake()
    {
        if (I && I != this) { Destroy(gameObject); return; }
        I = this; DontDestroyOnLoad(gameObject);
        panel.SetActive(false);
        btnConfirm.onClick.AddListener(() => StartCoroutine(Confirm()));
        btnCancel.onClick.AddListener(() => { panel.SetActive(false); _onCancel?.Invoke(); });
    }

    public void Show(string title, string price, Action onOk, Action onCancel = null)
    {
        titleTxt.text = title; priceTxt.text = price;
        _onOk = onOk; _onCancel = onCancel;
        panel.SetActive(true);
    }

    IEnumerator Confirm()
    {
        btnConfirm.interactable = false;
        yield return new WaitForSeconds(0.5f); // giả lập xử lý
        panel.SetActive(false);
        btnConfirm.interactable = true;
        _onOk?.Invoke(); // coi như mua thành công
    }
}
