using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTester : MonoBehaviour
{
    public float _damage;
    bool playerIn;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && playerIn == false)
        {
            gameManager.instance.playerScript.TakeDamage(_damage);
        }
    }
}
