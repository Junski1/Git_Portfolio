using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleScript : MonoBehaviour
{
    Animator anim;
    Scoremanager scoreManager;

    EffectScripts effects;

    [SerializeField]GameObject splashEffect;

    CameraDetection cam;

    bool inCamera;

    float jumpPercentage;

    float score;

    Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        jumpPercentage = 50;
        scoreManager = GameObject.FindGameObjectWithTag("ScriptManager").GetComponent<Scoremanager>();
        effects = GameObject.FindGameObjectWithTag("ScriptManager").GetComponent<EffectScripts>();
        cam = GameObject.FindGameObjectWithTag("ScriptManager").GetComponent<CameraDetection>();
        manager = new Manager();
        anim = GetComponent<Animator>();
        StartCoroutine(WhenToJump());
    }

    void Update()
    {
        inCamera = cam.ActivateWhenOnScreen(gameObject);
        score = scoreManager.score;
        jumpPercentage = manager.IncreasePercentage(score,jumpPercentage, 5f, 25f,100f);
        
    }

    void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            Debug.Log("scorea lisättiin");
            scoreManager.coins++;
            scoreManager.SetCoinText();
            Destroy(gameObject);
        }
    }


    void Jump()
    {
        int _randomAnim = Random.Range(1, 2);
        anim.SetInteger("jumpHeight", _randomAnim);
        effects.SpawnEffect(splashEffect, transform.position, Quaternion.identity, 2f);
    }
    IEnumerator WhenToJump()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(Random.Range(0f, 0.51f));
            anim.SetInteger("jumpHeight", 0);
            if (inCamera)
            {
               int _jump = Random.Range(0, 101);
                if (_jump < jumpPercentage + 1)
                {
                    Jump();
                    while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
                    {
                        yield return null;
                    }
                }
                else
                {
                    anim.SetInteger("jumpHeight", 0);
                }
            }
        }
    }
}
