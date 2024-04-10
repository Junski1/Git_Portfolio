using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetection : MonoBehaviour
{
    float horizontalMin;
    void Start()
    {
        float _halfHeight = Camera.main.orthographicSize;
        float _halfWidth = Camera.main.aspect * _halfHeight;
        horizontalMin = _halfWidth*3;
    }


    public void DestroyWhenOffSCreen(GameObject _destroyable)
    {
        if (_destroyable.transform.position.x < Camera.main.transform.position.x-horizontalMin)
        {
            Destroy(_destroyable);
        }
    }

    public bool ActivateWhenOnScreen(GameObject _obj)
    {
        if(_obj.transform.position.x< Camera.main.transform.position.x + horizontalMin&& _obj.transform.position.x > Camera.main.transform.position.x - horizontalMin * 0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
