using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScripts : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject pausePanel;

    [SerializeField] Text scoreTxt;



    public void Death()
    {
        Scoremanager _scoreManager = GetComponent<Scoremanager>();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().gravityScale = 0;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        _scoreManager.isDead = true;
        _scoreManager.CheckForHighScore();
        deathPanel.SetActive(true);
    }

    public void DisableFlight()
    {
        Scoremanager _scoreManager = GetComponent<Scoremanager>();
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        _player.GetComponent<PlayerMovement>().enabled = false;
        _player.GetComponent<Rigidbody2D>().gravityScale = 1;
        //_scoreManager.isDead = true;
        //_scoreManager.CheckForHighScore();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        scoreTxt.text = GetComponent<Scoremanager>().scoreTxt.text;
        pausePanel.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
