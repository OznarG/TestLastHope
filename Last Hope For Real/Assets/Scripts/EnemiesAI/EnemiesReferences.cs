using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesReferences : MonoBehaviour
{
   [HideInInspector] public NavMeshAgent navMeshAgent;
   [HideInInspector] public Animator animator;

    [Header("Stats")]

    public float pathUpdateDelay = 0.2f;

    private void Awake()
    {     
        animator = gameObject.GetComponentInChildren<Animator>();
        
        if (animator != null)
        {
            Debug.Log("Now active");
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
