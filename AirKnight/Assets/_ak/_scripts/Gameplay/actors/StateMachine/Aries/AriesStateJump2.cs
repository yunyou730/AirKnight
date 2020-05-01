using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesStateJump2 : BaseState<AriesEntity>
    {
        public override void OnEnter(AriesEntity entity)
        {
            base.OnEnter(entity);
            AriesJump jumpComp = entity.GetAgent().GetComponent<AriesJump>();
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            jumpComp.ResetForJump2();
            if(rigid.velocity.y < 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x,0);// give a min velocity of y
            }
            jumpComp.jumpPhase = AriesJump.Phase.Jump2;
        }

        public override void Update(AriesEntity entity,float dt)
        {
            AriesController ctrl = entity.GetAgent().GetComponent<AriesController>();
            AriesJump jumpComp = entity.GetAgent().GetComponent<AriesJump>();
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            EnvironmentDetector envDector = entity.GetAgent().GetComponent<EnvironmentDetector>();

            ctrl.UpdateHorizontalMove();

            if(ctrl.m_jumpButton.IsHold()
                && jumpComp.GetLeftAvailableHoldDurationForJump2() > 0
                && !jumpComp.HasJumpBtnReleased())
            {
                jumpComp.UpdateJump2(dt);
            }
        }

        public override void FixedUpdate(AriesEntity entity, float dt)
        {
            AriesController ctrl = entity.GetAgent().GetComponent<AriesController>();
            AriesJump jumpComp = entity.GetAgent().GetComponent<AriesJump>();
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            EnvironmentDetector envDector = entity.GetAgent().GetComponent<EnvironmentDetector>();
            
            Vector2 curVelocity = rigid.velocity;
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
