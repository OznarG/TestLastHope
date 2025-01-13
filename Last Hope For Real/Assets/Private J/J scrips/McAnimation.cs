using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] float speed = 0f;
    [SerializeField] float Runspeed = 10f;
    private Animator Skeleton;
    private Rigidbody rb;

    bool running;
    // Start is called before the first frame update
    void Start()
    {
        Skeleton =GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");     

        Vector3 movement = new Vector3(horizontal, 0, Vertical) * speed * Time.deltaTime;

      

        rb.MovePosition(transform.position + movement);
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = Runspeed;
            running = true;


        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5f;
            running = false;
            
        }
        if(horizontal == 0 && Vertical == 0)
        {
            speed = 0f;
            running = false;
        }
        else
        {
            if(!running)
            {
              speed = 5f;
            }
            else
            {
                speed = Runspeed;
            }
            
        }
        
        Debug.Log(movement.magnitude);

        Skeleton.SetFloat("speed", speed);
    }
}
