using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class OnlineLoading : MonoBehaviour
{
    Image imgLoading;
    Image imgFlag;
    float loadingFinish = 0f;

    private void Awake() {
        imgLoading = gameObject.GetChildComponent<Image>("imgLoading");
        imgFlag = gameObject.GetChildComponent<Image>("imgFlag");
    }

    public void Loading() {
        loadingFinish = Time.time + Random.Range(0f, Constants.ONLINE_MATCHING_TIME);
    }

    private void Update() {
        imgLoading.gameObject.SetActive(Time.time < loadingFinish);
        imgFlag.gameObject.SetActive(Time.time > loadingFinish);
    }
}
