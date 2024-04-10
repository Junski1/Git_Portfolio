using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScripts : MonoBehaviour,IDamageable
{
    [Range(0,100)]
    public float HP = 100;
    Slider hpSlider;

    private void Awake()
    {
        hpSlider = GameObject.FindGameObjectWithTag("PlayerHP").GetComponent< Slider>();
    }

    void Update()
    {
        UIScripts.ChangeSliderValueEvent.Invoke(hpSlider, HP);
    }
    public void TakeDamage(float _amount)
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
}
