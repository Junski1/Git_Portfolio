using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleScripts : MonoBehaviour
{
    CameraDetection cam;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("ScriptManager").GetComponent<CameraDetection>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.collider.CompareTag("player"))
        {
            Debug.Log("Kuolit lol");
        }
    }
}
