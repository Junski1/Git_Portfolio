using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{
    [SerializeField] Gun pistol;

    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] Camera fpsCam;
    [SerializeField] GameObject impactEffect;
    [SerializeField] GameObject bloodEffect;
    [SerializeField] GameObject bullet;
    [SerializeField] Animator animator;
    PlayerMovement movement;

    Text magazine;
    public int bulletCount;
    public bool isReloading;


    private float nextTimeToFire = 0f;
    private float hitMultiplier;

    private bool canAttack;

    void Start()
    {
        bulletCount = pistol.fullMag;
        isReloading = false;
        canAttack = true;

        magazine = GameObject.FindGameObjectWithTag("magTxt").GetComponent<Text>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        animator.SetBool("reload", false);
        StartCoroutine(animPlaying(animator.GetCurrentAnimatorStateInfo(0).length));
    }
    // Update is called once per frame
    void Update()
    {
        if (isReloading) return;
        if (bulletCount <= 0 || Input.GetKeyDown(KeyCode.R) && bulletCount != pistol.fullMag)
        {

            animator.SetBool("reload", true);
            StartCoroutine(ReloadTime(2f));
            return;
        }


        if (!movement.isGrounded)
        {
            animator.SetBool("jumping", true);
        }
        else
        {
            animator.SetBool("jumping", false);
        }

        

        if (Input.GetButtonDown("Fire1") && Time.time>= nextTimeToFire&&canAttack)
        {
            nextTimeToFire = Time.time + 1f / pistol.fireRate;
            Shoot();
        }
        magazine.text = bulletCount.ToString();
    }

    IEnumerator ReloadTime(float time)
    {
        isReloading = true;
        yield return new WaitForSeconds(time - .25f);
        
        animator.SetBool("reload", false);
        yield return new WaitForSeconds(.25f);

        bulletCount = pistol.fullMag;
        isReloading = false;
    }

    void Shoot()
    {
        bulletCount--;
        animator.SetTrigger("shoot");
        muzzleFlash.Play();
        RaycastHit _hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out _hit, pistol.range))
        {
            //Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward* pistol.range, Color.green, 2f);
            Debug.Log(_hit.collider.gameObject);
            Enemy _enemy = _hit.transform.GetComponent<Enemy>();
            if (_enemy == null) return;

            if (_hit.collider.CompareTag("Head"))
            {
                hitMultiplier = pistol.multipliers[0];
            }
            else if (_hit.collider.CompareTag("Body"))
            {
                hitMultiplier = pistol.multipliers[1];
            }
            else if (_hit.collider.CompareTag("Leg"))
            {
                hitMultiplier = pistol.multipliers[2];
            }
            pistol.DoDmg(_enemy.gameObject, hitMultiplier);
            //GameObject bloodGO = Instantiate(bloodEffect, _hit.point, Quaternion.LookRotation(_hit.normal));
            //Destroy(bloodGO, 1f);


            //GameObject impactGO = Instantiate(impactEffect, _hit.point, Quaternion.LookRotation(_hit.normal));
            //Destroy(impactGO, 1f);


            if (_hit.rigidbody != null)
            {
                _hit.rigidbody.AddForce(-_hit.normal * pistol.impactForce);
            }
        }
        GameObject bulletGO = Instantiate(bullet, transform.position, Quaternion.Euler(90,0,0));
        bulletGO.GetComponent<Rigidbody>().AddForce(new Vector3(gameObject.transform.right.magnitude, gameObject.transform.up.magnitude) *2f, ForceMode.Impulse);
        Destroy(bulletGO, 5f);
    }

    IEnumerator animPlaying(float _wait)
    {
        canAttack = false;
        yield return new WaitForSeconds(_wait);
        canAttack = true;
    }
}
