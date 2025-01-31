using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DFAttackNode : Node
{
    private NavMeshAgent agent;
    public DragonFlacoAI ai;
    Transform path;

    public DFAttackNode(NavMeshAgent agent, DragonFlacoAI ai)
    {

        this.agent = agent;
        this.ai = ai;

    }

    public override NodeState Evaluate()
    {
        Debug.Log(ai.canAttack);
        if(ai.canAttack == true)
        {
            ai.attacking = true;
            ai.canAttack = false;
            Debug.Log("Attacking before animation");
            ai.enemiesReferences.animator.SetBool("Attacking", ai.attacking);
        }

        if (ai.attacking == true)
        {
            
            //agent.isStopped = false;
            agent.SetDestination(ai.target.position);
            //ai.roaming = true;           
            return NodeState.RUNNING;
        }
        else
        {
            //Debug.Log("Roamming ended");
            //agent.isStopped = true;
            //ai.roaming = false;

            return NodeState.SUCCESS;
        }
        
    }
}
