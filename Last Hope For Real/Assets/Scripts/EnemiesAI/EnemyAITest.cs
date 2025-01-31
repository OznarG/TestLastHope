using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAITest : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float lowHealthThreshold;
    [SerializeField] private float healRestoreRate;

    [SerializeField] private float chasingRange;
    [SerializeField] private float shootingRange;

    [SerializeField] private Transform playertransform;
    [SerializeField] private Cover[] availableCovers;

    private Material material;
    private Transform bestCoverSpot;
    private NavMeshAgent agent;

    private Node topNode;

    private float _currentHealth;
    public float currentHealth
    {
        get { return _currentHealth; } 
        set { _currentHealth = Mathf.Clamp( value, 0, startingHealth); }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        material = GetComponent<MeshRenderer>().material;
    }
    private void Start()
    {
        currentHealth = startingHealth;     
        ConstructBehaviourTree();
    }

    private void Update()
    {
        topNode.Evaluate();
        if(topNode.nodeState == NodeState.FAILURE)
        {
            Debug.Log("Nodefailled");
            SetColor(Color.black);
        }
        if (topNode.nodeState == NodeState.SUCCESS)
        {
            Debug.Log("Nodefailled");
            SetColor(Color.grey);
        }
        currentHealth += Time.deltaTime * healRestoreRate;
    }

    private void OnMouseDown()
    {
        currentHealth -= 10f;
    }
    public void SetColor(Color color)
    {
        material.color = color;
    }

    internal void SetBestCover(Transform bestSpot)
    {
        this.bestCoverSpot = bestSpot;
    }

    internal Transform GetBesCoverSpot()
    {
        return bestCoverSpot;
    } 
    
    private void ConstructBehaviourTree()
    {
        IsCoverAvailableNode coverAvailableNode = new IsCoverAvailableNode(availableCovers, playertransform, this);
        GoToCoverNode gotCoverNode = new GoToCoverNode(agent, this);
        HealthNode healthNode = new HealthNode(this, lowHealthThreshold);
        IsCoveredNode isCoverNode = new IsCoveredNode(playertransform, transform);
        ChaseNode chaseNode = new ChaseNode(playertransform, agent, this);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, playertransform, transform);
        RangeNode shootingRangeNode = new RangeNode(shootingRange, playertransform, transform);
        ShootNode shootNode = new ShootNode(agent, this);

        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });

        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvailableNode, gotCoverNode });
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
        Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoverNode, findCoverSelector });
        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, findCoverSelector });

        topNode = new Selector(new List<Node> { mainCoverSequence, shootSequence, chaseSequence });


    }
}
