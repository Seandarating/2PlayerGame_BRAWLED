using UnityEngine;
using UnityEngine.SceneManagement; // Needed to reload the level
using UnityEngine.UI; // Needed for the text
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI winText;

    void Start()
    {
        // Make sure the menu is hidden when the game starts
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void TriggerGameOver(string winnerMessage)
    {
        // Show the panel and update the text
        gameOverPanel.SetActive(true);
        winText.text = winnerMessage + " WINS!";
    }

    public void RestartGame()
    {
        // Reloads the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit!"); // This just proves it works in the Unity Editor
        Application.Quit(); // This actually closes the game when it's fully built
    }
}