using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DragonFlacoAI : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] float speed;

    public Transform target;

    public EnemiesReferences enemiesReferences;

    private float pathUpdateDeadLine;

    private float attackingDistance;
    [SerializeField] public Transform[] availableWayPoints;
    [Header("- Enemy Stats -")]
    [SerializeField] float enemyHealth;
    [SerializeField] float attackingSpeed;
    private Node topNode;
    public bool playerInRange = false;
    public bool roaming = false;
    public bool attacking = false;
    public bool canAttack = true;

    //bool playerIn = false;
    private void Awake()
    {
        enemiesReferences = GetComponent<EnemiesReferences>();
    }
    // Start is called before the first frame update
    void Start()
    {
        attackingDistance = enemiesReferences.navMeshAgent.stoppingDistance;
        ConstructBehaviourTree();
    }
    void Update()
    {
        topNode.Evaluate();
        if (topNode.nodeState == NodeState.FAILURE)
        {
            Debug.Log("Nodefailled");
            //SetColor(Color.black);
        }
        if (topNode.nodeState == NodeState.SUCCESS)
        {
            Debug.Log("Nodefailled");
            //SetColor(Color.grey);
        }
        enemiesReferences.animator.SetFloat("Speed", enemiesReferences.navMeshAgent.desiredVelocity.sqrMagnitude);

        //if (target != null)
        //{
        //    bool inRange = Vector3.Distance(transform.position, target.position) < attackingDistance;
        //    if (inRange)
        //    {
        //        LookAtTarget();
        //    }
        //    else
        //    {
        //        UpdatePath();
        //    }
        //    enemiesReferences.animator.SetBool("Attacking", inRange);
        //}
        //enemiesReferences.animator.SetFloat("Speed", enemiesReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
    }
    private void UpdatePath()
    {
        if (Time.time >= pathUpdateDeadLine)
        {
            Debug.Log("Updating Path");
            pathUpdateDeadLine = Time.time + enemiesReferences.pathUpdateDelay;
            enemiesReferences.navMeshAgent.SetDestination(target.position);
        }
    }

    private void LookAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")/* && playerIn == false*/)
        {
            //Debug.Log("player just got in");
            //Debug.Log(playerInRange);
            playerInRange = true;
            //Debug.Log(playerInRange);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")/* && playerIn == false*/)
        {
            //Debug.Log("player just got out");
            //Debug.Log(playerInRange);
            playerInRange = false;
            //Debug.Log(playerInRange);

        }
    }

    public Transform GetPath()
    {
        int randomIndex = Random.Range(0, availableWayPoints.Length);
        return availableWayPoints[randomIndex];
    }

    private void ConstructBehaviourTree()
    {
        InSightNode inSightNode = new InSightNode(this);
        DFChaseNode chasePlayer = new DFChaseNode(target, enemiesReferences.navMeshAgent, this);

        NotInRangeNode notInRangeNode = new NotInRangeNode(this);
        DFRoamNode roamNode = new DFRoamNode(enemiesReferences.navMeshAgent, this);

        InAttackRangeNode inAttackRangeNode = new InAttackRangeNode(this);
        DFAttackNode attackNode = new DFAttackNode(enemiesReferences.navMeshAgent, this);

        Sequence roamSequence = new Sequence(new List<Node> { notInRangeNode, roamNode });
        Sequence chaseSequence = new Sequence(new List<Node> { inSightNode, chasePlayer });
        Sequence attackSequence = new Sequence(new List<Node>() { inAttackRangeNode, attackNode });

        topNode = new Selector(new List<Node> { attackSequence, chaseSequence, roamSequence });


    }



    public void BasicAttackDone()
    {
        Debug.Log("Attack Done");
        attacking = false;
        Debug.Log(attacking);
        enemiesReferences.animator.SetBool("Attacking",attacking);
        StartCoroutine(attackCoolDown());
    }

    IEnumerator attackCoolDown()
    {
        
        yield return new WaitForSeconds(2);
        canAttack = true;
    }
}
