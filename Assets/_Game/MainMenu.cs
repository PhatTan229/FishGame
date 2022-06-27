using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    const float REFERENCE_X = 1920;

    [SerializeField] GameObject canvas;

    GameObject popupMain;
    GameObject popupMatching;
    GameObject middleGroup;

    Button btnPlay;

    private void Awake() {
        popupMain = canvas.GetChildComponent<GameObject>("popupMain");
        popupMatching = canvas.GetChildComponent<GameObject>("popupMatching");
        middleGroup = popupMain.GetChildComponent<GameObject>("middleGroup");

        btnPlay = popupMain.GetChildComponent<Button>("btnPlay");

        btnPlay.onClick.AddListener(Play);
        popupMatching.SetActive(false);
    }

    public void Play() {
        //popupMain.transform.position = new Vector3(0, popupMain.transform.position.y);
        //popupMain.transform.DOMoveX(-REFERENCE_X, 1f);

        //popupMatching.gameObject.SetActive(true);
        //popupMatching.transform.position = new Vector3(REFERENCE_X * 1.5f, popupMatching.transform.position.y);
        //popupMatching.transform.DOMoveX(0, 1f);
        popupMain.gameObject.SetActive(false);
        popupMatching.gameObject.SetActive(true);

        GameSystem.DelayCall(2f, () => {
            SceneManager.LoadScene("Gameplay");
        });
    }

    public void ShowMainMenu() {
        popupMain.transform.DOMoveX(0, popupMain.transform.position.y);
        popupMatching.gameObject.SetActive(true);
        popupMatching.transform.position = new Vector3(0, popupMatching.transform.position.y);
        popupMatching.transform.DOMoveX(Screen.width, 1f);
    }
}
