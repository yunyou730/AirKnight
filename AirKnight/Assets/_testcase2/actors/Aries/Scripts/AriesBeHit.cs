using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ff
{
    public class AriesBeHit : CanBeHit
    {   
        private SpriteRenderer m_spriteRenderer = null;
        private Rigidbody2D m_rigidBody = null;
        private ff.AriesAnimBridge m_animBridge = null;
        private Animator m_animator = null;
        private AriesController m_ctrl = null;

        private bool m_bPlayingHit = false;


        [SerializeField]
        private float m_hitAwaySpeed = 3.0f;
        [SerializeField]
        private float m_angleFactor = 0.5f;

        [SerializeField]
        private float m_maxHurtPeriod = 1.0f;
        private float m_prevGravityScale = 1.0f;

        private Vector2 m_bounceVelocity = Vector2.zero;
        
        Coroutine m_endHurtCo = null;
        
        private void Awake()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
            m_rigidBody = GetComponent<Rigidbody2D>();
            m_animBridge = GetComponent<ff.AriesAnimBridge>();
            m_animator = GetComponent<Animator>();
            m_ctrl = GetComponent<AriesController>();
        }

        private void FixedUpdate()
        {
            if (m_bPlayingHit)
            {
                m_rigidBody.velocity = m_bounceVelocity;
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            TryTerminateHurtProcess(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            TryTerminateHurtProcess(collision);
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

            m_ctrl.enabled = false;

            // play anim
            FaceToAttacker(caster);
            m_animator.SetTrigger(m_animBridge.hurtTrigger);

            // play color
            m_spriteRenderer.color = new Color(1, 0, 0, 1);

            Vector2 dir = GetHitDir(caster);
            m_prevGravityScale = m_rigidBody.gravityScale;
            m_rigidBody.gravityScale = 0;
            m_bounceVelocity = dir * m_hitAwaySpeed;

            // restore later 
            m_endHurtCo = StartCoroutine(EndHurt());
        }

        private IEnumerator EndHurt()
        {
            yield return new WaitForSeconds(m_maxHurtPeriod);
            DoEndHurt();
        }

        private void DoEndHurt()
        {
            m_ctrl.enabled = true;

            // recover anim
            m_animator.SetTrigger(m_animBridge.hurtRecoverTrigger);

            m_bounceVelocity = Vector2.zero;

            m_spriteRenderer.color = new Color(1, 1, 1, 1);
            m_bPlayingHit = false;

            m_rigidBody.velocity = Vector2.zero;
            m_rigidBody.gravityScale = m_prevGravityScale;
        }

        private void FaceToAttacker(GameObject caster)
        {
            if (transform.position.x < caster.transform.position.x)
            {
                m_ctrl.SetFace(FaceDir.RIGHT);
            }
            else if (transform.position.x > caster.transform.position.x)
            {
                m_ctrl.SetFace(FaceDir.LEFT);
            }
        }

        private Vector2 GetHitDir(GameObject caster)
        {
            Vector2 dir = transform.position.x >= caster.transform.position.x ?
                                    new Vector2(1, m_angleFactor) :
                                    new Vector2(-1, m_angleFactor);
            return dir.normalized;
        }

        private void TryTerminateHurtProcess(Collision2D collision)
        {
            if (m_bPlayingHit && m_endHurtCo != null)
            {
                float dot = Vector2.Dot(m_bounceVelocity, collision.contacts[0].normal);
                // 夹角 > 90  度
                if (dot < 0)
                {
                    StopCoroutine(m_endHurtCo);
                    m_endHurtCo = null;
                    DoEndHurt();
                }
            }
        }
    }
}
