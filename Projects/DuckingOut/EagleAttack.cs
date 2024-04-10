using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleAttack : MonoBehaviour
{
    public float speed = 1f;
    Vector3 currentPos;
    Vector3 centerPoint;

    Vector3 centerPointRot;

    Vector3 currentPos_Rot;
    Vector3 lastPos_Rot;

    Vector3 directionToTarget;
    float distanceToTarget;
    Vector3 targetPosition;

    Vector3 currentRelCenter, targetRelCenter;

    Quaternion currentRot;

    float pastTime;

    [SerializeField]
    AnimationCurve curve;


    public float radius;
    [Range(0, 360)]
    public float angle;

    public LayerMask targetMask;

    bool canSeePlayer;

    public GameObject flyTrail;
    public GameObject hitEffect;

    bool search;

    MainMenuScripts menuScipts;

    void Start()
    {
        currentPos_Rot = transform.position;
        currentPos = transform.position;
        search = true;
        menuScipts = GameObject.FindGameObjectWithTag("ScriptManager").GetComponent<MainMenuScripts>();
        StartCoroutine(searchPlayer());
    }
    // Update is called once per frame
    void Update()
    {
        if (canSeePlayer)
        {
            StartCoroutine(FlyTowards());
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            //kuolit
            menuScipts.Death();
            Destroy(gameObject);
            Debug.Log("Kuolit lol");
        }
    }

    void FieldOfViewCheck()
    {
        Collider2D[] _rangeChecks = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);
        if (_rangeChecks.Length != 0)
        {
            Transform _target = _rangeChecks[0].transform;
            directionToTarget = (_target.position - transform.position).normalized;

            if (Vector3.Angle(-transform.right, directionToTarget) < angle / 2)
            {
                distanceToTarget = Vector3.Distance(transform.position, _target.position);

                if (Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, targetMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
        targetPosition = transform.position + directionToTarget * distanceToTarget;
    }

    void GetCenter(Vector3 _dir)
    {
        centerPoint = new Vector3(targetPosition.x, currentPos.y);
        centerPoint -= _dir;
        currentRelCenter = currentPos - centerPoint;
        targetRelCenter = targetPosition - centerPoint;
    }

    IEnumerator FlyTowards()
    {
        yield return new WaitForSeconds(0.1f);

        search = false;

        pastTime += Time.deltaTime;

        float _percent = pastTime / speed;
        float _lastPercent = 0;
        GetCenter(-directionToTarget*5);

        if (_percent > _lastPercent)
        {
            lastPos_Rot = currentPos_Rot;
            currentPos_Rot = transform.position;
        }

        Vector3 _rotDirection = lastPos_Rot - currentPos_Rot;

        if (currentPos.y >= targetPosition.y)
        {
            
            transform.position = Vector3.Slerp(currentRelCenter, targetRelCenter, curve.Evaluate(_percent));
            transform.position += centerPoint;
            Quaternion _rot;
            if (_rotDirection == Vector3.zero)
            {
                _rot = transform.rotation;
            }
            else
            {
                _rot = Quaternion.LookRotation(_rotDirection);
            }

            
            _rot.x = 0;
            _rot.y = 0;
            if (_percent > 0.2f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, _rot, _percent * 5);
            }
            _lastPercent = _percent;
            if(_percent>= 100)
            {
                transform.Translate(_rotDirection * speed);
            }
        }
        else
        {
            Quaternion _targetRot = Quaternion.LookRotation(directionToTarget);
            _targetRot.x = 0;
            _targetRot.y = 0;

            transform.rotation = Quaternion.Lerp(currentRot, _targetRot, _percent*5);
            transform.position = Vector3.Lerp(currentPos, targetPosition, curve.Evaluate(_percent));
        }


        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator searchPlayer()
    {
        WaitForSeconds _wait = new WaitForSeconds(0.2f);

        while (search)
        {
            yield return _wait;
            FieldOfViewCheck();
        }
    }
}
