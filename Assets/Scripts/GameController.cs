using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject pauseScreen;
    public static bool isGamePaused = false;

    public static int EnemiesSlayed = 0;

    public static GameController instance;


    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        PlayerSpawner.instance.InvokeRepeating(nameof(PlayerSpawner.instance.IncreaseIngredientCount), 5f, 5f);
    }


    public void TogglePauseMenu()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            isGamePaused = false;
        }

        else
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            isGamePaused = true;
        }
    }


    public void TogglePauseGame()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1;
            isGamePaused = false;
        }

        else
        {
            isGamePaused = true;
            Time.timeScale = 0;
        }
    }


    public void EndGame(bool isGameWon)
    {
        TogglePauseGame();

        if (isGameWon)
        {
            winScreen.SetActive(true);
        }
        else
        {
            loseScreen.SetActive(true);
        }
    }
}
