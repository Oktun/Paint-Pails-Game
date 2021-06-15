using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private string runString;

    private Animator animator;

    private void Awake() => animator = GetComponent<Animator>();

    public void JumpAnimation(bool state) => animator.SetBool(runString, state);
}
