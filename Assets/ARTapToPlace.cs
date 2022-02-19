using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ARTapToPlace : MonoBehaviour
{
    //To use the ImportedAvatar property and get the Avatar GameObject
    [SerializeField] private AvatarImporter avatarImporter; 

    // To Start and stop the Avatar animation
    [SerializeField] private AvatarAnimationController avatarAnimationController;

    // To raycast from phone touch to tracked planes
    [SerializeField] private ARRaycastManager aRRaycastManager;

    // To get the ARCamera/ Phone's camera position in world space
    [SerializeField] private ARPoseDriver poseDriver;

    private Vector3 cameraPos;

    private Vector2 touchPosition;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    
    private void Update()
    {
        cameraPos = poseDriver.transform.position;

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

                if (avatarImporter.ImportedAvatar.activeSelf == false)
                {
                    //For the 1st time set the GameObject to true and place it at that point 
                    avatarImporter.ImportedAvatar.SetActive(true);
                    avatarImporter.ImportedAvatar.transform.position = hitPose.position;
                    avatarImporter.ImportedAvatar.transform.LookAt(cameraPos);
                }
                else
                {
                    // 1st turn towars the hit position
                    avatarImporter.ImportedAvatar.transform.LookAt(hitPose.position);
                    // start the walking animation
                    avatarAnimationController.StartAnimation();
                    // move the avatar using DOTtween. On completion,LookAtCamera fucntion is called.
                    avatarImporter.ImportedAvatar.transform.DOMove(hitPose.position, 2.95f, false).OnComplete(LookAtCamera);
                }
            }
        }   
    }

    private void LookAtCamera()
    {
        // Avatar looks at the Camera
        avatarImporter.ImportedAvatar.transform.LookAt(cameraPos);
        avatarAnimationController.StopAnimation();

    }
}