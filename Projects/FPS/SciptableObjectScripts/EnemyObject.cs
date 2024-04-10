using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyTypes = Enemy.enemyTypes;

[CreateAssetMenu(fileName = "New enemy", menuName = "Enemy")]
public class EnemyObject : ScriptableObject
{
    public EnemyTypes type;
    public GameObject weapon;
    public float attackRange, sightRange, walkPointRange, patrolSpeed, chaseSpeed, timeBetweenAttacks;
}
