using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameOver = false;

    public bool isWin = false;

    public  bool inLastCheckPoint = false;

    public List<Shower>  covers = new List<Shower>();

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
        Shower.OnGameOver += GameState;
    }

    private void Update()
    {
        if(isGameOver == true || isWin == true)
            animationHandler.RunAnimation(false);
    }

    private void OnDisable()
    {
        Shower.OnGameOver -= GameState;
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
        //disable GameOver Window
        uiHandler.DisplayGameOverWindow(false);
        uiHandler.DisplayWinWindow(false);
        uiHandler.ResetScore();
        controller.ResetPlayerSettings();
        isGameOver = false;
        isWin = false;
        ResetCoverPositionHandler();
        ResetSharkSettings();
    }

    //Display Display Win Window and Reset PlayerSettings and Score
    private void PlayerWin()
    {
        uiHandler.DisplayWinWindow(true);
        controller.ResetPlayerSettings();
        isWin = true;
    }

    //Reset the Cover setting after Lose or Win 
    private void ResetCoverPositionHandler()
    {
        Debug.Log("RESET COVER ");
        for (int i = 0; i < covers.Count -1; i++)
        {
            if(covers[i].cover != null)
            {
                covers[i].ResetCoverPosition();
                covers[i].moveOnce = false;
                covers[i].inCheckMode = true;
                covers[i].cover.transform.DOKill(false);
                StartCoroutine(ResetMovementCoroutine());
            }
        }
    }

    private void ResetSharkSettings()
    {
        for (int i = 0; i < covers.Count -1; i++)
        {
                covers[i].ResetSharkPosition();
                covers[i].sharkTransform.DOKill(false);
        }
    }

    //Reset The cover move once state after 0.5 seconds of losing or wining
    IEnumerator ResetMovementCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < covers.Count - 1; i++)
        {
            if (covers[i].cover != null)
            {
                covers[i].moveOnce = true;
            }
        }
    }
}
