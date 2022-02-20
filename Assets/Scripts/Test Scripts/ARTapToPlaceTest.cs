using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceTest : MonoBehaviour
{
    [SerializeField] GameObject avatarGameObject;

    // To Start and stop the Avatar animation
    [SerializeField] private AvatarAnimationControllerTest avatarAnimationController;

    // To raycast from phone touch to tracked planes
    [SerializeField] private ARRaycastManager aRRaycastManager;

    private Vector2 touchPosition;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private Vector3 newPosition;

    private void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                this.touchPosition = touch.position;
            }

            if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                if (avatarGameObject.activeSelf == false)
                {
                    avatarGameObject.SetActive(true);
                    //For the 1st time set the GameObject to true and place it at that point 
                    avatarGameObject.transform.rotation = hitPose.rotation;
                    avatarGameObject.transform.position = hitPose.position;
                }
                else
                {
                    newPosition = hitPose.position;
                    // 1st turn towars the hit position
                    avatarGameObject.transform.LookAt(newPosition);
                    // then start the walking animation
                    avatarAnimationController.StartAnimation();
                }
            }
        }

        if (Vector3.Distance(avatarGameObject.transform.position, newPosition) > 0.1f)
        {
            // the position is chnaged using the equation avatarPos += (Target - avatarPos).normalized * speed * Time
            // it's normalized so that the velocity remains constant
            avatarGameObject.transform.position += (newPosition - avatarGameObject.transform.position).normalized * 0.3f * Time.deltaTime;
            // 0.3 is a magic number to match the speed with the animation speed
        }
        else
        {
            avatarAnimationController.StopAnimation();
        }
    }  
}
        