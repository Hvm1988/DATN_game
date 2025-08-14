using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingHelper : MonoBehaviour
{
    private void Start()
    {
        // Bắt đầu coroutine load scene khi script chạy
        StartCoroutine(loadSceneCor());
    }

    private IEnumerator loadSceneCor()
    {
        // Kiểm tra xem đây có phải lần chạy đầu tiên của phiên bản này không
        // Nếu chưa có key hoặc phiên bản hiện tại khác phiên bản đã lưu -> lần đầu
        bool firstRunThisVersion = !PlayerPrefs.HasKey("ver_loaded")
                                   || PlayerPrefs.GetString("ver_loaded") != Application.version;
        
        float UPDATE_INTERVAL = firstRunThisVersion ? 0.07f : 0.02f; // Lần đầu chậm, lần sau nhanh
        float MIN_SHOW_SEC = firstRunThisVersion ? 5.0f : 2.0f;  // Lần đầu giữ lâu hơn
        float startTime = Time.time;                           // Ghi lại thời gian bắt đầu

        // Load scene bất đồng bộ
        this.sceneAO = SceneManager.LoadSceneAsync(this.sceneName);
        this.sceneAO.allowSceneActivation = false; // Không cho chuyển scene ngay

        int shownPercent = 0; // % đang hiển thị trên UI

        // Vòng lặp chạy cho đến khi scene load xong
        while (!this.sceneAO.isDone)
        {
            // Chờ theo khoảng UPDATE_INTERVAL (để kiểm soát tốc độ %)
            yield return new WaitForSeconds(UPDATE_INTERVAL);

            // Lấy tiến độ thật, clamp về tối đa 90% khi chưa activate
            int targetPercent = Mathf.Clamp(Mathf.FloorToInt(this.sceneAO.progress * 100f), 0, 90);

            // Nếu chưa đạt tiến độ thật thì tăng % hiển thị lên dần
            if (shownPercent < targetPercent)
                shownPercent += 5;
            // Nếu đã đạt 90% thật, tự tăng % hiển thị tới 100%
            else if (this.sceneAO.progress >= 0.9f && shownPercent < 100)
                shownPercent += 5;

            // Cập nhật thanh và text % trên UI
            this.loadingBar.fillAmount = shownPercent / 100f;
            this.loadingStatus.text = "Loading " + shownPercent + "%";

            // Khi scene đã load xong (>=90%), % UI đã đạt 100%, và đã qua MIN_SHOW_SEC
            if (this.sceneAO.progress >= 0.9f && shownPercent >= 100 && (Time.time - startTime) >= MIN_SHOW_SEC)
            {
                // Cho phép chuyển scene
                this.sceneAO.allowSceneActivation = true;

                // Nếu là lần đầu chạy -> lưu lại phiên bản hiện tại
                if (firstRunThisVersion)
                {
                    PlayerPrefs.SetString("ver_loaded", Application.version);
                    PlayerPrefs.Save();
                }
            }
        }
    }

    private IEnumerator timeOutCor()
    {
        // Giữ nguyên code gốc của bạn nếu cần timeout xử lý Firebase
        yield break;
    }

    public void updateLoadingStatus(string str)
    {
        // Cập nhật text trạng thái loading
        this.loadingStatus.text = "----" + str;
    }

    // Các biến tham chiếu UI và dữ liệu
    public Image loadingBar;
    public Text loadingStatus;
    public DataHolder dataHolder;

    private AsyncOperation sceneAO;
    public string sceneName;
    private bool loadedFirebase;
}
