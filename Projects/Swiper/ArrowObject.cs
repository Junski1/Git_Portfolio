using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwipeDirection = SwipeDetector.SwipeDirection;

public class ArrowObject : MonoBehaviour
{
    private float[] angles = { 0, 90, 180, 270 };
    private int curAngle = 0;

    private SwipeDirection swipeDir;
    public SwipeDirection SwipeDir => swipeDir;
    // Start is called before the first frame update
    void Awake()
    {
        SetAngle();
    }

    private void SetAngle()
    {
        curAngle = Random.Range(0, angles.Length);
        transform.Rotate(0,0,angles[curAngle],Space.World);
        swipeDir = (SwipeDirection)curAngle;
    }
}
