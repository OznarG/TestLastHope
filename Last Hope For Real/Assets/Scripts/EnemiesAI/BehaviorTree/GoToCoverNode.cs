using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToCoverNode : Node
{

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
        float distance = Vector3.Distance(cover.position, agent.transform.position);
        Debug.Log(distance);
        if(distance > 0.2f)
        {
            agent.isStopped = false;
            agent.SetDestination(cover.position);
            return NodeState.RUNNING;
        }
        else
        {
            agent.isStopped = true;
            return NodeState.SUCCESS;
        }
    }
}
