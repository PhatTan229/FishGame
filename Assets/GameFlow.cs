using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;

    private void Start()
    {
        gameOverCanvas.gameObject.SetActive(false);
    }
    public void ShowGameOver()
    {
        gameOverCanvas.gameObject.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
}
