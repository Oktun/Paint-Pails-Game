using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameOver = false;

    public bool isWin = false;

    public  bool inLastCheckPoint = false;

    [SerializeField] Controller controller;
    [SerializeField] UIHandler uiHandler;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }

        inLastCheckPoint = false;
    }

    private void OnEnable()
    {
        PaintPail.OnGameOver += GameState;
    }

    private void OnDisable()
    {
        PaintPail.OnGameOver -= GameState;
    }

    // this fucntion subcribe to Paint Pail Script
    private void GameState(bool state)
    {
        if(state == true)
        {
            isGameOver = true;
            //Display GameOver Window with Score
            uiHandler.DisplayGameOverWindow(true);
            //Reset Player Position
            controller.ResetPlayerSettings();
            inLastCheckPoint = false;
        }
        else
        {
            //If player in Last Check point and Win
            if(inLastCheckPoint == true && controller.isJumping == false)
            {
                PlayerWin();
                inLastCheckPoint = false;
            }
        }
    }

    //Restart The game and Disbale all Windows
    public void ReStartGame()
    {
        //disable GameOver Window
        uiHandler.DisplayGameOverWindow(false);
        uiHandler.DisplayWinWindow(false);
        uiHandler.ResetScore();
        controller.ResetPlayerSettings();
        isGameOver = false;
        isWin = false;
    }

    //Display Display Win Window and Reset PlayerSettings and Score
    private void PlayerWin()
    {
        uiHandler.DisplayWinWindow(true);
        controller.ResetPlayerSettings();
        isWin = true;
    }
}
