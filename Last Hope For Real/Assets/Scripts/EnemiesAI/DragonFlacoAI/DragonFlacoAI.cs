using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DragonFlacoAI : MonoBehaviour, IDamage
{

    [Header("----- Enemy References -----")]
    public EnemyStats enemyStats;
    public DamageTester damageSourceScript;
    public GameObject jawDamageSource;
    public Transform target;
    public EnemiesReferences enemiesReferences;
    [SerializeField] public Transform[] availableWayPoints;
    private Node topNode;
    private float pathUpdateDeadLine;

    
    private void Awake()
    {
        enemiesReferences = GetComponent<EnemiesReferences>();
        damageSourceScript = jawDamageSource.GetComponent<DamageTester>();
        damageSourceScript._damage = 20;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyStats.attackingDistance = enemiesReferences.navMeshAgent.stoppingDistance;
        ConstructBehaviourTree();
    }
    void Update()
    {
        topNode.Evaluate();
        if (topNode.nodeState == NodeState.FAILURE)
        {
            Debug.Log("Nodefailled");         
        }
        if (topNode.nodeState == NodeState.SUCCESS)
        {
            Debug.Log("Nodefailled");
        }
        enemiesReferences.animator.SetFloat("Speed", enemiesReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
       // LookAtTarget();
    }
    private void UpdatePath()
    {
        if (Time.time >= pathUpdateDeadLine)
        {
            //Debug.Log("Updating Path");
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
        if (other.CompareTag("Player"))
        {
            enemyStats.playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyStats.playerInRange = false;
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
        //Debug.Log("Attack Done");
        enemyStats.attacking = false;
        //Debug.Log(enemyStats.attacking);
        enemiesReferences.animator.SetBool("Attacking",enemyStats.attacking);
        jawDamageSource.SetActive(false);
        StartCoroutine(attackCoolDown());
    }


    IEnumerator attackCoolDown()
    {
        
        yield return new WaitForSeconds(2);
        enemyStats.canAttack = true;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Draco took damage");
        enemyStats.health -= damage;
    }
}
