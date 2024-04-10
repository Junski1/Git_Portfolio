using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateBG : MonoBehaviour
{
    BGSpawner spawner;
    CameraDetection cam;
    [SerializeField] GameObject bottle;
    Transform spawnPoint;
    private void Start()
    {
        spawner = GetComponentInParent<BGSpawner>();
        cam = GameObject.FindGameObjectWithTag("ScriptManager").GetComponent<CameraDetection>();
        spawnPoint = transform.GetChild(1);
        spawnBottle();
    }

    private void Update()
    {
        cam.DestroyWhenOffSCreen(gameObject);
    }

    void OnDisable()
    {
        spawner.spawnBG();
    }

    void spawnBottle()
    {
        int _canSpawn = Random.Range(0, 101);
        if (_canSpawn < 10)
        {
            float _bgWidth = transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;

            float _getBgWidth = Random.Range(-(_bgWidth / 2), (_bgWidth / 2));

            GameObject _temp = Instantiate(bottle, new Vector2(spawnPoint.position.x + _getBgWidth, spawnPoint.position.y + Random.Range(0,0.35f)), Quaternion.Euler(0,0,Random.Range(0,360)), spawnPoint);
            float _scaleInt = Random.Range(0.5f, 1.5f);
            _temp.transform.localScale =new Vector3(_scaleInt, _scaleInt, 1);
        }
    }

    void OnDestroy()
    {
        Destroy(gameObject);
    }
}
