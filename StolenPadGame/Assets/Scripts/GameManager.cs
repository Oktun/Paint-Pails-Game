using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameOver = false;

    public bool isWin = false;

    public  bool inLastCheckPoint = false;

    public List<CheckPoint>  covers = new List<CheckPoint>();

    [SerializeField] Controller controller;
    [SerializeField] UIHandler uiHandler;
    [SerializeField] AnimationHandler animationHandler ;

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
        CheckPoint.OnGameOver += GameState;
    }

    private void Update()
    {
        if(isGameOver == true || isWin == true)
            animationHandler.JumpAnimation(false);
    }

    private void OnDisable()
    {
        CheckPoint.OnGameOver -= GameState;
    }

    // this fucntion subcribe to Paint Pail Script
    public void GameState(bool state)
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
            if(inLastCheckPoint == true && controller.isRunning == false)
            {
                PlayerWin();
                inLastCheckPoint = false;
            }
        }
    }

    //Restart The game and Disbale all Windows
    public void ReStartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex  );
    }

    public void NextLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    //Display Display Win Window and Reset PlayerSettings and Score
    private void PlayerWin()
    {
        uiHandler.DisplayWinWindow(true);
        controller.ResetPlayerSettings();
        isWin = true;
    }

}
