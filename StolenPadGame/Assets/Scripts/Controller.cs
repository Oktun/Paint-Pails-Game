using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Controller : MonoBehaviour
{
    public static event Action OnJumpEnded;

    [HideInInspector]
    public bool isRunning;

    [Header("Timer")]
    [SerializeField] private float cooldownTime = 2f;
    [SerializeField] private float timer = 2f;

    [Space]
    [Header("Move Settings")]
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
    [SerializeField] private float scaleMaxLimit = 1.2f;
    [SerializeField] private float scaleMinLimit = 0.4f;
    [SerializeField] private bool canScallInshower = false;
    private float currentScale;

    private bool isGameStarted = false;
    private Vector3 startPosition;
    private Vector3 startScale;

    private AnimationHandler animationHandler ;

    private void Awake()
    {
        startPosition = transform.position;
        startScale = transform.localScale;
        animationHandler = GetComponent<AnimationHandler>();
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
        isRunning = true;
        transform.DOMove(checkPointsList[checkPointIndex].position, moveDuration, false).OnComplete(() =>
        {
            isRunning = false;
            if (checkPointsList[checkPointIndex].parent.GetComponent<Shower>().isMovable)
            {
                OnJumpEnded?.Invoke();
            }
            IncreaseIndex();
        }
        );
    }

    private void  Running() => isRunning = false;

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

        if (isRunning || canScallInshower)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if(currentScale < scaleMaxLimit)
                {
                    transform.DOScaleY(currentScale + scaleValue, 0);
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (currentScale > scaleMinLimit)
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

    //Player can scall if the shower is Movable
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shower"))
        {
            animationHandler.RunAnimation(false);
            if (other.GetComponent<Shower>().isMovable == true)
            {
                canScallInshower = true;
            }
            else
            {
                canScallInshower = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shower"))
            animationHandler.RunAnimation(true);

    }
}
