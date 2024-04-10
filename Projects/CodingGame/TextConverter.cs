using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


namespace Coder
{
    public class TextConverter : MonoBehaviour
    {

        public static readonly UnityEventString ConvertText = new UnityEventString();
        public static readonly UnityEventGameObject GetCodableObj = new UnityEventGameObject();

        [SerializeField] private float resetTimer = 5f;

        private GameObject codableObj = null;
        private Quaternion objRotation = Quaternion.identity;

        void Awake()
        {
            ConvertText.AddListener((_text) =>
            {
                SplitToLines(_text);

            });

            GetCodableObj.AddListener((_obj) => SetupCodableObjInfo(_obj));
        }

        void SplitToLines(string _text)
        {
            _text = _text.Trim('"');

            string[] _textParts = _text.Split('\n');

            foreach (string _part in _textParts)
            {
                SplitToCommands(_part.Split('.', '('));
            }
        }
        
        private void SplitToCommands(string[] _lines)
        {
            if (_lines[0] != "this")
                return;

            StopAllCoroutines();

            switch (_lines[1])
            {
                case "move":

                    if (codableObj.GetComponent<CodableObject>().CodeType == GameManager.CodeType.Rotate)
                        return;

                    CodingFuctions.MoveObj.Invoke(codableObj?.transform, ReturnVectorValues(_lines[2]));
                    StartCoroutine(ResetObjMove(codableObj, -ReturnVectorValues(_lines[2])));

                    break;

                case "rotate":

                    if (codableObj.GetComponent<CodableObject>().CodeType == GameManager.CodeType.Move)
                        return;

                    CodingFuctions.RotateObj.Invoke(codableObj?.transform, ReturnVectorValues(_lines[2]));
                    StartCoroutine(ResetObjRotate(codableObj, objRotation));

                    break;

                default:
                    return;
            }
        }

        private void SetupCodableObjInfo(GameObject _obj)
        {
            codableObj = _obj;
            objRotation = _obj.transform.rotation;
        }

        private Vector3 ReturnVectorValues(string _text)
        {
            string[] _values = _text.Split(')')[0].Split(',');
            float[] _parsedValues = new float[_values.Length];

            for (int _i = 0; _i < _values.Length; _i++)
            {
                _parsedValues[_i] = float.Parse(_values[_i]);
            }

            return new Vector3(_parsedValues[0], _parsedValues[1], _parsedValues[2]);
        }

        IEnumerator ResetObjMove(GameObject _obj, Vector3 _oldPos)
        {
            yield return new WaitForSeconds(resetTimer);
            CodingFuctions.MoveObj.Invoke(_obj.transform, _oldPos);
        }

        IEnumerator ResetObjRotate(GameObject _obj, Quaternion _rot)
        {
            yield return new WaitForSeconds(resetTimer);
            CodingFuctions.RotateObj.Invoke(_obj.transform, _rot.eulerAngles);
        }
    }
}

