using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI; // For UI elements like score and timer
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] moles; // Array of moles
    public GameObject startScreen;
    public GameObject endScreen_l;
    public GameObject endScreen_w;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public float popUpDuration = 100f;
    public float moveSpeed = 1f;
    public float intervalBetweenMoles = 3f;
    public float gameDuration = 120f; // Game duration in seconds

    private int score = 0;
    private float remainingTime;

    // private void Start()


    // {
    //     remainingTime = gameDuration;
    //     StartCoroutine(ActivateRandomMoles());
    //     StartCoroutine(GameTimer());
    //     UpdateScoreText();
    // }
        private void Start()
    {
        // Show the start screen at the beginning
        startScreen.SetActive(true);
        endScreen_w.SetActive(false); // Hide the end screen
        endScreen_l.SetActive(false); // Hide the end screen
    }

    public void StartGame()
    {
        // Initialize game variables
        remainingTime = gameDuration;
        score = 0;
        UpdateScoreText();

        // Start coroutines for the game
        StartCoroutine(ActivateRandomMoles());
        StartCoroutine(GameTimer());
    }


    private IEnumerator ActivateRandomMoles()
{
    while (remainingTime > 0)
    {
        int randomIndex = UnityEngine.Random.Range(0, moles.Length);
        MoleBehavior mole = moles[randomIndex].GetComponent<MoleBehavior>();

        // Subscribe to the hit event
        mole.OnHit += IncrementScore;

        // Activate the mole
        mole.PopUp(moveSpeed, popUpDuration);

        // Wait for the mole's cycle to finish
        // Debug.Log("H1");
        yield return new WaitForSeconds(1);
        // Debug.Log("H2");

        // Unsubscribe from the hit event
        mole.OnHit -= IncrementScore;
    }
}

    private void IncrementScore()
    {
        score++;
        UpdateScoreText();
    }

    // private void ResetScore()
    // {
    //     score = 0;
    //     UpdateScoreText();
    // }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    private IEnumerator GameTimer()
    {
        while (remainingTime > 0)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(remainingTime) + "s";
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        EndGame();
    }

    private void EndGame()
    {
        Debug.Log("Game Over! Final Score: " + score);
        timerText.text = "Time: 0s";
        StopAllCoroutines();
        if(score>20) endScreen_w.SetActive(true);
        else endScreen_l.SetActive(false); // Hide the end screen

    }
}
