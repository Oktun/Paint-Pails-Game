using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;


public class Shower : MonoBehaviour
{
    public static event Action<bool> OnGameOver;

    public static event Action<bool, int> OnScoreIncrease;

    [SerializeField] private bool lastPaintPail = false;

    [Space]
    [Header("Cover of the Shower")]
    public bool moveOnce = true;
    public bool isMovable = false;
    public GameObject cover;

    [SerializeField] private Transform moveTowardTransform;
    [SerializeField] private float coverMoveAfter = 1f;
    [SerializeField] private float durationMovement = 2f;
    private Vector3 coverStartTransform;


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
        if(cover != null)
            coverStartTransform = cover.transform.position;

        sharkStartPos = sharkTransform.position;
    }

    //private void OnEnable() => Controller.OnJumpEnded += SharkMovement;

    //private void OnDisable() => Controller.OnJumpEnded -= SharkMovement;

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
            //CoverMovement();
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
        if(playerUpPivot.position.y > pivotUp.position.y && 
            playerDownPivot.position.y < pivotUp.position.y)
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


    #region Cover
    //Cover Movement Y axe
    private void CoverMovement()
    {
        if (cover == null) { return; }

        if(GameManager.instance.isGameOver == true) { return; }

        if (isMovable == true )
        {
            if (moveOnce == true)
            {
                StartCoroutine(CoverMoveRoutine());
                moveOnce = false;
                Debug.Log("Tween completed");
            }
        }
    }

    IEnumerator CoverMoveRoutine()
    {
        yield return new WaitForSeconds(coverMoveAfter);
        cover.transform.DOMoveY(moveTowardTransform.position.y,
                    durationMovement, false);
    }

    public void ResetCoverPosition() => cover.transform.position = coverStartTransform;
    #endregion

    #region Shark
    //Shark Jump
    private void SharkMovement()
    {
        this.sharkTransform.DOJump(sharkEndJump.position, sharkJumpPower, 1, sharkJumpDuration,
            false);
    }

    public void ResetSharkPosition() => sharkTransform.position = sharkStartPos;

    #endregion


    // Reset the cover position the start position

}
