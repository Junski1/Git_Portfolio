using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    public enum SwipeDirection
    {
        Up, Left, Down, Right
    }

    public struct SwipeData
    {
        public Vector2 StartPos;
        public Vector2 EndPos;
        public SwipeDirection Direction;
    }

    public static event Action<SwipeData> OnSwipe  = delegate{ };

    [SerializeField] private float minSwipeDist = 20f;

    private Vector2 fingerDownPos = new Vector2();
    private Vector2 fingerUpPos = new Vector2();


     // Update is called once per frame
     void Update()
    {
        foreach (Touch _touch in Input.touches)
        {
            if(_touch.phase == TouchPhase.Began)
            {
                fingerDownPos = _touch.position;
                fingerUpPos = _touch.position;
            }

            if(_touch.phase == TouchPhase.Moved)
            {
                fingerUpPos = _touch.position;
                DetectSwipe();
            }

            if(_touch.phase == TouchPhase.Ended)
            {
                SwipeDrawer.DisableLineEvent.Invoke();
            }
        }
    }

    private void DetectSwipe()
    {
        if(!CheckSwipeDistance())
            return;

        SwipeDirection _dir;
        if (IsVertical())
            _dir = fingerDownPos.y - fingerUpPos.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
        else
            _dir = fingerDownPos.x - fingerUpPos.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;

        SendSwipe(_dir);
    }

    private void SendSwipe(SwipeDirection _dir)
    {
        SwipeData _data = new SwipeData()
        {
            StartPos = fingerDownPos,
            EndPos = fingerUpPos,
            Direction = _dir
        };

        OnSwipe(_data);
    }

    private bool CheckSwipeDistance()
    {
        return VerticalMovementDistance() > minSwipeDist || HorizontalMovementDistance() > minSwipeDist;
    }

    private bool IsVertical()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPos.x - fingerUpPos.x);
    }
}
