using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SwipeData = SwipeDetector.SwipeData;

public class GameManager : MonoBehaviour
{
    public static readonly UnityEvent StartGameEvent = new UnityEvent();
    public static readonly UnityEvent SpawnDrawerEvent = new UnityEvent();

    [SerializeField] private ArrowObject curArrow = null;
    [SerializeField] private GameObject arrowPrefab = null;
    [SerializeField] private GameObject drawer = null;



    [SerializeField] private float maxTimer = 5f;

    private float timer = 0f;
    public float Timer => timer;

    [Range(1f, 10f)]
    private float timerMultiplier = 1f;


    private int score = 0;

    private bool gameRunning = false;

    void Awake()
    {
        StartGameEvent.AddListener(() => 
        {
            gameRunning = true;
            score = 0;
            SpawnArrow();
            SpawnDrawerEvent.Invoke();
            SwipeDetector.OnSwipe += CheckCorSwipe;
        });

        SpawnDrawerEvent.AddListener(() => Instantiate(drawer));
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameRunning)
            return;

        RunTimer();
    }

    private void RunTimer() 
    { 
        timer -= Time.deltaTime * timerMultiplier;

        UIScripts.ChangeSliderEvent.Invoke(timer);

        if(timer < 0)
            EndGame();
    }

    private void ResetTimer() => timer = maxTimer;

    private void CheckCorSwipe(SwipeData _data)
    {
        if (_data.Direction != curArrow.SwipeDir)
            return;

        score++;

        if(score % 2 == 0)
        {
            timerMultiplier = ChangeFloat(timerMultiplier, 0.5f);
        }

        UIScripts.ChangeTextEvent.Invoke(score);
        SwipeDrawer.DisableLineEvent.Invoke();
        BGController.MoveBGEvent.Invoke(_data.Direction);

        SpawnArrow();
    }

    private void SpawnArrow()
    {
        if(curArrow != null)
            Destroy(curArrow.gameObject);

        ResetTimer();
        curArrow = Instantiate(arrowPrefab).GetComponent<ArrowObject>();
    }

    private void EndGame()
    {
        gameRunning = false;
    }

    private float ChangeFloat(float _value, float _changeValue)
    {
        return _value + _changeValue;
    }
}
