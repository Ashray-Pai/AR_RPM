using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ARTapToPlace : MonoBehaviour
{
    [SerializeField] private AvatarImporter avatarImporter;
    [SerializeField] private AvatarAnimationController avatarAnimationController;

    [SerializeField] private ARRaycastManager aRRaycastManager;
    [SerializeField] private ARPoseDriver poseDriver;

    private Vector3 cameraPos;

    private Vector2 touchPosition;

    //private bool isMoving;
    //private bool isNewPosition;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    [SerializeField] private GameObject effect;
    
    [SerializeField] private Slider slider;

    private void Update()
    {
        cameraPos = poseDriver.transform.position;
        effect.transform.localScale = Vector3.one * slider.value;
        Debug.Log("Slider value" + slider.value);

        //avatarImporter.ImportedAvatar.transform.LookAt(poseDriver.transform.position);

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
                    this.avatarImporter.ImportedAvatar.SetActive(true);
                    avatarImporter.ImportedAvatar.transform.position = hitPose.position;
                    avatarImporter.ImportedAvatar.transform.LookAt(cameraPos);
                }
                else
                {
                    Vector3 lookDirection = avatarImporter.ImportedAvatar.transform.position - hitPose.position;
                    avatarImporter.ImportedAvatar.transform.DORotate(lookDirection, 0.1f,RotateMode.Fast);
                    avatarAnimationController.StartAnimation();
                    avatarImporter.ImportedAvatar.transform.DOMove(hitPose.position, 10*slider.value, false).OnComplete(LookAtCamera);
                }
            }
        }
        
    }

    private void LookAtCamera()
    {
        avatarImporter.ImportedAvatar.transform.LookAt(cameraPos);
        Debug.Log("Look at cam!");
        avatarAnimationController.StopAnimation();

    }
}