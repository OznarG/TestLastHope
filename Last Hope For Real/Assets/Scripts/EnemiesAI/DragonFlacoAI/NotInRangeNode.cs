using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotInRangeNode : Node
{
    private DragonFlacoAI ai;


    public NotInRangeNode(DragonFlacoAI ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {

        return ai.playerInRange ? NodeState.FAILURE : NodeState.SUCCESS;
    }

}

