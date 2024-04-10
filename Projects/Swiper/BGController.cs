using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SwipeDirection = SwipeDetector.SwipeDirection;

public class BGController : MonoBehaviour
{
    public class UnityEventBG : UnityEvent<SwipeDirection> { }


    public static readonly UnityEventBG MoveBGEvent = new UnityEventBG();


    [SerializeField] private Transform BGPatternObj = null;

    [SerializeField] private float moveAmount = 0f;


    private void Awake()
    {
        MoveBGEvent.AddListener((_dir) => MoveBg(_dir));
    }

    private void MoveBg(SwipeDirection _dir)
    {
        Vector3 _moveV = Vector3.zero;

        switch (_dir)
        {
            case SwipeDirection.Up:
                _moveV.y = moveAmount;
                break;
            case SwipeDirection.Down:
                _moveV.y = -moveAmount;
                break;
            case SwipeDirection.Left:
                _moveV.x = -moveAmount;
                break;
            case SwipeDirection.Right:
                _moveV.x = moveAmount;
                break;
        }

        StartCoroutine(LerpBG(BGPatternObj, _moveV, 0.1f));
    }
    private IEnumerator LerpBG(Transform _obj, Vector3 _moveV, float _duration)
    {

        if (_obj == null)
            yield return null;
        
        float _time = 0;
        Vector3 _startValue = _obj.position;
        Vector3 _endValue = _obj.position + _moveV;

        Debug.Log($"pos: {_startValue}\nuus: {_endValue}\nmoveV: {_moveV}");

        while (_time <= _duration)
        {
            _time += Time.deltaTime;
            _obj.position = Vector3.Lerp(_startValue, _endValue, _time / _duration);

            yield return null;
        }
    }
}
