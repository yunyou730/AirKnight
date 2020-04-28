using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesStateJump1 : BaseState<AriesEntity>
    {
        private static AriesStateJump1 s_instance = null;

        public static AriesStateJump1 Instance()
        {
            if(s_instance == null)
            {
                s_instance = new AriesStateJump1();
            }
            return s_instance;
        }

        public override void OnEnter(AriesEntity entity)
        {
            AriesJump jumpComp = entity.GetAgent().GetComponent<AriesJump>();
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            jumpComp.ResetForJump1();
            rigid.velocity = new Vector2(rigid.velocity.x,0);// give a min velocity of y
        }

        public override void OnExit(AriesEntity entity)
        {
            
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
                jumpComp.m_bHasRaised = true;
            }

            if (ctrl.m_jumpButton.IsRelease())
            {
                jumpComp.JumpBtnReleasedForJump1();
            }
            else
            {
                if(curVelocity.y >= 0)
                {
                    if(
                        (ctrl.m_jumpButton.IsPress() || ctrl.m_jumpButton.IsHold())
                        && jumpComp.GetLeftAvailableHoldDurationForJump1() > 0
                    )
                    {
                        jumpComp.UpdateJump(dt);
                    }
                }
            }   

            // Debug.LogWarning("[JumpState:Update()][curVelocity.y] " + curVelocity.y);
            if(jumpComp.m_bHasRaised)
            {
                if(curVelocity.y <= 0 && envDector.isOnGround)
                {
                    entity.GetFSM().ChangeState(AriesStateIdle.Instance());
                }
            }
        }


        private void ApplyJump(float dt)
        {

        }
    }

}
