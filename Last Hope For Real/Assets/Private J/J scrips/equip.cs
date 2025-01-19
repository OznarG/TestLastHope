using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equip : MonoBehaviour
{
    public GameObject axe;
    public Transform handpos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            EquipWeapon();
        }
    }
    void EquipWeapon(){
        if(axe!= null)
        {
            axe.transform.SetParent(handpos);
            axe.transform.localPosition = new Vector3(-0.02f,0.4f,-0.06f);
            axe.transform.localRotation = Quaternion.Euler(1.4f, 50.0f, -2.87f);
        }
        
    }
}
