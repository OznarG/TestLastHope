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
        //Debug.Log("InCondition");
        Debug.Log("in range?" + ai);
        return ai.playerInRange ? NodeState.SUCCESS : NodeState.FAILURE;
    }

}
