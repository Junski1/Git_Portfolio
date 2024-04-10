using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New gun", menuName ="Weapons/Gun")]
public class Gun : Weapon
{
    public float dmg;
    public float range;
    public float fireRate;
    public float impactForce;

    public int fullMag;

    public float[] multipliers;

    public override void DoDmg(GameObject _target, float _amount)
    {
        _target.GetComponent<IDamageable>().TakeDamage(dmg*_amount);
    }
}
