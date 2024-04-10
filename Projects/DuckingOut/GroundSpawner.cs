using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    Vector3 nextSpawnPoint;

    public GameObject[] groundPrefabs;

    public bool hasWater;
    // Start is called before the first frame update
    void Start()
    {
        nextSpawnPoint = transform.position;

        for (int _i = 0; _i <3; _i++)
        {
            spawnGrounds();
        }
    }

    public void spawnGrounds()
    {
        int _nextGround = Random.Range(0, groundPrefabs.Length);
        hasWater = true;
        //if(_nextGround == groundPrefabs.Length-1)
        //{
        //    hasWater = true;
        //}
        //else
        //{
        //    hasWater = false;
        //}

        GameObject _temp = Instantiate(groundPrefabs[_nextGround], nextSpawnPoint, Quaternion.identity, gameObject.transform);
        nextSpawnPoint = _temp.transform.GetChild(_temp.transform.childCount-1).position;
    }
}
