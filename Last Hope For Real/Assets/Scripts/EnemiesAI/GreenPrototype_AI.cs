using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPrototype_AI : MonoBehaviour
{
    public Transform target;

    private EnemiesReferences enemiesReferences;

    private float pathUpdateDeadLine;

    private float attackingDistance;

    [Header("- Enemy Stats -")]
    [SerializeField] float enemyHealth;
    [SerializeField] float attackingSpeed;

    private void Awake()
    {
        enemiesReferences = GetComponent<EnemiesReferences>();
    }
    // Start is called before the first frame update
    void Start()
    {
        attackingDistance = enemiesReferences.navMeshAgent.stoppingDistance;
    }
    void Update()
    {
        if (target != null)
        {
            bool inRange = Vector3.Distance(transform.position, target.position) < attackingDistance;

            if (inRange)
            {
                LookAtTarget();
            }
            else
            {
                UpdatePath();
            }

            enemiesReferences.animator.SetBool("Attacking", inRange);
        }   
        enemiesReferences.animator.SetFloat("Speed", enemiesReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
    }
    private void UpdatePath()
    {
        if(Time.time >= pathUpdateDeadLine)
        {
            Debug.Log("Updating Path");
            pathUpdateDeadLine = Time.time + enemiesReferences.pathUpdateDelay;
            enemiesReferences.navMeshAgent.SetDestination(target.position);
        }
    }

    private void LookAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    // Update is called once per frame
    
}
