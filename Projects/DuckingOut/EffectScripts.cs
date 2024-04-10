using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScripts : MonoBehaviour
{
    public void SpawnEffect(GameObject _obj, Vector3 _pos,Quaternion _rot, float _timeToDestroy)
    {
        GameObject _temp = Instantiate(_obj, _pos, _rot);
        Destroy(_temp, _timeToDestroy);
    }
}
