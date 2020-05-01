using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{

    public class AriesStateFall : BaseState<AriesEntity>
    {
        public override void OnEnter(AriesEntity entity)
        {
            base.OnEnter(entity);
            EnvironmentDetector envDetector = entity.GetAgent().GetComponent<EnvironmentDetector>();
        }

        public override void OnExit(AriesEntity entity)
        {
            AriesJump jumpComp = entity.GetAgent().GetComponent<AriesJump>();
            jumpComp.jumpPhase = AriesJump.Phase.None;
        }

        public override void Update(AriesEntity entity,float dt)
        {
            AriesController ctrl = entity.GetAgent().GetComponent<AriesController>();
            AriesJump jumpComp = entity.GetAgent().GetComponent<AriesJump>();

            ctrl.UpdateHorizontalMove();
            if(jumpComp.jumpPhase == AriesJump.Phase.Jump1 && ctrl.m_jumpButton.IsPress())
            {
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
