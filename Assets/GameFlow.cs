using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    public static GameFlow Instance;

    [SerializeField] GameObject canvasGameplay;

    GameObject popupGameOver;
    PlayerController player;
    Slider sliderStamina;
    Button btnDash;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        popupGameOver = canvasGameplay.GetChildComponent<GameObject>("popupGameOver");
        sliderStamina = canvasGameplay.GetChildComponent<Slider>("sliderStamina");
        btnDash = canvasGameplay.GetChildComponent<Button>("btnDash");

        popupGameOver.SetActive(false);
    }

    private void Update()
    {
        sliderStamina.value = player.Stamina / Constants.MAX_STAMINA;
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
