using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarAnimationControllerTest : MonoBehaviour
{
    [SerializeField] private GameObject avatarGameObject;
    private Animator animator;

    private void Start()
    {
        animator = avatarGameObject.GetComponent<Animator>();
    }

    public void StartAnimation()
    {
        this.animator.SetBool("isMoving", true);
    }

    public void StopAnimation()
    {
        this.animator.SetBool("isMoving", false);
    }
}

