using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPrototypeAttack : MonoBehaviour
{
    [Header("General")]
    public GameObject mouthAttackSource;

    private EnemiesReferences enemyReferences;

    public DamageTester damageSourceScript;

    public int gamage;
    
    // Start is called before the first frame update
    private void Awake()
    {
        enemyReferences = GetComponent<EnemiesReferences>();
        damageSourceScript = mouthAttackSource.GetComponent<DamageTester>();
        damageSourceScript._damage = 20;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BasicAttack()
    {
        mouthAttackSource.SetActive(true);
    }

    public void BasicAttackDone()
    {
        mouthAttackSource.SetActive(false);
    }
}
