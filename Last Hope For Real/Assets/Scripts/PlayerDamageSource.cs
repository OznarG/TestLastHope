using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerDamageSource : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {Debug.Log(other.tag);
        Debug.Log("Something in");
        IDamage damageable = other.transform.GetComponent<IDamage>();
        if (damageable != null && !other.CompareTag("Player"))
        {
            
            damageable?.TakeDamage(20);
        }
    }
}
