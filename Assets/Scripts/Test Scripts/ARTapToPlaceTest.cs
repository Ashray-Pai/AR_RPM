using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceTest : MonoBehaviour
{
    [SerializeField] GameObject avatarGameObject;
    [SerializeField] Camera arCam;

    [SerializeField] private ARRaycastManager aRRaycastManager;

    [SerializeField] private ARPlaneDetection arPlaneDetection;

    private Vector2 touchPosition;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private Vector3 newPosition= Vector3.zero;
    public Vector3 TouchPosition { get => newPosition; }

    public UnityEvent OnNewTouch;


    //private void Update()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);

    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            this.touchPosition = touch.position;
    //        }

    //        if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon) && arPlaneDetection.IsFloorPlaced)
    //        {
    //            Pose hitPose = hits[0].pose;
    //            if (avatarGameObject.activeSelf == false)
    //            {
    //                //Debug.Log("<<<<<< Avatar activated>>>>>");
    //                avatarGameObject.SetActive(true);
    //                //For the 1st time set the GameObject to true and place it at that point 
    //                avatarGameObject.transform.rotation = hitPose.rotation;
    //                avatarGameObject.transform.position = hitPose.position;
    //            }
    //            else
    //            {
    //                newPosition = hitPose.position;
    //                //Debug.Log("<<<<<< Event Invoked>>>>>");
    //                OnNewTouch.Invoke();
    //            }
    //        }
    //    }
    //}
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                this.touchPosition = touch.position;
                Ray ray = arCam.ScreenPointToRay(touch.position);
                RaycastHit hitAnything;
                if(Physics.Raycast(ray, out hitAnything,Mathf.Infinity))
                {
                    Debug.Log("<<<<<< >>>>>" + hitAnything.transform.gameObject.tag);
                    if(hitAnything.transform.gameObject.CompareTag("Floor"))
                    {
                        Debug.Log("<<<<<< Has hit floor >>>>>");
                        if (avatarGameObject.activeSelf == false)
                        {
                            //Debug.Log("<<<<<< Avatar activated>>>>>");
                            avatarGameObject.SetActive(true);
                            //For the 1st time set the GameObject to true and place it at that point 
                            avatarGameObject.transform.rotation = hitAnything.transform.rotation;
                            avatarGameObject.transform.position = hitAnything.point;
                        }
                        else
                        {
                            newPosition = hitAnything.point;
                            //Debug.Log("<<<<<< Event Invoked>>>>>");
                            OnNewTouch.Invoke();
                        }
                    }
                    


                }
            }

            //if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon) && arPlaneDetection.IsFloorPlaced)
            //{
            //    Pose hitPose = hits[0].pose;
            //    if (avatarGameObject.activeSelf == false)
            //    {
            //        //Debug.Log("<<<<<< Avatar activated>>>>>");
            //        avatarGameObject.SetActive(true);
            //        //For the 1st time set the GameObject to true and place it at that point 
            //        avatarGameObject.transform.rotation = hitPose.rotation;
            //        avatarGameObject.transform.position = hitPose.position;
            //    }
            //    else
            //    {
            //        newPosition = hitPose.position;
            //        //Debug.Log("<<<<<< Event Invoked>>>>>");
            //        OnNewTouch.Invoke();
            //    }
            //}
        }
    }
}
        