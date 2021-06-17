using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Body"))
        {
            GameManager.instance.GameState(true);
            AudioManger.instance.Play("ImpactSharkGirl");
        }
    }
}
