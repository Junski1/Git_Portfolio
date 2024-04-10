using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using SwipeData = SwipeDetector.SwipeData;

public class SwipeDrawer : MonoBehaviour
{
    public static readonly UnityEvent DisableLineEvent = new UnityEvent();

    [SerializeField] private int posSelector = 10;
    [SerializeField] private float fadeDuration = 0.5f;

    private LineRenderer lineRenderer;

    private float zOffset = 10;

    private int posCounter = 0;

    private bool disabled = false;



    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        DisableLineEvent.AddListener(() => 
        {
            if (disabled)
                return;

            disabled = true;
            StartCoroutine(FadeLine(lineRenderer, fadeDuration));
            GameManager.SpawnDrawerEvent.Invoke();
        });

        SwipeDetector.OnSwipe += DrawSwipeLine;
    }

    private void DrawSwipeLine(SwipeData _data)
    {
        if (disabled)
            return;

        posCounter++;

        if (posCounter % posSelector != 0 && posCounter != 1)
            return;

        lineRenderer.positionCount++;

        Vector3 _pos = Vector3.zero;

        if (lineRenderer.positionCount == 1)
            _pos = Camera.main.ScreenToWorldPoint(new Vector3(_data.StartPos.x, _data.StartPos.y, zOffset));
        else
            _pos = Camera.main.ScreenToWorldPoint(new Vector3(_data.EndPos.x, _data.EndPos.y, zOffset));

        lineRenderer.SetPosition(lineRenderer.positionCount-1, _pos);
    }

    private IEnumerator FadeLine(LineRenderer _rend, float _duration)
    {
        if (_rend == null)
            yield return null;

        float _time = 0;
        float _startValue = _rend.startColor.a;

        while(_time <= _duration)
        {
            _time += Time.deltaTime;

            float _newA = Mathf.Lerp(_startValue, 0, _time / _duration);

            _rend.startColor = new Color(_rend.startColor.r, _rend.startColor.g, _rend.startColor.b, _newA);
            _rend.endColor = new Color(_rend.endColor.r, _rend.endColor.g, _rend.endColor.b, _newA);

            yield return null;
        }

        Destroy(gameObject);
    }
}
