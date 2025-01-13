using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] float num = 10;
    float second;
    [SerializeField] SphereCollider colider;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello");
        //colider = gameObject.GetComponent<SphereCollider>();
        colider.radius = 50;
        //gameObject.transform.Translate(60,0,0);
        //second = num;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Hello Update");
        //gameObject.transform.position.x = 10;
        if (gameObject.transform.position.x <= 0)
        {
            second = num;
        }
        else if (gameObject.transform.position.x >= 60)
        {
            second = num;
            second = -second;

        }
        gameObject.transform.Translate(second * Time.deltaTime, 0, 0);
    }
}
