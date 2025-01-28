using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNode : Node
{
    private EnemyAITest ai;
    private float treshold;

    public HealthNode(EnemyAITest ai, float treshold)
    {
        this.ai = ai;
        this.treshold = treshold;
    }

    public override NodeState Evaluate()
    {
        return ai.currentHealth <= treshold ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
