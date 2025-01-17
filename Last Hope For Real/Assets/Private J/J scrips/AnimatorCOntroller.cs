using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCOntroller : MonoBehaviour
{

    private CharacterController controller;
    [SerializeField] float Maxspeed = 2.0f;
    private Animator animator;
    public bool isGrounded;
    private bool isRolling;

    // Start is called before the first frame update
    void Start()
    {

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;


        float moveV = Input.GetAxis("Vertical");
        float moveH = Input.GetAxis("Horizontal");
        float move = Mathf.Max(Mathf.Abs(moveV), Mathf.Abs(moveH));
        float speed = move / Maxspeed;
        float adjustedSpeed = speed * 3.5f;
        animator.SetFloat("speed", adjustedSpeed);

       
            if (Input.GetKeyDown(KeyCode.Space) && !isRolling)
            {

               
                animator.SetBool("jump", true);

            } else
        {
            animator.SetBool("jump", false);
        }
                
           
                
        
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Maxspeed = 1.0f;
            Debug.Log("Shift key held down, max speed set to 1.0f");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {

            Maxspeed = 2.0f;
           
        }
      

       
        if(Input.GetKeyDown(KeyCode.LeftControl) && move > 0.5f && !isRolling)
        {
            isRolling = true;
            animator.SetBool("roll", true);
            Debug.Log("is rolling true");
            
        }
       


    }
    public void EndRoll() {
        isRolling = false; 
        animator.SetBool("roll", false);
        Debug.Log("is rolling false");
    }
}
