using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ak.os
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Base class")]

        public float minGroundNormalY = 0.65f;
        public float gravityModifier = 1.0f;
        public Vector2 velocity;
        public bool IsGrounded { get;private set; }

        private Rigidbody2D body = null;


        [Header("Sub class")]
        public float maxSpeed = 7;
        public float jumpTakeOfSpeed = 7;


        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed,
        }

        public JumpState jumpState = JumpState.Grounded;

        private bool stopJump = false;

        public Collider2D collider2d = null;

        SpriteRenderer spriteRenderer = null;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {

        }


        void Update()
        {

        }
    }

}

