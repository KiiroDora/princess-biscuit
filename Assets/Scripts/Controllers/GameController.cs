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

    public static int currentGameSpeed = 1;

    public static GameController instance;


    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        PlayerSpawner.instance.InvokeRepeating(nameof(PlayerSpawner.instance.IncreaseIngredientCount), 5f, 5f);
        EnemiesSlayed = 0;
        currentGameSpeed = 1;
        Time.timeScale = 1;
    }


    public void TogglePauseMenu()
    {
        if (isGamePaused)
        {
            Time.timeScale = currentGameSpeed;
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
            Time.timeScale = currentGameSpeed;
            isGamePaused = false;
        }

        else
        {
            isGamePaused = true;
            Time.timeScale = 0;
        }
    }

    public void PauseGame()
    {
        if (!isGamePaused)
        {
            isGamePaused = true;
            Time.timeScale = 0;
        }
    }


    public void ToggleSpeedUpGame(TextMeshProUGUI buttonText)
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 2;
            currentGameSpeed = 2;
            buttonText.text = ">> x2";
        }
        else if (Time.timeScale == 2)
        {
            Time.timeScale = 3;
            currentGameSpeed = 3;
            buttonText.text = ">> x3";
        }
        else if (Time.timeScale == 3)
        {
            Time.timeScale = 1;
            currentGameSpeed = 1;
            buttonText.text = ">> x1";
        }
    }


    public void EndGame(bool isGameWon)
    {
        StartCoroutine(WaitThenEndGame(isGameWon));
    }

    IEnumerator WaitThenEndGame(bool isGameWon)
    {
        yield return new WaitForSecondsRealtime(2f);
        PauseGame();

        if (isGameWon && !winScreen.activeSelf && !loseScreen.activeSelf)
        {
            winScreen.SetActive(true);
            AudioPlayer.instance.PlayAudio("Level Clear");
        }
        else if (!isGameWon && !winScreen.activeSelf && !loseScreen.activeSelf)
        {
            loseScreen.SetActive(true);
            AudioPlayer.instance.PlayAudio("Game Over");
        }
    }
}
