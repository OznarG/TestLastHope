using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToCoverNode : Node
{
    private Transform target;
    private NavMeshAgent agent;
    public EnemyAITest ai;

    public GoToCoverNode(NavMeshAgent agent, EnemyAITest ai)
    {
        
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Transform cover = ai.GetBesCoverSpot();
        if (cover == null)
        {
            return NodeState.FAILURE;
        }
        ai.SetColor(Color.yellow);
        float distance = Vector3.Distance(target.position, agent.transform.position);
        if(distance > 0.2f)
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
