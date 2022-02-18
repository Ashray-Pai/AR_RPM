using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlace2 : MonoBehaviour
{
    [SerializeField] private AvatarImporter avatarImporter;
    [SerializeField] private ARRaycastManager aRRaycastManager;
    [SerializeField] 
    private Vector2 touchPosition;
    private Vector3 newTargetPosition = Vector3.zero;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

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
                    this.avatarImporter.ImportedAvatar.SetActive(true);
                }
                else
                {
                    
                    avatarImporter.ImportedAvatar.transform.position = hitPose.position;
                }
            }
        }

    }

}

