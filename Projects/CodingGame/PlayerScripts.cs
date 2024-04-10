using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerScripts : MonoBehaviour
{
    [Range(0,100)]
    public float HP = 100;
    //Slider hpSlider;
    PlayerMovement movementScript;
    PlayerLook lookScript;

    public static readonly UnityEventFloat TakeDmgEvent = new UnityEventFloat();
    public static readonly UnityEvent DisableMovement = new UnityEvent();
    public static readonly UnityEvent EnableMovement = new UnityEvent();

    private void Awake()
    {
        //hpSlider = GameObject.FindGameObjectWithTag("PlayerHP").GetComponent< Slider>();
        movementScript = GetComponent<PlayerMovement>();
        lookScript = GetComponent<PlayerLook>();
    }

    void Update()
    {
        //UIScripts.ChangeSliderValueEvent.Invoke(hpSlider, HP);
        TakeDmgEvent.AddListener((_amount) => {
            TakeDamage(_amount);
        });
        DisableMovement.AddListener(() => {
            ToggleMovement(false);
        });
        EnableMovement.AddListener(() => {
            ToggleMovement(true);
        });
    }
    private void TakeDamage(float _amount)
    {
        HP -= _amount;
    }

    public void Die()
    {
        if (HP < 1)
        {
            Destroy(gameObject);
        }
    }

    private void ToggleMovement(bool _toggle)
    {
        movementScript.enabled = _toggle;
        lookScript.enabled = _toggle;
    }
}
