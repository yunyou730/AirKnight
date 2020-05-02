using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{

    public class AriesStateFall : BaseState<AriesEntity>
    {
        EnvironmentDetector m_envDetector = null;
        AriesJump m_jumpComp = null;

        public AriesStateFall(AriesEntity entity):base(entity)
        {
            m_envDetector = entity.GetAgent().GetComponent<EnvironmentDetector>();
            m_jumpComp = entity.GetAgent().GetComponent<AriesJump>();            
        }

        public override void OnEnter(AriesEntity entity)
        {
            base.OnEnter(entity);
            
        }

        public override void OnExit(AriesEntity entity)
        {
            // m_jumpComp.jumpPhase = AriesJump.Phase.None;
        }

        public override void Update(AriesEntity entity,float dt)
        {
            AriesController ctrl = entity.GetAgent().GetComponent<AriesController>();
            AriesJump jumpComp = entity.GetAgent().GetComponent<AriesJump>();
            ctrl.UpdateHorizontalMove();
            if(ctrl.m_jumpButton.IsPress() && jumpComp.CheckJumpChance())
            {
                m_jumpComp.UpdateJump2(dt);
                entity.ChangeState(AriesState.Jump2);
            }
        }

        public override bool HandleMessage(AriesEntity entity,Telegram msg)
        {
            bool bHandled = true;
            switch(msg.m_msgType)
            {
                case MessageType.MT_TryDash:
                {
                    m_jumpComp = entity.GetAgent().GetComponent<AriesJump>();
                    if(m_jumpComp.GetDashCount() == 0)
                    {
                        m_jumpComp.AddDashCount();
                        entity.ChangeState(AriesState.Dash);
                    }
                    break;
                }
                default:
                    bHandled = false;
                    break;
            }
            return bHandled;
        }      
        
        public override void FixedUpdate(AriesEntity entity, float dt)
        {
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            EnvironmentDetector envDetector = entity.GetAgent().GetComponent<EnvironmentDetector>();
            if(envDetector.isOnGround)
            {
                entity.ChangeState(AriesState.Idle);
            }
        }
    }
}
