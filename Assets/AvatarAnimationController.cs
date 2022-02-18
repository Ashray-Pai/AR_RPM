using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarAnimationController : MonoBehaviour
{
    [SerializeField] private AvatarImporter avatarImporter;

    private Animator animator;
    //[SerializeField] private AnimationClip[] animationClips;
    [SerializeField] private RuntimeAnimatorController[] controllers;
    //[SerializeField] private AnimatorOverrideController overrideController;

    private bool isAnimatorAssigned = false;

    private void Update()
    {
        if (avatarImporter.ImportedAvatar != null && !isAnimatorAssigned)
        {
            animator = avatarImporter.ImportedAvatar.transform.GetComponent<Animator>();
            isAnimatorAssigned= true;
        }
    }

    public void StartAnimation()
    {
        animator.runtimeAnimatorController = controllers[0];
        animator.Play("Walk");
        Debug.Log("<<Play animation>>>>");
    }

    public void StopAnimation()
    {
        Debug.Log("<<Stop animation>>>>");

        animator.runtimeAnimatorController = controllers[1];
        animator.Play("Idle");


    }


}
