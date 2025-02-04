using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("----- Enemy Stats -----")]
    public float health;
    public float speed;
    public float enemyHealth;
    public float attackingSpeed;
    public float attackingDistance;

    [Header("----- Enemy States -----")]
    public bool playerInRange = false;
    public bool roaming = false;
    public bool attacking = false;
    public bool canAttack = true;
    public EnemyState enemyState;
}
