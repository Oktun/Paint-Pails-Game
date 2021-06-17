using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

    [SerializeField] private Text currentScoreText;
    [SerializeField] private Text gameOverScoreText;
    [SerializeField] private Text winScoreText;
    [SerializeField] private GameObject gameOverWindow;
    [SerializeField] private GameObject winWindow;
    private int score = 0;

    private void OnEnable()
    {
        CheckPoint.OnScoreIncrease += ScoreUpdater;
    }

    private void OnDisable()
    {
        CheckPoint.OnScoreIncrease -= ScoreUpdater;
    }

    // Update the Score in Real time
    private void ScoreUpdater(bool state,int value)
    {
        if (state == true)
        {
            score += value;
            Debug.Log("<<<<<<SCORE INCREASE>>>>");
            //currentScoreText.text = score.ToString();
            //winScoreText.text = "Score: " + score.ToString();
            //gameOverScoreText.text = "Score: " + score.ToString();
        }
        else
        {
            //gameOverScoreText.text ="Score: " + score.ToString();
            //ResetScore();
        }
    }

    //Reset the Score
    public void ResetScore()
    {
        score = 0;
        currentScoreText.text = score.ToString();
    }

    //Display GameOver Window
    public void DisplayGameOverWindow(bool state)
    {
        gameOverWindow.SetActive(state);
        AudioManger.instance.Play("Lose");
    }

    //Display Win Window
    public void DisplayWinWindow(bool state)
    {
        winWindow.SetActive(state);
        AudioManger.instance.Play("Win");
    }

    public void ButtonSound()
    {
        AudioManger.instance.Play("Button");
    }
}
