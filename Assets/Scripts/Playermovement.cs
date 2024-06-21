using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;
    public CharacterController controller;

    public float speed = 3f;
    public float shiftSpeed = 4.5f;
    public float sneak_speed = 1.5f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    private bool isRunning;
    private bool isCrouching;
    private bool isSneaking;
   


    public bool AlphaKey;
    public bool IsAttack;





    void Start()
    {
        animator = GetComponent<Animator>();
        isCrouching = isSneaking = AlphaKey = IsAttack = false;

    }

    // Update is called once per frame
    void Update()
    {
        //checking if we hit the ground to reset our falling velocity, otherwise we will fall faster the next time
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 1f)
        {
            velocity.y = -3f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //right is the red Axis, foward is the blue axis
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        // update
        
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);



        //check if the player is on the ground so he can jump
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);

        }

        //////////////////////////           Animator of Character

        
        if (move != Vector3.zero)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsSneaking", isCrouching);


            // Running
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Vector3 run = transform.right * x + transform.forward * z;
                controller.Move(shiftSpeed * run * Time.deltaTime);
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsWalking", false);
                isRunning = true;

            }
            else
            {
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsRunning", false);
                isRunning = false;
            }



            // Sneaking
            if (Input.GetKeyDown(KeyCode.C) && !isCrouching)
            {
                isSneaking = true;
                animator.SetBool("IsSneaking", true);

            }
            else if (Input.GetKeyDown(KeyCode.C) && isCrouching)
            {
                isSneaking = false;
                animator.SetBool("IsSneaking", false);
            }

        }
        else
        {

            animator.SetBool("IsWalking", false);
        }


        /// crouching
        if (Input.GetKeyDown(KeyCode.C) && !isCrouching)
        {
            isCrouching = true;
            animator.SetBool("IsCrouching", true);
            animator.SetBool("IsWalkForward", true);
            if (isRunning) {
                animator.SetBool("IsCrouching", false);
                animator.SetBool("IsRunning", true);

            }
            else
            {
                animator.SetBool("IsCrouching", true);
                animator.SetBool("IsRunning", false);
            }
            

        }
        else if (Input.GetKeyDown(KeyCode.C) && isCrouching)
        {
            isCrouching = false;
            animator.SetBool("IsCrouching", false);
            animator.SetBool("IsWalkForward", false);


        }






        /// Combat
        if (Input.GetKeyDown(KeyCode.Alpha1) && !AlphaKey || Input.GetKeyDown(KeyCode.Alpha2) && !AlphaKey || Input.GetKeyDown(KeyCode.Alpha3) && !AlphaKey)
        {
            AlphaKey = true;
            animator.SetBool("IsDrawing",true);
            
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1) && AlphaKey || Input.GetKeyDown(KeyCode.Alpha2) && AlphaKey || Input.GetKeyDown(KeyCode.Alpha3) && AlphaKey)
        {
            AlphaKey = false;
            animator.SetBool("IsDrawing", false);
        }


       





        /// Click left mouse

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject selectedTree = SelectionManager.Instance.selectedTree;
            if (selectedTree != null)
            {
                selectedTree.GetComponent<ChoppableTree>().GetHit();
            }
            animator.SetTrigger("Hit");

        }
       

        //


        /// Walking back
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("IsWalkBack", true);

        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("IsWalkBack", false);
        }

        /// Walking left

        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("IsWalkLeft", true);

        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("IsWalkLeft", false);
        }

        /// Walking right
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("IsWalkRight", true);

        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("IsWalkRight", false);
        }


        

      

    }

}
