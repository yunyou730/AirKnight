using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesStateJump1 : BaseState<AriesEntity>
    {
        AriesJump m_jumpComp = null;
        Rigidbody2D m_rigid = null;
        EnvironmentDetector m_envDetector = null;

        public AriesStateJump1(AriesEntity entity):base(entity)
        {
            m_jumpComp = entity.GetAgent().GetComponent<AriesJump>();
            m_rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            m_envDetector = entity.GetAgent().GetComponent<EnvironmentDetector>();
        }        

        public override void OnEnter(AriesEntity entity)
        {
            base.OnEnter(entity);


            m_jumpComp.ResetForJump1();
            m_rigid.velocity = new Vector2(m_rigid.velocity.x,0);// give a min velocity of y
        }

        public override void OnExit(AriesEntity entity)
        {

        }

        public override void Update(AriesEntity entity,float dt)
        {
            AriesController ctrl = entity.GetAgent().GetComponent<AriesController>();
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            EnvironmentDetector envDector = entity.GetAgent().GetComponent<EnvironmentDetector>();

            ctrl.UpdateHorizontalMove();

            Vector2 curVelocity = rigid.velocity;

            if(ctrl.m_jumpButton.IsPress() && m_jumpComp.CheckJumpChance())
            {
                entity.ChangeState(AriesState.Jump2);
            }
        }

        public override void FixedUpdate(AriesEntity entity, float dt)
        {
            AriesController ctrl = entity.GetAgent().GetComponent<AriesController>();
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            EnvironmentDetector envDector = entity.GetAgent().GetComponent<EnvironmentDetector>();

            Vector2 curVelocity = rigid.velocity;

            if (ctrl.m_jumpButton.IsRelease())
            {
                m_jumpComp.JumpBtnReleased();
            }
            else
            {
                if(
                    (ctrl.m_jumpButton.IsPress() || ctrl.m_jumpButton.IsHold())
                    && m_jumpComp.GetLeftAvailableHoldDurationForJump1() > 0
                )
                {
                    m_jumpComp.UpdateJump(dt);
                }
            }

            if(curVelocity.y < 0)
            {
                entity.ChangeState(AriesState.Fall);
            }
        }

        public override bool HandleMessage(AriesEntity entity,Telegram msg)
        {
            bool bHandled = true;
            switch(msg.m_msgType)
            {
                case MessageType.MT_TryDash:
                {
                    AriesJump ariesJumpComp = entity.GetAgent().GetComponent<AriesJump>();
                    if(ariesJumpComp.GetDashCount() == 0)
                    {
                        ariesJumpComp.AddDashCount();
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
    }

}
