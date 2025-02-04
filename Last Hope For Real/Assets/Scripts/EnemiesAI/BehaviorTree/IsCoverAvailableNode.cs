using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCoverAvailableNode : Node
{
    private Cover[] availableCovers;
    private Transform target;
    private EnemyAITest ai;

    public IsCoverAvailableNode(Cover[] availableCovers, Transform target, EnemyAITest ai)
    {
        this.availableCovers = availableCovers;
        this.target = target;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Transform bestSpot = FindBestCoverSpot();
        ai.SetBestCover(bestSpot);
        return bestSpot != null ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    private Transform FindBestCoverSpot()
    {
        float minAngle = 90;
        Transform bestSpot = null;
        for(int i = 0; i < availableCovers.Length; i++)
        {
            Transform bestSpotInCover = FindBestSpotInCover(availableCovers[i], ref minAngle);
            if(bestSpotInCover != null)
            {
                bestSpot = bestSpotInCover;
            }
        }
        return bestSpot;
    }

    private Transform FindBestSpotInCover(Cover cover, ref float minAngle)
    {
        Transform[] availableSpots = cover.GetCoverSpots();
        Transform bestSpot = null;
        for(int i = 0; i < availableSpots.Length; i++)
        {
            Vector3 direction = target.position - availableSpots[i].position;
            if (CheckIfCoverIsValid(availableSpots[i]))
            {
                float angle = Vector3.Angle(availableSpots[i].forward, direction);
                if(angle < minAngle)
                {
                    minAngle = angle;
                    bestSpot = availableSpots[i];
                }
            }
        }
        return bestSpot;
    }

    private bool CheckIfCoverIsValid(Transform spot)
    {
        RaycastHit hit;
        Vector3 direction = target.position - spot.position;
        if(Physics.Raycast(spot.position, direction, out hit))
        {
            if(hit.collider.transform != target)
            {
                return true;
            }
        }
        return false;
    }
}
