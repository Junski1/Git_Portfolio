using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyTypes = Enemy.enemyTypes;

public class SpawnEnemies : MonoBehaviour
{
    public int enemyCount = 0;
    [SerializeField] GameObject enemyGO = null;
    [SerializeField] GameObject enemySnipers = null;

    [SerializeField] GameObject[] enemies = null;

    void Start()
    {
        enemyCount = enemyGO.transform.childCount;

        GetEnemySpawns();
    }
    // Update is called once per frame
   

    void GetEnemySpawns()
    {
        foreach (Transform spawn in enemyGO.transform)
        {
            Instantiate(enemies[Random.Range(0, EnemyTypes.GetNames(typeof(EnemyTypes)).Length-1)], spawn.position, Quaternion.identity);
            enemyCount++;
        }

        foreach (Transform spawn in enemySnipers.transform)
        {
            Instantiate(enemies[((int)EnemyTypes.sniper)], spawn.position, Quaternion.identity);
            enemyCount++;
        }
    }
}
