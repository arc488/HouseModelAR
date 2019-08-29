using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using System;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject myObject;
    public Camera firstPersonCamera;
    public DetectedPlane detectedPlane;

    void Start()
    {
        
    }


    void Update()
    {
        if (Session.Status != SessionStatus.Tracking)
        {
            Screen.sleepTimeout = 15;
            return;
        }
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        ProcessTouch();
    }

    private void ProcessTouch()
    {
        Touch touch;
        
        if (Input.touchCount < 1) return;

        touch = Input.GetTouch(0);

        if (Input.touchCount != 1 || (touch.phase != TouchPhase.Began)) return;

        TrackableHit hit;
        TrackableHitFlags hitFilter = TrackableHitFlags.PlaneWithinBounds | TrackableHitFlags.PlaneWithinPolygon;
        if (Frame.Raycast(touch.position.x, touch.position.y, hitFilter, out hit))
        {
            detectedPlane = hit.Trackable as DetectedPlane;
            PlaceHouse(detectedPlane, hit);
        }

    }

    private void PlaceHouse(DetectedPlane plane, TrackableHit hit)
    {
        if (plane == null) return;

        var hitPos = hit.Pose;

        Anchor anchor = plane.CreateAnchor(hitPos);

        var objectInstance = Instantiate(myObject, hitPos.position, Quaternion.identity, anchor.transform);
    }
}
