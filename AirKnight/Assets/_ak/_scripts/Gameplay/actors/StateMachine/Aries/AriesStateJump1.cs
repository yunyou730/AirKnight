using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesStateJump1 : BaseState<AriesEntity>
    {

        public override void OnEnter(AriesEntity entity)
        {
            base.OnEnter(entity);

            AriesJump jumpComp = entity.GetAgent().GetComponent<AriesJump>();
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            EnvironmentDetector envDetector = entity.GetAgent().GetComponent<EnvironmentDetector>();

            jumpComp.ResetForJump1();
            rigid.velocity = new Vector2(rigid.velocity.x,0);// give a min velocity of y

            jumpComp.jumpPhase = AriesJump.Phase.Jump1;
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
            if(ctrl.m_jumpButton.IsPress())
            {
                entity.ChangeState(AriesState.Jump2);
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
    }

}
