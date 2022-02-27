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

    public void StartWalkAnimation()
    {
        Debug.Log("<<<<<<< Walking Started>>>>>>> ");
        this.animator.SetBool("isMoving", true);
    }

    public void StopWalkAnimation()
    {
        Debug.Log("<<<<<<< Walking Stopped>>>>>>> ");
        this.animator.SetBool("isMoving", false);
    }

    public void StartTurnAnimation(float angle)
    {     
        if( angle >0)
        {
            Debug.Log("<<<<<<< Right Rotation Started>>>>>>> ");
            this.animator.SetBool("isTurningRight", true);
        }
        else
        {
            Debug.Log("<<<<<<< Left Rotation Started>>>>>>> ");
            this.animator.SetBool("isTurningLeft", true);
        }     
    }

    public void StopTurnAnimation()
    {     
        Debug.Log("<<<<<<< Left Rotation Stopped >>>>>>> ");
        this.animator.SetBool("isTurningLeft", false);
        Debug.Log("<<<<<<< Right Rotation Stopped>>>>>>> ");
        this.animator.SetBool("isTurningRight", false);
    }

    public bool IsTurnAnimatorPlaying()
    {      
        return animator.GetCurrentAnimatorStateInfo(0).IsName("RightTurn") || animator.GetCurrentAnimatorStateInfo(0).IsName("LeftTurn") ;     
    }

    public bool IsMoveAnimatorPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Walking");
    }
}

