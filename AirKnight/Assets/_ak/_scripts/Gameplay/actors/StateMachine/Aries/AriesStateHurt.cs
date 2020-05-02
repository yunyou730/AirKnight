using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesStateHurt : BaseState<AriesEntity>
    {
        AriesBeHit  m_beHit = null;
        AriesController     m_ctrl = null;
        AriesAnimBridge     m_animBridge = null;
        Transform   m_transform = null;
        Animator m_animator = null;
        SpriteRenderer  m_spriteRenderer = null;
        Rigidbody2D     m_rigidBody = null;
        EnvironmentDetector m_envDetector = null;


        float   m_elapsedTime = 0;
        Vector2 m_bounceVelocity = Vector2.zero;
        float m_prevGravityScale = 1;


        bool    m_bHurtFlying = false;

        public AriesStateHurt(AriesEntity entity) : base(entity)
        {
            m_beHit = entity.GetAgent().GetComponent<AriesBeHit>();
            m_ctrl = entity.GetAgent().GetComponent<AriesController>();
            m_animBridge = entity.GetAgent().GetComponent<AriesAnimBridge>();
            m_transform = entity.GetAgent().GetComponent<Transform>();
            m_spriteRenderer = entity.GetAgent().GetComponent<SpriteRenderer>();
            m_animator = entity.GetAgent().GetComponent<Animator>();
            m_rigidBody = entity.GetAgent().GetComponent<Rigidbody2D>();
            m_envDetector = entity.GetAgent().GetComponent<EnvironmentDetector>();
        }
        
        public override void OnEnter(AriesEntity entity,Telegram msg)
        {
            base.OnEnter(entity,msg);
            StartFly(msg);
        }

        public override void OnExit(AriesEntity entity)
        {
            base.OnExit(entity);
            m_animator.SetTrigger(m_animBridge.hurtRecoverTrigger);
            StopFly();
        }


        public override void Update(AriesEntity entity, float dt)
        {
            base.Update(entity, dt);
            m_elapsedTime += dt;
            if(m_elapsedTime >= m_beHit.m_maxHurtPeriod)
            {
                StopFly();
                
            }
        }

        public override void FixedUpdate(AriesEntity entity, float dt)
        {
            base.FixedUpdate(entity, dt);
            if(m_bHurtFlying)
            {
                m_rigidBody.velocity = m_bounceVelocity;
            }
            else if(m_envDetector.isOnGround)
            {
                MessageDispatcher.Instance().Dispatch(entity.GetID(),entity.GetID(),MessageType.MT_BreakHurt,null);
            }

            
        }

        public override bool HandleMessage(AriesEntity entity, Telegram msg)
        {
            switch(msg.m_msgType)
            {
                case MessageType.MT_BreakHurt:
                {
                    entity.GetFSM().FlipState();
                    return true;
                }
            }
            return base.HandleMessage(entity, msg);
        }

        private Vector2 GetHitDir(GameObject caster)
        {
            if(caster != null)
            {
                Vector2 dir = m_transform.position.x >= caster.transform.position.x ?
                                    new Vector2(1, m_beHit.m_angleFactor) :
                                    new Vector2(-1, m_beHit.m_angleFactor);
                return dir.normalized;
            }
            else
            {
                Vector3 front = m_ctrl.GetFront();
                return new Vector2(-front.x,front.y).normalized;
            }
        }


        private void FaceToAttacker(GameObject caster)
        {
            if(caster == null)
            {
                return;
            }
            if (m_transform.position.x < caster.transform.position.x)
            {
                m_ctrl.SetFace(FaceDir.RIGHT);
            }
            else if (m_transform.position.x > caster.transform.position.x)
            {
                m_ctrl.SetFace(FaceDir.LEFT);
            }
        }


        private void StartFly(Telegram msg)
        {
            m_elapsedTime = 0;
            m_bHurtFlying = true;

            TakeDamageExtraInfo damageInfo = (TakeDamageExtraInfo)msg.m_extraInfo;
            FaceToAttacker(damageInfo.caster);

            // anim
            m_animator.SetTrigger(m_animBridge.hurtTrigger);

            // sprite change color
            m_spriteRenderer.color = new Color(1, 0, 0, 1);

            // move with speed
            Vector2 dir = GetHitDir(damageInfo.caster);
            m_prevGravityScale = m_rigidBody.gravityScale;
            m_rigidBody.gravityScale = 0;
            m_bounceVelocity = dir * m_beHit.m_hitAwaySpeed;
        }

        private void StopFly()
        {
            m_bHurtFlying = false;
            m_spriteRenderer.color = new Color(1, 1, 1, 1);
            //m_rigidBody.velocity = Vector2.zero;

            m_rigidBody.velocity = new Vector2(0,m_rigidBody.velocity.y);

            m_rigidBody.gravityScale = m_prevGravityScale;
        }
    }
}

