using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;


public class CheckPoint : MonoBehaviour
{
    public static event Action<bool> OnGameOver;

    public static event Action<bool, int> OnScoreIncrease;

    [SerializeField] private bool lastPaintPail = false;



    [Space]
    [Header("Check Pivots")]
    [SerializeField] private Transform pivotUp;
    [SerializeField] private Transform pivotDown;[Space]

    [Header("Player Pivots")]
    [SerializeField] private Transform playerUpPivot;
    [SerializeField] private Transform playerDownPivot;

    [Space]
    [Header("Shark")]
    public Transform sharkTransform;
    [SerializeField] private Transform sharkEndJump;
    [SerializeField] private float sharkJumpPower;
    [SerializeField] private float sharkJumpDuration;
    private Vector3 sharkStartPos;
    
    // Check the Paint Pail Once and after exit reset it back
    public bool inCheckMode = false;

    private void Start()
    {
        sharkStartPos = sharkTransform.position;
    }

    #region Triggers

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is here");
            if (other.GetComponent<Controller>().isRunning == false )
            {
                CheckDistance();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            SharkMovement();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            inCheckMode = true;
    }

    #endregion


    //Check the Distance between two Vectors
    private bool CheckDistance()
    {

        //if(Vector2.Distance(paintHead, playerHead) < distanceToCheck)
        if(playerUpPivot.position.y > pivotUp.position.y  )
        {
            //Win Conditions here
            //Check if the player Win the Last CheckPoint
            if (lastPaintPail == true)
            {
                GameManager.instance.inLastCheckPoint = true;
            }
            OnGameOver?.Invoke(false);
            if (inCheckMode == true)
            {
                OnScoreIncrease?.Invoke(true, 1);
                inCheckMode = false;
            }
            return true;
        }
        else
        {
            //Lose Conditions here
            OnGameOver?.Invoke(true);
            if (inCheckMode == true)
            {
                OnScoreIncrease?.Invoke(false, 0);
                inCheckMode = false;
            }
            return false;
        }
    }


    #region Shark
    //Shark Jump
    private void SharkMovement()
    {
        this.sharkTransform.DOJump(sharkEndJump.position, sharkJumpPower, 1,
            sharkJumpDuration, false).OnComplete(() =>
            {
                AudioManger.instance.Play("SharkSplash2");
            }
            ) ;
    }

    public void ResetSharkPosition() => sharkTransform.position = sharkStartPos;

    #endregion

}
