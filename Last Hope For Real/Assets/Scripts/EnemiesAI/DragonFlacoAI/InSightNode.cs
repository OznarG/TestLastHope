using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSightNode : Node
{
    private DragonFlacoAI ai;


    public InSightNode(DragonFlacoAI ai)
    {
        this.ai = ai;
    }
        
    public override NodeState Evaluate()
    {
 
        return ai.enemyStats.playerInRange ? NodeState.SUCCESS : NodeState.FAILURE;
    }

}
