using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int coins;
    public int highScore;

    public GameData(Scoremanager _scoreManager)
    {
        coins = (int)Mathf.Round(_scoreManager.highScore);
        highScore = _scoreManager.coins;
    }
}
