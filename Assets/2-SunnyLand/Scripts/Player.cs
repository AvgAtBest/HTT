using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plugins.Controllers;

namespace SunnyLand
{
    public class Player : MonoBehaviour
    {
        public float gravity = -25f;
        public float runSpeed = 8f;
        public float climbSpeed = 4f;
        public float groundDamping = 20f; // how fast do we change direction?
        public float inAirDamping = 5f;
        public float jumpHeight = 3f;
        public bool isClimbing;
        private CharacterController2D controller; //the player controller
        private Animator anim; //the animator
        private SpriteRenderer rend; //the sprite renderer
        private Vector3 velocity; // calculate velocity

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();//gets the animator
            rend = GetComponent<SpriteRenderer>();//gets the sprite renderer
            controller = GetComponent<CharacterController2D>();//gets the controller
        }

        // Update is called once per frame
        void Update()
        {
            //if the player is grounded
            if (controller.isGrounded)
            {
                //resets the y velocity
                velocity.y = 0f;
            }
            float inputH = Input.GetAxis("Horizontal");//left and/or right (A or D)
            float inputV = Input.GetAxis("Vertical");//Up and/or down (W or S)
            //if button is pressed (horizontal)
            if (inputH != 0)
            {
                //Check what direction the sprite should be flipped
                rend.flipX = inputH < 0;
            }
            //Move Horizontall
            velocity.x = inputH * runSpeed;
            if (controller.isGrounded && Input.GetButtonDown("Jump") && !isClimbing)
            {

                velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);

                Debug.Log("Jump Willy!");
            }

            if (!isClimbing)
            {
                //Apply Gravity
                velocity.y += gravity * Time.deltaTime;
            }
            else
            {
                velocity.y = 0;
            }

            if (controller.isGrounded && inputV < 0)
            {
                if (!Input.GetButtonDown("Jump"))
                {
                    velocity.y *= 3f;
                }
                Debug.Log("Swoosh");

                controller.ignoreOneWayPlatformsThisFrame = true;
            }
            if (isClimbing)
            {
                //velocity.y = 0;
                if (inputV > 0)
                {

                    velocity.y += climbSpeed;

                    //transform.Translate(0, 1 * Time.deltaTime, 0);

                }
                else if (inputV < 0)
                {


                    velocity.y -= climbSpeed;
                    //transform.Translate(0, -1 * Time.deltaTime, 0);
                }

            }
            //Apply Velocity to controller
            controller.Move(velocity * Time.deltaTime);//moves character controller

            velocity = controller.velocity;

            UpdateAnim();
        }
        void UpdateAnim()
        {
            if (!isClimbing)
            {
                anim.SetBool("IsGrounded", controller.isGrounded);
                anim.SetFloat("JumpY", controller.velocity.normalized.y);

                anim.SetBool("IsRunning", controller.velocity.x != 0);

                if (controller.isGrounded && Input.GetKey(KeyCode.C))
                {
                    anim.SetBool("IsCrouching", true);
                }
                if (controller.isGrounded && Input.GetKeyUp(KeyCode.C))
                {
                    anim.SetBool("IsCrouching", false);
                }
            }
            else if (isClimbing)
            {
                anim.SetBool("IsClimbing", isClimbing);

                anim.SetBool("IsGrounded", false);
                anim.SetBool("IsRunning", false);

            }


        }
        public void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Ladder")
            {

                isClimbing = true;
                velocity.y = 0;

            }

        }
        public void OnTriggerExit2D(Collider2D collision)
        {
            //anim.SetBool("IsClimbing", false);
            isClimbing = false;

            gravity = -25f;
        }
    }
}
