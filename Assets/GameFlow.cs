using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    public static GameFlow Instance;

    [SerializeField] GameObject canvasGameplay;

    GameObject popupGameOver;
    PlayerController player;
    Button btnDash;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        popupGameOver = canvasGameplay.GetChildComponent<GameObject>("popupGameOver");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        btnDash = canvasGameplay.GetChildComponent<Button>("btnDash");

        popupGameOver.SetActive(false);
        btnDash.onClick.AddListener(player.Dash);
    }

    public void ShowGameOver()
    {
        popupGameOver.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
