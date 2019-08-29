using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using System;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject myObject;
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
            Instantiate(myObject, touch.position, Quaternion.identity);
        }

    }
}
