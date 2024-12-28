using UnityEngine;

public class StartScreenManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameManager gameManager;

    public void OnStartButtonClicked()
    {   Debug.Log("Hello");
        startScreen.SetActive(false); // Hide the start screen
        gameManager.StartGame();     // Start the game
    }
}