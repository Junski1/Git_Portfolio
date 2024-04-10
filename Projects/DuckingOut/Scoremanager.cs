using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoremanager : MonoBehaviour
{
    public float score;
    public float highScore;
    public Text scoreTxt;

    static float currentScore;

    public int coins;
    public Text coinTxt;

    public bool isDead;
    // Start is called before the first frame update

    private void Start()
    {
        isDead = false;
    }
    void Update()
    {
        if (!isDead)
        {
            AddScore();
        }
    }

    void AddScore()
    {
            score += Time.deltaTime;
            SetScoreText();
        
    }
    public void SetScoreText()
    {
        scoreTxt.text = "Score: " + Mathf.Round(score).ToString();
    }

    public void SetCoinText()
    {
        coinTxt.text = coins.ToString();
    }
    public void CheckForHighScore()
    {
        if(score> highScore)
        {
            highScore = score;
        }
    }

    public void SaveScore()
    {
        SaveSystem.SaveScore(this);
    }

    public void LoadScore()
    {
        GameData _data = SaveSystem.loadScore();

        highScore = _data.highScore;
        score = _data.coins;
    }
}
public class Manager
{
    float temp;
    public float IncreasePercentage(float _score, float _percent, float _incAmount, float _gap, float _max)
    {
        if (_score > temp + _gap - 1 && _percent < _max)
        {
            _percent += _incAmount;
            temp = _score;
        }
        return _percent;
    }
}
