using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    Animator anim;

    MainMenuScripts menuScipts;
    EffectScripts effects;
    Scoremanager scoreManager;
    

    [SerializeField] GameObject splashEffect;

    CameraDetection cam;

    bool inCamera;

    float jumpPercentage;

    float score;

    Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindWithTag("ScriptManager").GetComponent<CameraDetection>();
        effects = GameObject.FindGameObjectWithTag("ScriptManager").GetComponent<EffectScripts>();
        menuScipts = GameObject.FindGameObjectWithTag("ScriptManager").GetComponent<MainMenuScripts>();
        scoreManager = GameObject.FindGameObjectWithTag("ScriptManager").GetComponent<Scoremanager>();

        manager = new Manager();

        anim = GetComponent<Animator>();
        jumpPercentage = 15f;
        StartCoroutine(WhenToJump());
    }

    void Update()
    {
        inCamera = cam.ActivateWhenOnScreen(gameObject);
        score = scoreManager.score;
        jumpPercentage = manager.IncreasePercentage(score, jumpPercentage, 5f, 15f,100f);
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.collider.CompareTag("Player"))
        {
            menuScipts.DisableFlight();
            Debug.Log("Kuolit lol");
        }
    }


    void Jump()
    {
        int _randomAnim = Random.Range(1, 4);//vika animaatioden m‰‰r‰ +1
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
                if (_jump < jumpPercentage+1)
                {
                    Jump();
                    while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
                    {
                        yield return null;
                    }
                  
                    effects.SpawnEffect(splashEffect, transform.position, Quaternion.identity, 2f);
                }
                else
                {
                    anim.SetInteger("jumpHeight", 0);
                }
            }
        }
    }
}
