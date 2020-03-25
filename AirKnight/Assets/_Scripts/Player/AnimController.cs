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


        private void UpdateAnim()
        {
            if (controller.isOnGround)
            {
                float xSpeed = rigidBody.velocity.x;
                if (Mathf.Abs(xSpeed) >= 0.1f)
                {
                    animator.Play("walk");
                }
                else
                {
                    animator.Play("idle");
                }
            }
            else
            {
                animator.Play("jump");
            }
        }
        /*
        private void RefreshFace()
        {
            if (rigidBody.velocity.x < -0.1f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (rigidBody.velocity.x > 0.1f)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        */
    }

}
