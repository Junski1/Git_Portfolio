using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript1 : MonoBehaviour
{

    public Transform[] fishPoints;

    public GameObject[] fishPrefabs;

    public GameObject[] edibles;

    public Transform[] EagleSpawns;

    public GameObject EaglePrefab;

    GroundSpawner groundSpawner;
    EffectScripts effects;
    MainMenuScripts menuScipts;
    Scoremanager scoreManager;

    float spawnPercentage;

    Manager manager;

    [SerializeField]GameObject splashEffect;

    CameraDetection cam;

    float score;

    bool eagleSpawned;
    // Start is called before the first frame update
    void Start()
    {
        spawnPercentage = 5;
        eagleSpawned = false;
        groundSpawner = GetComponentInParent<GroundSpawner>();
        effects = GameObject.FindGameObjectWithTag("ScriptManager").GetComponent<EffectScripts>();
        cam = GameObject.FindGameObjectWithTag("ScriptManager").GetComponent<CameraDetection>();
        menuScipts = GameObject.FindGameObjectWithTag("ScriptManager").GetComponent<MainMenuScripts>();
        scoreManager = GameObject.FindGameObjectWithTag("ScriptManager").GetComponent<Scoremanager>();

        manager = new Manager();

        if (scoreManager.score > 3)
        {
            for (int _i = 0; _i < fishPoints.Length; _i++)
            {
                SpawnFish(_i);
            }

            for (int _i = 0; _i < EagleSpawns.Length; _i++)
            {

                if (!eagleSpawned)
                {
                    SpawnEagles(_i);
                }
                else
                {
                    break;
                }
            }
        } 
    }

    void Update()
    { 
        cam.DestroyWhenOffSCreen(gameObject);
        score = scoreManager.score;
        spawnPercentage = manager.IncreasePercentage(score,spawnPercentage, 5f, 20f,100f);
    }

    void OnDisable()
    {
        groundSpawner.spawnGrounds();
    }

    void OnDestroy()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D _collider)
    {
        effects.SpawnEffect(splashEffect, _collider.transform.position, Quaternion.identity, 2f);

        if (_collider.collider.CompareTag("Player"))
        {
            menuScipts.Death();
        }
    }
    void SpawnFish(int _spawner)
    {
        GameObject _temp;

        //if (groundSpawner.hasWater)
        //{
            int _isFish = Random.Range(0, 2);
            switch (_isFish)
            {
                case 0:
                    int _whatFish = Random.Range(0, fishPrefabs.Length);
                    _temp = Instantiate(fishPrefabs[_whatFish], fishPoints[_spawner].position, Quaternion.identity, fishPoints[_spawner]);
                    break;
                case 1:
                    int _whatEdible = Random.Range(0, edibles.Length);
                    _temp = Instantiate(edibles[_whatEdible], fishPoints[_spawner].position, Quaternion.identity, fishPoints[_spawner]);
                    break;
            }
        //}
    }

    void SpawnEagles(int _spawner)
    {
        int _whatEagle = Random.Range(0, 101);
        float _groundWidth = transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;

        float _getGroundWidth = Random.Range(-(_groundWidth/2), (_groundWidth/2));
        if (_whatEagle < spawnPercentage+1)
        {
            GameObject _temp = Instantiate(EaglePrefab, new Vector2(EagleSpawns[_spawner].position.x + _getGroundWidth, EagleSpawns[_spawner].position.y), Quaternion.identity, EagleSpawns[_spawner]);
            eagleSpawned = true;
        }
    }
}
