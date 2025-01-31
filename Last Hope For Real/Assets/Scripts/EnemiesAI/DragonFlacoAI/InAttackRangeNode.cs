using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAttackRangeNode : Node
{
    private DragonFlacoAI ai;


    public InAttackRangeNode(DragonFlacoAI ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(ai.target.position, ai.enemiesReferences.navMeshAgent.transform.position);
        return distance <= ai.enemiesReferences.navMeshAgent.stoppingDistance ? NodeState.SUCCESS : NodeState.FAILURE;
    }

}
