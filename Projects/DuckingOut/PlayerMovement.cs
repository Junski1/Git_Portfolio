using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float speed;
    float pastTime;

    float verticalInput;

    Quaternion dropRotate  = Quaternion.Euler(0, 0, -10);

    Quaternion flyDownRotate = Quaternion.Euler(0, 0, -20);
    Quaternion flyUpRotate = Quaternion.identity;


    public Animator anim;

    bool hitBorder;

    float borderTimer;
    float maxTimer = 0.2f;

    Scoremanager scoreManager;

    float score;

    Manager speedManager;
    Manager timerManager;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        anim.SetBool("liito", false);
        scoreManager = GameObject.FindGameObjectWithTag("ScriptManager").GetComponent<Scoremanager>();
        speedManager = new Manager();
        timerManager = new Manager();
    }
    Vector3 moveDirection;
    void Move()
    {
        moveDirection = new Vector2(0, verticalInput) + Vector2.right;
        transform.Translate(moveDirection* speed * Time.deltaTime,Space.World);
    }

    void MovePC()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void MoveMobile()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("Kosketusta");
            Touch _touch = Input.GetTouch(0);
            if(_touch.position.x < Screen.width/2)
            {
                verticalInput = 1;
            }
            else if(_touch.position.x > Screen.width / 2)
            {
                verticalInput = -1;
            }
        }
        else
        {
            verticalInput = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("BG"))
        {
            borderTimer = maxTimer;
            hitBorder = true;
            verticalInput = -1;
        }
    }

    void OnHitBorder()
    {
        if (hitBorder)
        {
            borderTimer -= Time.deltaTime;
        }
        if (borderTimer < 0f)
        {
            hitBorder = false;
            borderTimer = maxTimer;
        }
    }

    void DropPLayer()
    {
        float _percent = pastTime / speed;
        if(verticalInput == 0)
        {
            anim.SetBool("liito",true);
            //if (dropDownTimer < 0f)
            //{
            verticalInput = -0.5f;
            moveDirection *= (Mathf.Sqrt(5) / 2);
            transform.rotation = Quaternion.Lerp(transform.rotation,dropRotate, _percent);
            //}
            //else
            //{
                //transform.rotation = Quaternion.Lerp(transform.rotation, flyUpRotate, Time.deltaTime * 20f);
            //}
        }
        else if(verticalInput > 0)
        {
            anim.SetBool("liito", false);
            moveDirection *= Mathf.Sqrt(2);
            transform.rotation = Quaternion.Lerp(transform.rotation, flyUpRotate, _percent);
        }else if (verticalInput < 0)
        {
            anim.SetBool("liito", true);
            verticalInput = -2;
            moveDirection *= Mathf.Sqrt(5);
            transform.rotation = Quaternion.Lerp(transform.rotation, flyDownRotate, _percent);
        }
    }


    // Update is called once per frame
    void Update()
    {
        pastTime += Time.deltaTime;
        score = scoreManager.score;

        speed = speedManager.IncreasePercentage(score,speed, 0.1f, 10f, 100f);
        maxTimer = timerManager.IncreasePercentage(score, maxTimer, 0.05f, 10f, 1f);

        DropPLayer();
        OnHitBorder();
        Move();

        Debug.Log(moveDirection * Mathf.Sqrt(2) + ": Ylös");
        Debug.Log(moveDirection * (Mathf.Sqrt(5)/2) + ": none");
        Debug.Log(moveDirection* Mathf.Sqrt(5) + ": Alas");
        //Debug.Log(SystemInfo.deviceType);
        if (!hitBorder)
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                MovePC();
            }
            else if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                MoveMobile();
            }
        }
    }
}
