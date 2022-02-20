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

                if (avatarImporter.ImportedAvatar.activeSelf == false)
                {
                    //For the 1st time set the GameObject to true and place it at that point
                    avatarImporter.ImportedAvatar.SetActive(true);
                    avatarImporter.ImportedAvatar.transform.position = hitPose.position;
                    avatarImporter.ImportedAvatar.transform.rotation = hitPose.rotation;


                }
                else
                {
                    newPosition = hitPose.position;

                    // 1st turn towars the hit position
                    avatarImporter.ImportedAvatar.transform.LookAt(newPosition);
                    // start the walking animation
                    avatarAnimationController.StartAnimation();
                }
            }
        }

        if (Vector3.Distance(avatarImporter.ImportedAvatar.transform.position, newPosition) > 0.1f)
        {
            // the position is chnaged using the equation avatarPos += (Target - avatarPos).normalized * speed * Time
            // it's normalized so that the velocity remains constant
            avatarImporter.ImportedAvatar.transform.position += (newPosition - avatarImporter.ImportedAvatar.transform.position).normalized * 0.3f *
                                                                      Time.deltaTime;
            // 0.3 is a magic number to match the speed with the animation speed
        }
        else
        {
            avatarAnimationController.StopAnimation();
        }
    }
}