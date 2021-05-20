using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PaintPail : MonoBehaviour
{
    public static event Action<bool> OnGameOver;

    [SerializeField] private bool lastPaintPail = false;

    [Space]
    [Header("Check Pivots")]
    [SerializeField] private Transform pivotUp;
    [SerializeField] private Transform pivotDown;[Space]
    [Header("Player Pivots")]
    [SerializeField] private Transform playerUpPivot;
    [SerializeField] private Transform playerDownPivot;
    
    // Check the Paint Pail Once and after exit reset it back
    private bool inCheckMode = false; 

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is here");
            if (other.GetComponent<Controller>().isJumping == false && inCheckMode == false)
            {
                CheckDistance();

                inCheckMode = true;
            }
        }
    }

    //Reset the inCheckMode to false
    private void OnTriggerExit(Collider other) => inCheckMode = false;

    //Check the Distance between two Vectors
    private bool CheckDistance()
    {

        //if(Vector2.Distance(paintHead, playerHead) < distanceToCheck)
        if(playerUpPivot.position.y < pivotUp.position.y && 
            playerDownPivot.position.y > pivotDown.position.y)
        {
            //Check if the player Win the Last CheckPoint
            if (lastPaintPail == true)
            {
                GameManager.instance.inLastCheckPoint = true;
            }
            OnGameOver?.Invoke(false);
            return true;
        }
        else
        {
            OnGameOver?.Invoke(true);
            return false;
        }
    }

}
