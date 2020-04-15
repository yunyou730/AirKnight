using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ff
{
    public class AriesBeHit : CanBeHit
    {
        private bool m_bCanBeHit = true;
        private SpriteRenderer m_spriteRenderer = null;
        private Rigidbody2D m_rigidBody = null;
        private ff.AriesAnimBridge m_animBridge = null;
        private Animator m_animator = null;

        private bool m_bPlayingHit = false;


        [SerializeField]
        private float m_hitAwaySpeed = 3.0f;
        [SerializeField]
        private float m_angleFactor = 0.5f;


        private float m_prevGravityScale = 1.0f;
        
        private void Awake()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
            m_rigidBody = GetComponent<Rigidbody2D>();
            m_animBridge = GetComponent<ff.AriesAnimBridge>();
            m_animator = GetComponent<Animator>();
        }
        
        public override void OnBeHit(GameObject caster)
        {
            if (m_bPlayingHit)
            {
                return;
            }
            StartHurt(caster);
        }
        
        private void StartHurt(GameObject caster)
        {
            m_bPlayingHit = true;

            // play
            m_animator.SetTrigger(m_animBridge.hurtTrigger);

            // play color
            m_spriteRenderer.color = new Color(1, 0, 0, 1);

            // kinematic
            Vector2 dir = GetHitDir(caster);
            m_rigidBody.AddForce(dir * m_hitAwaySpeed, ForceMode2D.Impulse);
            /*
             * Vector2 dir = GetHitDir(caster);
            m_prevGravityScale = m_rigidBody.gravityScale;
            m_rigidBody.gravityScale = 0;
            
            Debug.Log("dir " + dir);
            m_rigidBody.velocity = dir * m_hitAwaySpeed;
            Debug.Log("vel " + m_rigidBody.velocity);
            */
        }

        public void EndHurt()
        {
            m_spriteRenderer.color = new Color(1, 1, 1, 1);
            m_bPlayingHit = false;

            /*
            m_rigidBody.gravityScale = m_prevGravityScale;
            m_rigidBody.velocity = Vector2.zero;
            */

        }

        private Vector2 GetHitDir(GameObject caster)
        {
            Vector2 dir = transform.position - caster.transform.position;
            dir.y = Mathf.Abs(dir.x) * m_angleFactor;
            return dir.normalized;
        }
    }
}
