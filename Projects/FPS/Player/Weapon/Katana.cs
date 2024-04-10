using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Katana : MonoBehaviour
{

    [SerializeField] Melee katana;
    [SerializeField] Animator animator;

    private float nextTimeToSwing = 0f;
    private int swingNmr = 0;

    public bool canDash = true;

    private float nextSwing;

    //private bool isHit;
    private bool canAttack;

    //Rigidbody rb;
    // Start is called before the first frame update

    void Awake()
    {
        //isHit = false;
        canDash = true;
        canAttack = true;
        //rb = GetComponent<Rigidbody>();
        animator = gameObject.GetComponentInParent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(animPlaying(animator.GetCurrentAnimatorStateInfo(0).length));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSwing)
        {
            nextSwing = Time.time + 1f / katana.resetRate;
            swingNmr = 0;
            animator.SetInteger("Swing", swingNmr);
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToSwing&&canAttack)
        {
            canDash = false;
            Swing();
           
            nextTimeToSwing = Time.time + 1f / katana.fireRate;
        }
    }


    public void CheckHit()
    {
        Debug.Log("Checked");

        List<Enemy> _enemiesHit = new List<Enemy>();

        Collider[] _col = Physics.OverlapBox(transform.root.position + transform.root.forward * 2, new Vector3(.75f, 1f, 1f), transform.root.rotation);

        if (_col.Length <= 0)
            return;

        Transform _parent = null;

        for (int _i = 0; _i < _col.Length; _i++)
        {

            if (_col[_i] == null)
                continue;

            Debug.Log(_col[_i].name);
            if (_col[_i].GetComponentInParent<Enemy>() == null)
                continue;

            if (_parent == _col[_i].transform.root)
                continue;

            _enemiesHit.Add(_col[_i].GetComponentInParent<Enemy>());
            _parent = _col[_i].transform.root;
        }

        foreach (Enemy _elem in _enemiesHit)
        {
            Debug.Log("Did dmg");
            katana.DoDmg(_elem.gameObject, 1f);
            katana.spawnBloodEffect(_elem.GetComponentInChildren<Collider>().ClosestPoint(transform.position), Quaternion.LookRotation((transform.position - _elem.GetComponentInChildren<Collider>().ClosestPoint(transform.position)).normalized), transform);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{

    //    if (other.gameObject != transform.root.gameObject)
    //        return;

    //    if (other.GetComponentInParent<Enemy>() == null&& other.gameObject!=transform.root.gameObject)
    //    {
    //        katana.spawnHitEffect(other.ClosestPoint(transform.position), Quaternion.LookRotation((transform.position - other.ClosestPoint(transform.position)).normalized), transform);
    //        return;
    //    }

    //    if (isHit) return;

    //    katana.DoDmg(other.gameObject, 1f);
    //    katana.spawnBloodEffect(other.ClosestPoint(transform.position), Quaternion.LookRotation((transform.position - other.ClosestPoint(transform.position)).normalized), transform);

    //    isHit = true;
    //}

    void Swing()
    {
        //isHit = false;
        swingNmr++;
        nextSwing = Time.time + 1f / 0.2f;
        if (swingNmr > 4)
        {
            swingNmr = 1;
        }

        animator.SetInteger("Swing", swingNmr);
        StartCoroutine(IECheckHit(animator.GetCurrentAnimatorStateInfo(0).length / 2));
        StartCoroutine(animPlaying(animator.GetCurrentAnimatorStateInfo(0).length));
    }

    public void Dash()
    {
        animator.SetTrigger("dodge");
        //CheckHit();
        swingNmr = 0;
        animator.SetInteger("Swing", swingNmr);
    }

    IEnumerator IECheckHit(float _wait)
    {
        yield return new WaitForSeconds(_wait);
        CheckHit();
    }

    IEnumerator animPlaying(float _wait)
    {
        canAttack = false;
        yield return new WaitForSeconds(_wait);
        canAttack = true;
        canDash = true;
    }
}
