using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New melee", menuName = "Weapons/Melee")]
public class Melee : Weapon
{
    [SerializeField] GameObject impactEffect;
    [SerializeField] GameObject bloodEffect;

    public float dmg;
    public float fireRate;
    public float resetRate;
    public float impactForce;

    public float[] multipliers;
    public override void DoDmg(GameObject _target, float _amount)
    {
        _target.GetComponent<IDamageable>().TakeDamage(dmg * _amount);
    }

    public void spawnBloodEffect(Vector3 _pos, Quaternion _rot, Transform _parent)
    {
        GameObject impactGO = Instantiate(bloodEffect, _pos, _rot, _parent);
        Destroy(impactGO, 1f);
    }

    public void spawnHitEffect(Vector3 _pos, Quaternion _rot, Transform _parent)
    {
        GameObject impactGO = Instantiate(impactEffect, _pos, _rot, _parent);
        Destroy(impactGO, 1f);
    }
}
