﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ak
{
    [RequireComponent(typeof(SpriteRenderer),typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("speed factor")]
        public float moveSpeed = 7.0f;
        public float jumpSpeed = 3.0f;
        public float jumpForce = 5.0f;

        public Vector2 inputDir = new Vector2();


        Rigidbody2D rigidBody = null;
        public Collider2D mainCollider = null;


        public enum CtrlType
        {
            PressJump,
            HoldJump,
            ReleaseJump,

            Max,
        }

        [Header("input flag")]
        [SerializeField]
        public InputButton btnJump = new InputButton("Jump");
        [SerializeField]
        public InputButton btnFire1 = new InputButton("Fire1");


        [Header("state flag")]
        public bool isJumpUp = false;
        public bool isOnGround = false;

        [Header("jump factor")]
        public float jumpHoldDuration = 1.0f;


        [Header("Environment")]
        //public LayerMask[] groundLayers;
        public Vector3[] footOffsets = null;
            

        float jumpTime = 0;

        SpriteRenderer spriteRenderer = null;

        
        
        public enum Facing
        {
            Right,
            Left,
        }
        public Facing facing = Facing.Right;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            mainCollider = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {

        }

        void Update()
        {
            //CollectInput();
        }

        private void FixedUpdate()
        {
            CollectInput();

            PhysicCheck();
            HorizonMovement();
            JumpMovement();
        }


        private void CollectInput()
        {
            inputDir.x = Input.GetAxisRaw("Horizontal");
            btnJump.Collect();
            btnFire1.Collect();
        }

        private void PhysicCheck()
        {
            isOnGround = false;
            for (int i = 0; i < footOffsets.Length; i++)
            {
                Vector2 from = new Vector2();
                from.x = transform.position.x + footOffsets[i].x;
                from.y = transform.position.y + footOffsets[i].y;
                float checkLength = 0.25f;
                string[] checkLayers = { "AK_ground" };
                RaycastHit2D hit = Physics2D.Raycast(from, new Vector2(0, -1), checkLength, LayerMask.GetMask(checkLayers));

                Color col = new Color(0, 0, 0);
                if (hit.collider != null)
                {
                    isOnGround = true;
                    col.r = 1;
                }
                else
                {
                    col.g = 1;
                }
                Debug.DrawRay(transform.position + footOffsets[i], new Vector3(0, -checkLength, 0), col);

            }
        }

        void HorizonMovement()
        {
            float xSpeed = inputDir.x * moveSpeed;
            rigidBody.velocity = new Vector2(xSpeed, rigidBody.velocity.y);
            UpdateFacing(inputDir.x);
        }

        void JumpMovement()
        {
            if (!isJumpUp)
            {
                if (isOnGround && btnJump.isPress)
                {
                    //Debug.Log("jump press start jump");
                    isJumpUp = true;
                    jumpTime = Time.time + jumpHoldDuration;
                    //rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
                    rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                }
            }
            else
            {
                if (btnJump.isHold && Time.time <= jumpTime)
                {
                    //rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
                    rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                }
                else
                {
                    isJumpUp = false;
                }
            }
        }


        void UpdateFacing(float inputX)
        {
            if (inputX > 0 && facing == Facing.Left)
            {
                spriteRenderer.flipX = false;
                //transform.localScale = new Vector3(1, 1, 1);
                facing = Facing.Right;
            }
            else if (inputX < 0 && facing == Facing.Right)
            {
                //transform.localScale = new Vector3(-1, 1, 1);
                spriteRenderer.flipX = true;
                facing = Facing.Left;
            }
        }
    }

}
