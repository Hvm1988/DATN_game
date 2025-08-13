using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] string targetScene = "GameMenu";
    [SerializeField] Slider progressBar;      // Min=0, Max=1
    [SerializeField] TMP_Text percentTMP;     // nếu dùng TextMeshPro
    [SerializeField] Text percentText;        // hoặc Text thường
    [SerializeField] float minShowTime = 0.5f;
    [SerializeField] float after100Delay = 0.1f;

    void Start() => StartCoroutine(LoadAsync());

    IEnumerator LoadAsync()
    {
        float start = Time.time;
        var op = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Single);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
        {
            UpdateUI(op.progress / 0.9f);
            yield return null;
        }

        float p = progressBar ? progressBar.value : 0f;
        while (p < 1f)
        {
            p = Mathf.MoveTowards(p, 1f, Time.deltaTime * 1.5f);
            UpdateUI(p);
            yield return null;
        }

        float elapsed = Time.time - start;
        if (elapsed < minShowTime) yield return new WaitForSeconds(minShowTime - elapsed);
        if (after100Delay > 0) yield return new WaitForSeconds(after100Delay);

        op.allowSceneActivation = true; // vào GameMenu
    }

    void UpdateUI(float v)
    {
        if (progressBar) progressBar.value = v;
        int pct = Mathf.RoundToInt(v * 100f);
        if (percentTMP) percentTMP.text = pct + "%";
        if (percentText) percentText.text = pct + "%";
    }
}
