using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class equip : MonoBehaviour
{
    public GameObject axe;
    public Transform handpos;
    public bool equipA;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)&& equipA)
        {
            EquipWeapon();
           
        }else if (Input.GetKeyDown(KeyCode.F)&& !equipA)
        {
            UnequipWeapon();
        }
    }
    void EquipWeapon(){
        if(axe!= null)
        {
            
            axe.transform.SetParent(handpos);
            axe.transform.localPosition = new Vector3(0.146f,0.103f,-0.051f);
            axe.transform.localRotation = Quaternion.Euler(84.9f, -26.58f, -119.3f);
            equipA = false;
        }
       
    }
    void UnequipWeapon()
    {
        
        axe.transform.localPosition = new Vector3(0.146f, 15, -0.051f);
        axe.transform.localRotation = Quaternion.Euler(84.9f, -26.58f, -119.3f);
        equipA = true;
    }

}
