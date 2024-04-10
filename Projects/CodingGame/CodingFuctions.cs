using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Coder { 
    public class CodingFuctions : MonoBehaviour
    {
        public class UnityEventMoveObj : UnityEvent<Transform,Vector3> { }

        // Start is called before the first frame update
        public static readonly UnityEventMoveObj MoveObj = new UnityEventMoveObj();
        public static readonly UnityEventMoveObj RotateObj = new UnityEventMoveObj();

        [SerializeField] private float lerpTime = 3f;
        void Awake()
        {
            MoveObj.AddListener((_obj,_vector) =>
            {
                StartCoroutine(MoveLerp(_obj,_vector, lerpTime));
            });

            RotateObj.AddListener((_obj, _vector) =>
            {
                StartCoroutine(RotateLerp(_obj, Quaternion.Euler(_vector), lerpTime));
            });
        }

        private IEnumerator MoveLerp(Transform _obj, Vector3 _targetPos, float _duration)
        {
            float _timer = 0;
            Vector3 _startPos = _obj.position;
            Vector3 _endPos = _startPos + _targetPos;
            while (_timer <= _duration)
            {

                _obj.position = Vector3.Lerp(_startPos, _endPos, _timer/_duration);
                _timer += Time.deltaTime;
                yield return null;
            }

            _obj.position = _endPos;
        }

        IEnumerator RotateLerp(Transform _obj, Quaternion _endValue, float duration)
        {
            float _timer = 0;
            Quaternion _startValue = _obj.rotation;

            while (_timer < duration)
            {
                _obj.rotation = Quaternion.Lerp(_startValue, _endValue, _timer / duration);
                _timer += Time.deltaTime;
                yield return null;
            }
            _obj.rotation = _endValue;
        }
    }
}

