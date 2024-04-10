using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpawner : MonoBehaviour
{
    Vector3 nextSpawnPoint;
    public GameObject[] bgPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        nextSpawnPoint = transform.position;

        for (int _i = 0; _i < 3; _i++)
        {
            spawnBG();
        }
    }

    public void spawnBG()
    {
        int _nextBG = Random.Range(0, bgPrefabs.Length);

        GameObject _temp = Instantiate(bgPrefabs[_nextBG], nextSpawnPoint, Quaternion.identity, gameObject.transform);
        nextSpawnPoint = _temp.transform.GetChild(_temp.transform.childCount - 1).position;
    }
}
