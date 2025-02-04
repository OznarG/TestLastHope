using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DFRoamNode : Node
{
    private NavMeshAgent agent;
    public DragonFlacoAI ai;
    Transform path;

    public DFRoamNode(NavMeshAgent agent, DragonFlacoAI ai)
    {

        this.agent = agent;
        this.ai = ai;

    }

    public override NodeState Evaluate()
    {
        
        if (ai.enemyStats.roaming == false)
        {
           path = ai.GetPath();
           
        }
        if (path == null)
        {
            return NodeState.FAILURE;
        }
        //ai.SetColor(Color.yellow);
        
        float distance = Vector3.Distance(path.position, agent.transform.position);       
        if (distance > 1.2)
        {
            
            agent.isStopped = false;
            agent.SetDestination(path.position);
            ai.enemyStats.roaming = true;           
            return NodeState.RUNNING;
        }
        else
        {
            
            agent.isStopped = true;
            ai.enemyStats.roaming = false;
            return NodeState.SUCCESS;
        }
        
    }
}
