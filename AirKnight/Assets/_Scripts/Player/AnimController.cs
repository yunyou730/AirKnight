using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ak
{
    public class AnimController : MonoBehaviour
    {
        Animator animator = null;
        PlayerController controller = null;
        Rigidbody2D rigidBody = null;


        public bool isGroundAttacking = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            controller = GetComponent<PlayerController>();
            rigidBody = GetComponent<Rigidbody2D>();
        }
        
        private void Update()
        {
            UpdateAnim();
        }

        /*
        private void FixedUpdate()
        {
            UpdateAnim();
        }
        */

        private void UpdateAnim()
        {
            if (controller.isOnGround)
            {
                if (controller.btnFire1.isPress)
                {
                    isGroundAttacking = true;
                    animator.Play("attack");
                }


                if (!isGroundAttacking)
                {
                    if (controller.inputDir.magnitude == 0)
                    {
                        animator.Play("idle");
                    }
                    else
                    {
                        animator.Play("walk");
                    }
                }

            }
            else
            {
                animator.Play("jump");
            }
        }

        private void GroundAttack()
        {

        }

        private void AirAttack()
        {

        }


        public void OnAttackEnd()
        {
            Debug.Log("OnAttackEnd()");
            isGroundAttacking = false;
        }
    }

}
