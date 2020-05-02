using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesStateDash : BaseState<AriesEntity>
    {
        private float m_enterGravityScale = 1.0f;

        private float m_leftTime = 0;

        public AriesStateDash(AriesEntity entity):base(entity)
        {
            
        }

        public override void OnEnter(AriesEntity entity)
        {
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            AriesDash dash = entity.GetAgent().GetComponent<AriesDash>();
            Animator animator = entity.GetAgent().GetComponent<Animator>();
            AriesAnimBridge bridge = entity.GetAgent().GetComponent<AriesAnimBridge>();
            m_enterGravityScale = rigid.gravityScale;
            m_leftTime = dash.m_dashKeepTime;
            animator.SetTrigger(bridge.dashTrigger);
        }

        public override void OnExit(AriesEntity entity)
        {
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            Animator animator = entity.GetAgent().GetComponent<Animator>();
            AriesAnimBridge bridge = entity.GetAgent().GetComponent<AriesAnimBridge>();
            rigid.gravityScale = m_enterGravityScale; 
            animator.SetTrigger(bridge.dashRecoverTrigger);
        }

        public override void Update(AriesEntity entity,float dt)
        {
            
        }

        public override void FixedUpdate(AriesEntity entity,float dt)
        {
            AriesDash dash = entity.GetAgent().GetComponent<AriesDash>();
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            AriesController ctrl = entity.GetAgent().GetComponent<AriesController>();

            Vector3 dir = ctrl.GetFront();
            rigid.velocity = dir * dash.m_dashSpeed;
            
            m_leftTime -= dt;
            if(m_leftTime <= 0)
            {
                entity.ChangeState(AriesState.Idle);
            }
        } 

        public override bool HandleMessage(AriesEntity entity,Telegram msg)
        {
            return false;
        }        
    }
}

