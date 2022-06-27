using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    TextMeshProUGUI txtLoading;
    Image imgLoading;

    float speed = 0.5f;
    bool finish = false;

    private void Awake() {
        txtLoading = canvas.GetChildComponent<TextMeshProUGUI>("imgLoadingBackground/txtLoading");
        imgLoading = canvas.GetChildComponent<Image>("imgLoadingBackground/imgLoading");
        imgLoading.fillAmount = 0;
    }

    private void Update() {
        imgLoading.fillAmount += speed * Time.deltaTime;
        if (imgLoading.fillAmount >= 1f) {
            if (finish) return;
            finish = true;
            SceneManager.LoadScene("Gameplay");
        }
    }
}
