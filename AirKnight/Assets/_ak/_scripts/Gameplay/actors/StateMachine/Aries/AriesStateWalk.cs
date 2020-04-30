using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesStateWalk : BaseState<AriesEntity>
    {
        private static AriesStateWalk s_instance = null;

        public static AriesStateWalk Instance()
        {
            if (s_instance == null)
            {
                s_instance = new AriesStateWalk();
            }
            return s_instance;
        }

        public override void Update(AriesEntity entity, float dt)
        {
            AriesController ctrl = entity.GetAgent().GetComponent<AriesController>();
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            ctrl.UpdateHorizontalMove();

            if (Mathf.Abs(rigid.velocity.x) == 0)
            {
                entity.GetFSM().ChangeState(AriesStateIdle.Instance());
            }
            if (ctrl.m_jumpButton.IsPress())
            {
                entity.GetFSM().ChangeState(AriesStateJump1.Instance());
            }
        }

        public override void FixedUpdate(AriesEntity entity, float dt)
        {
            EnvironmentDetector envDetector = entity.GetAgent().GetComponent<EnvironmentDetector>();
            if (!envDetector.isOnGround)
            {
                entity.GetFSM().ChangeState(AriesStateJump1.Instance());
            }
        }
    }
}
