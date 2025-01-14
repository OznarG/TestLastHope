using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCOntroller : MonoBehaviour
{
    [SerializeField] float Maxspeed = 2.0f;
    private Animator animator;
    public bool Combat = false;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float move = Input.GetAxis("Vertical");
        float speed = Mathf.Abs(move) / Maxspeed;
        float adjustedSpeed = speed * 3.5f;
        animator.SetFloat("speed", adjustedSpeed);

        if (!Combat)
        { 
            transform.Translate(Vector3.forward * move * Time.deltaTime);
        }
        


        if (Input.GetKeyDown(KeyCode.Space))
        {

            //  transform.Translate(Vector3.up * 5 * Time.deltaTime);
            animator.SetBool("jump", true);

        } else if (Input.GetKeyUp(KeyCode.Space) )
        {
            animator.SetBool("jump", false);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Maxspeed = 1.0f;

        }else if (Input.GetKeyUp(KeyCode.LeftShift) && !Combat)
        {

            Maxspeed = 2.0f;
        }
      

        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("combat", true);
            Combat = true;
        }else if (Input.GetKeyDown(KeyCode.Q))
        {
            Combat = !true;
            animator.SetBool("combat", !true);
        }

    }
}
