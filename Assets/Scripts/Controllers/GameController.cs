using System.Collections;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private TextMeshProUGUI pauseButtonText;
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
            pauseButtonText.text = "Pause";
            isGamePaused = false;
        }

        else
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            pauseButtonText.text = "Unpause";
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
        StartCoroutine(WaitThenEndGame(isGameWon));
    }

    IEnumerator WaitThenEndGame(bool isGameWon)
    {
        yield return new WaitForSecondsRealtime(2f);
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
