using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource screamingSound;

    private void OnEnable()
    {
        screamingSound.Play();
    }
}
