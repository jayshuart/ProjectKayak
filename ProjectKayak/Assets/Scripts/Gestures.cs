using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gestures : MonoBehaviour
{
    private Vector2 inputStart;
    private Vector2 inputEnd;
    private bool gestureStarted;
    private const float MIN_SWIPE_LENGTH = 100;

    //screen zone properties
    private float SCREEN_CENTER_X = Screen.width / 2;
    private float SCREEN_CENTER_Y = Screen.height / 2;
    private float SCREEN_LEFT_SIDE = Screen.width / 4;
    private float SCREEN_RIGHT_SIDE = Screen.width - (Screen.width / 4);

    //swipe gesture delgates
    public UnityEvent event_SwipeLeft;
    public UnityEvent event_SwipeRight;
    public UnityEvent event_SwipeUp;
    public UnityEvent event_SwipeDown;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("[Gestures] -- Initalized");
        inputStart = Vector2.zero;
        inputEnd = Vector2.zero;
        gestureStarted = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && !gestureStarted)
        {
            StartGesture();
        }
        else if((Input.touchCount <= 0 && !Input.GetMouseButton(0)) && gestureStarted)
        {
            EndGesture();
        }
        else if((Input.touchCount > 0 || Input.GetMouseButton(0)) && gestureStarted)
        {
            UpdateGesture();
        }
    }

    private void StartGesture()
    {
        gestureStarted = true;

        //get inital gesture position
    #if UNITY_EDITOR
        inputStart = Input.mousePosition;
    #else
        inputStart = Input.GetTouch(0).position;
    #endif

        Debug.Log("[Gestures] -- Started: " + inputStart);
    }

    private void UpdateGesture()
    {
        #if UNITY_EDITOR
            inputEnd = Input.mousePosition;
        #else
            inputEnd = Input.GetTouch(0).position;
        #endif
    }

    private void EndGesture()
    {
        gestureStarted = false;

        //get vector between start and here
        Vector2 diff = inputEnd - inputStart;

        Debug.Log("[Gestures] -- Ended: " + inputEnd);
        Debug.Log(diff.magnitude);

        if(diff.magnitude >= MIN_SWIPE_LENGTH)
        { CalculateGesture(diff); }
    }

    private void CalculateGesture(Vector2 gestureLine)
    {
        //wittle down possible gestures based on start pos
        if(inputStart.x <= SCREEN_LEFT_SIDE) //left side start
        {
            if(inputEnd.x >= SCREEN_RIGHT_SIDE)
            {
                event_SwipeRight.Invoke();
            }

        }
        else if(inputStart.x >= SCREEN_RIGHT_SIDE) //right side start
        {
            if(inputEnd.x <= SCREEN_LEFT_SIDE)
            {
                event_SwipeLeft.Invoke();
            }
        }
        else //middle start
        {
            if(inputStart.y <= SCREEN_CENTER_Y && inputEnd.y >= SCREEN_CENTER_Y)
            {
                event_SwipeUp.Invoke();
            }
            else if(inputStart.y >= SCREEN_CENTER_Y && inputEnd.y <= SCREEN_CENTER_Y)
            {
                event_SwipeDown.Invoke();
            }
        }
    }
}
