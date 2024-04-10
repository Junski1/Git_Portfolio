using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerCheck : MonoBehaviour
{

    public Transform checkPlayer;
    [SerializeField] GameObject plane;
    public float playerDistance = 5f;
    public LayerMask portalMask;
    public GameObject textPanel;

    SpawnEnemies enemies;

    public Text finishText;
    public Text info;

    bool isNear;
    void Start()
    {
        textPanel.SetActive(false);
        finishText.enabled = false;
        enemies = GameObject.FindGameObjectWithTag("ScriptManager").GetComponent<SpawnEnemies>();
    }
    // Update is called once per frame
    void Update()
    {
        CheckForPlayer();
        if (enemies.enemyCount == 0)
        {
            plane.SetActive(true);
        }
    }

    void CheckForPlayer()
    {
        bool _isChecked =false;
        isNear = Physics.CheckBox(checkPlayer.position, new Vector3(3,7,playerDistance), Quaternion.identity,portalMask);
       
        if (isNear)
        {
            textPanel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && !_isChecked)
            {
                
                _isChecked = true;
                finishText.enabled = true;
                info.enabled = false;
            }

            if (enemies.enemyCount != 0)
            {
                finishText.text = "Kill all the enemies! REMAINING: " + enemies.enemyCount.ToString();
            }
            else if (enemies.enemyCount == 0)
            {
                finishText.text = "You've killed all the enemies. Press E to continue through the portal!";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Done");
                    //trigger animation
                }
            }
        }
        else
        {
            textPanel.SetActive(false);
        }
    }
}
