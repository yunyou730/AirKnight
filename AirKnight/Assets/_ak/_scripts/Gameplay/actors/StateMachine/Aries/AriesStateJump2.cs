using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesStateJump2 : BaseState<AriesEntity>
    {
        private static AriesStateJump2 s_instance = null;

        public static AriesStateJump2 Instance()
        {
            if(s_instance == null)
            {
                s_instance = new AriesStateJump2();
            }
            return s_instance;
        }

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
        }

        public override void Update(AriesEntity entity,float dt)
        {
            AriesController ctrl = entity.GetAgent().GetComponent<AriesController>();
            ctrl.UpdateHorizontalMove();
        }

        public override void FixedUpdate(AriesEntity entity, float dt)
        {
            AriesController ctrl = entity.GetAgent().GetComponent<AriesController>();
            AriesJump jumpComp = entity.GetAgent().GetComponent<AriesJump>();
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            EnvironmentDetector envDector = entity.GetAgent().GetComponent<EnvironmentDetector>();



            Vector2 curVelocity = rigid.velocity;

            if(curVelocity.y > 0)
            {
                jumpComp.SetHasRaisedFlag();
            }

            if(
                (ctrl.m_jumpButton.IsPress() || ctrl.m_jumpButton.IsHold())
                && jumpComp.GetLeftAvailableHoldDurationForJump2() > 0
            )
            {
                jumpComp.UpdateJump2(dt);
            }
            
            if(jumpComp.HasRaised())
            {
                // if(curVelocity.y <= 0 && envDector.isOnGround)
                // {
                if(envDector.isOnGround)
                {
                    entity.GetFSM().ChangeState(AriesStateIdle.Instance());
                }
            }
        }

    }

}
