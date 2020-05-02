using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesStateJump1 : BaseState<AriesEntity>
    {
        bool m_bHasHandleEnterFrame = false;

        public AriesStateJump1(AriesEntity entity):base(entity)
        {
            
        }        

        public override void OnEnter(AriesEntity entity)
        {
            base.OnEnter(entity);

            AriesJump jumpComp = entity.GetAgent().GetComponent<AriesJump>();
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            EnvironmentDetector envDetector = entity.GetAgent().GetComponent<EnvironmentDetector>();

            jumpComp.ResetForJump1();
            rigid.velocity = new Vector2(rigid.velocity.x,0);// give a min velocity of y

            // jumpComp.jumpPhase = AriesJump.Phase.Jump1;

            m_bHasHandleEnterFrame = false;
        }

        public override void OnExit(AriesEntity entity)
        {

        }

        public override void Update(AriesEntity entity,float dt)
        {


            AriesController ctrl = entity.GetAgent().GetComponent<AriesController>();
            AriesJump jumpComp = entity.GetAgent().GetComponent<AriesJump>();
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            EnvironmentDetector envDector = entity.GetAgent().GetComponent<EnvironmentDetector>();

            ctrl.UpdateHorizontalMove();

            Vector2 curVelocity = rigid.velocity;
            if(m_bHasHandleEnterFrame && ctrl.m_jumpButton.IsPress() && jumpComp.CheckJumpChance())
            {
                entity.ChangeState(AriesState.Jump2);
            }

            if(!m_bHasHandleEnterFrame)
            {
                m_bHasHandleEnterFrame = true;
            }            
        }

        public override void FixedUpdate(AriesEntity entity, float dt)
        {
            AriesController ctrl = entity.GetAgent().GetComponent<AriesController>();
            AriesJump jumpComp = entity.GetAgent().GetComponent<AriesJump>();
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            EnvironmentDetector envDector = entity.GetAgent().GetComponent<EnvironmentDetector>();

            Vector2 curVelocity = rigid.velocity;

            if (ctrl.m_jumpButton.IsRelease())
            {
                jumpComp.JumpBtnReleased();
            }
            else
            {
                if(
                    (ctrl.m_jumpButton.IsPress() || ctrl.m_jumpButton.IsHold())
                    && jumpComp.GetLeftAvailableHoldDurationForJump1() > 0
                )
                {
                    jumpComp.UpdateJump(dt);
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
