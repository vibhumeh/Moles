using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI; // For UI elements like score and timer
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] moles; // Array of moles
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public float popUpDuration = 100f;
    public float moveSpeed = 1f;
    public float intervalBetweenMoles = 3f;
    public float gameDuration = 60f; // Game duration in seconds

    private int score = 0;
    private float remainingTime;

    private void Start()


    {
        remainingTime = gameDuration;
        StartCoroutine(ActivateRandomMoles());
        StartCoroutine(GameTimer());
        UpdateScoreText();
    }

    private IEnumerator ActivateRandomMoles()
    {
        while (remainingTime > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, moles.Length);
            MoleBehavior mole = moles[randomIndex].GetComponent<MoleBehavior>();
            mole.PopUp(moveSpeed, popUpDuration);

            // Listen for mole hits
            mole.OnMissed += ResetScore; // Reset score if missed
            mole.OnHit += IncrementScore; // Increment score if hit

            //yield return new WaitForSeconds(intervalBetweenMoles);
            yield return new WaitForSeconds(3);

            // Stop listening to mole after its cycle
            mole.OnMissed -= ResetScore;
            mole.OnHit -= IncrementScore;
        }
    }

    private void IncrementScore()
    {
        score++;
        UpdateScoreText();
    }

    private void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

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
    }
}
