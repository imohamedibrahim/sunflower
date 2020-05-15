using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class TouchInputManager : MonoBehaviour
{
    public static TouchInputManager touchInstance;
    public event EventHandler<InfoOnTouch> OnTouchEvent;
    private int fingerId;
    private Vector2 initialTouchPosition;
    private float distanceSwiped;
    private string directionSwiped;
    void Start()
    {
        fingerId = 0;
        initialTouchPosition = Vector2.zero;
    }

    public static TouchInputManager Instance()
    {
        if (touchInstance == null)
        {
            touchInstance = new TouchInputManager();
        }
        return touchInstance;
    }

    private void Awake()
    {
        if (touchInstance != null && touchInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            touchInstance = this;
        }
    }

    void LateUpdate()
    {
        foreach (Touch touch in Input.touches)
        {
            if (EventSystem.current.IsPointerOverGameObject(fingerId))
                return;
            if (touch.phase == TouchPhase.Began)
            {
                fingerId = touch.fingerId;
                initialTouchPosition = touch.position;
            }
            else if (false && touch.phase == TouchPhase.Moved)
            {
               // ComputeSwipeDirection(initialTouchPosition,touch.position);
                initialTouchPosition = touch.position;
            }
            else if(fingerId == touch.fingerId && touch.phase == TouchPhase.Ended)
            {
                ComputeSwipeDirection(initialTouchPosition, touch.position);
            }
        }
    }

    void ComputeSwipeDirection(Vector2 initialPosition, Vector2 currentPosition)
    {
        InfoOnTouch infoOnTouch = new InfoOnTouch();
        Vector2 helperVector = currentPosition - initialPosition;
        if(Mathf.Abs(helperVector.x) > Mathf.Abs(helperVector.y))
        {
            if(Math.Sign(helperVector.x) == -1)
            {
                infoOnTouch.Swipe = SwipeDirection.LEFT;
            }
            else
            {
                infoOnTouch.Swipe = SwipeDirection.RIGHT;
            }
            infoOnTouch.DistanceSwiped = helperVector.x;
        }
        else
        {
            if(Math.Sign(helperVector.y) == -1)
            {
                infoOnTouch.Swipe = SwipeDirection.DOWN;
            }
            else
            {
                infoOnTouch.Swipe = SwipeDirection.UP;
            }
            infoOnTouch.DistanceSwiped = helperVector.y;
        }
        OnTouchEvent?.Invoke(this,infoOnTouch);
    }


    public class InfoOnTouch : EventArgs
    {
        public SwipeDirection Swipe = SwipeDirection.NONE;
        public float DistanceSwiped;
    }

    public enum SwipeDirection
    {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
}