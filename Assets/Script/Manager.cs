using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject respawnPoint;
    public GameObject player;
    public GameObject gameOverUI;

    void Start()
    {
        // Ensure UI is hidden at the start of the game
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    public void GameOver()
    {
        // Activates the game over UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // Deactivates the player object
        if (player != null)
        {
            player.SetActive(false);
        }
    }

    public void RestartGame()
    {
        // 1. CRITICAL: Unfreeze time in case it was paused on death
        Time.timeScale = 1f;

        // 2. Reloads the active scene safely
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}