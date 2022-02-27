using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private AvatarAnimationControllerTest avatarAnimationController;
    [SerializeField] private ARTapToPlaceTest arTapToPlace;
    [SerializeField] private Transform avatarTransform;
    private Vector3 touchPos;

    private bool isTurnning;
    private bool isMoving;

    private void Start()
    {
        arTapToPlace.OnNewTouch.AddListener(OnTouch);
    }

    private void OnTouch()
    {
        avatarAnimationController.StopWalkAnimation();
        avatarAnimationController.StopTurnAnimation();
        touchPos = arTapToPlace.TouchPosition;


        float angle = CalculateAngle(touchPos, avatarTransform.position, avatarTransform);
        Debug.Log("<<< the angle is: " + angle + " >>>>>>");
        if (angle == 0)
        {
            isTurnning = false;
            isMoving = true;
        }
        else
        {
            isTurnning = true;
            isMoving = false;
            avatarAnimationController.StartTurnAnimation(angle);
        }
    }

    void Update()
    {
        
        if(isTurnning)
        {
            Debug.Log("<<<<<<< Vector Dot product is >>>>>>> " +            Vector3.Dot(avatarTransform.forward, (touchPos - avatarTransform.position).normalized));
            Debug.Log("<<<<<<< Projected Vector Dot product is >>>>>>> " + Vector3.Dot(avatarTransform.forward, Vector3.ProjectOnPlane(touchPos - avatarTransform.position, avatarTransform.up).normalized));

            if (Vector3.Dot(avatarTransform.forward, Vector3.ProjectOnPlane(touchPos - avatarTransform.position, avatarTransform.up).normalized) > 0.9f)
            {
                avatarAnimationController.StopTurnAnimation();

                Debug.Log("<<<<<<< Facing same direction, animation stopped >>>>>>> ");
                isMoving = true;
                isTurnning = false;
            }
        }

        if(isMoving && !avatarAnimationController.IsTurnAnimatorPlaying())
        {
            if (Vector3.Distance(avatarTransform.position, touchPos) > 0.1f)
            {
                avatarTransform.LookAt(touchPos); // to account of that 0.1 rotation that's missed
                if(!avatarAnimationController.IsMoveAnimatorPlaying())
                {
                    Debug.Log("<<<<<<< Rot has completed, Starting Move animation >>>>>>> ");
                    avatarAnimationController.StartWalkAnimation();
                }

                // it's normalized so that the velocity remains constant
                avatarTransform.position += (touchPos - avatarTransform.position).normalized * 0.3f * Time.deltaTime;
                // 0.3 is a magic number to match the speed with the animation speed
            }
            else
            {
                avatarAnimationController.StopWalkAnimation();
                isMoving = false;
            }
        }

    }

    private void OnDisable()
    {
        arTapToPlace.OnNewTouch.RemoveListener(OnTouch);

    }
    private float CalculateAngle(Vector3 targetPos, Vector3 currentPos, Transform avatarTransform)
    {
        //Vector3 projectedVector = Vector3.ProjectOnPlane(targetPos - currentPos, avatarTransform.up);
        float angle = Vector3.SignedAngle(currentPos - targetPos, avatarTransform.forward, avatarTransform.up);
        return angle;
    }
}