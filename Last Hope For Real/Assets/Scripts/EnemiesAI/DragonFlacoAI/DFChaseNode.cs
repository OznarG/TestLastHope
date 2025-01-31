using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DFChaseNode : Node
{
    private Transform target;
    private NavMeshAgent agent;
    public DragonFlacoAI ai;

    public DFChaseNode(Transform transform, NavMeshAgent agent, DragonFlacoAI ai)
    {
        this.target = transform;
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {       
        float distance = Vector3.Distance(target.position, agent.transform.position);
        if (distance > ai.enemiesReferences.navMeshAgent.stoppingDistance)
        {
           
            agent.isStopped = false;
            agent.SetDestination(target.position);
            return NodeState.RUNNING;
        }
        else
        {
            agent.isStopped = true;
            return NodeState.SUCCESS;
        }
    }
}
