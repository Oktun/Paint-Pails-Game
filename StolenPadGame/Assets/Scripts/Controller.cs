using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Controller : MonoBehaviour
{

    public bool isJumping;

    [Header("Timer")]
    [SerializeField] private float cooldownTime = 2f;
    [SerializeField] private float timer = 2f;

    [Space]
    [Header("Move Settings")]
    [SerializeField] private Ease easeType;
    [SerializeField] private float moveDuration = 2f;
    [SerializeField] private float rotateDuration = 2f;
    [SerializeField] private float jumpPower = 100f;

    [Space]
    [Header("CheckPoints")]
    [SerializeField] private List<Transform> checkPointsList = new List<Transform>();
    [SerializeField] private int checkPointIndex = 0;

    [Space]
    [Header("Scale Settings")]
    [SerializeField] private float scaleValue = 0.1f;
    [SerializeField] private float scaleLimit = 0.4f;
    private float currentScale;

    private bool isGameStarted = false;
    private Vector3 startPosition;
    private Vector3 startScale;

    private void Awake()
    {
        startPosition = transform.position;
        startScale = transform.localScale;
    }

    private void Update()
    {
        if(GameManager.instance.isGameOver == false )
        {
            if (isGameStarted)
            {
                //Do the Scall
                PenScale();
        
                //Do the Movemeent
                TimeBetweenMovement();
            }
        }
    }


    #region Pen Movement

    //Time Between Movement
    private void TimeBetweenMovement()
    {
        if(timer >= cooldownTime)
        {
            PlayerMovement();
            IncreaseIndex();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    // Move The Pen Toward Paint Pails
    private void PlayerMovement()
    {
        isJumping = true;
        transform.DORotate(new Vector3(0, 0, 360), 1.4f, RotateMode.FastBeyond360)
            .SetEase(easeType);
        //transform.DOMove(checkPointsList[checkPointIndex].position, moveSpeed, false);
        transform.DOJump(checkPointsList[checkPointIndex].position, jumpPower, 1, moveDuration,
            false).OnComplete(Jamping);
    }

    private void  Jamping() => isJumping = false;

    // Update the next index to Move Toward Paint Pails
    private void IncreaseIndex()
    {
        if (checkPointIndex < checkPointsList.Count - 1)
            checkPointIndex++;
    }

    #endregion


    #region Scale

    //Scale the Pen with UpArrow and DownArrow 
    private void PenScale()
    {
        currentScale = transform.localScale.y;

        if (isJumping)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.DOScaleY(currentScale + scaleValue, 0);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (currentScale > scaleLimit)
                {
                    transform.DOScaleY(currentScale - scaleValue, 0);
                }
            }
        }
    }

    #endregion

    // Linked to button to Start the game
    public void StartGame() => isGameStarted = true;

    // Reset Player Settings
    public void ResetPlayerSettings()
    {
        transform.position = startPosition;
        transform.localScale = startScale;
        isGameStarted = false;
        checkPointIndex = 0;
        timer = cooldownTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Shower"))
            // if shower is with type movable cover here we let the player to use the scale
            // while he's in the shower
            // in trigger exit the variable wil reset to false
    }
}
