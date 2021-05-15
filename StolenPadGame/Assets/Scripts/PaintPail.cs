using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PaintPail : MonoBehaviour
{
    public static event Action<bool> OnGameOver;

    [SerializeField] private bool lastPaintPail = false;
    [SerializeField] private float distanceToCheck = 0.2f;
    
    // Check the Paint Pail Once and after exit reset it back
    private bool inCheckMode = false; 

    private enum ColorNumber
    {
        OneColor,
        TwoColor,
        ThreeColor,
    }

    [SerializeField] private ColorNumber colorNumber;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is here");
            if (other.GetComponent<Controller>().isJumping == false && inCheckMode == false)
            {
                switch (colorNumber)
                {
                    case ColorNumber.OneColor:
                        CheckDistance(transform.GetChild(0).position,
                            other.transform.GetChild(0).position);
                        break;
                    case ColorNumber.TwoColor:
                        CheckDistance(transform.GetChild(0).position,
                            other.transform.GetChild(1).position);
                        break;
                    case ColorNumber.ThreeColor:
                        CheckDistance(transform.GetChild(0).position,
                            other.transform.GetChild(2).position);
                        break;
                    default:
                        break;
                }
                inCheckMode = true;
            }
        }
    }

    //Reset the inCheckMode to false
    private void OnTriggerExit(Collider other) => inCheckMode = false;

    //Check the Distance between two Vectors
    private bool CheckDistance(Vector3 paintHead, Vector3 playerHead)
    {
        Debug.Log(Vector2.Distance(paintHead, playerHead));

        if(Vector2.Distance(paintHead, playerHead) < distanceToCheck)
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
