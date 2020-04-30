using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesStateIdle : BaseState<AriesEntity>
    {
        private static AriesStateIdle s_instance = null;

        public static AriesStateIdle Instance()
        {
            if(s_instance == null)
            {
                s_instance = new AriesStateIdle();
            }
            return s_instance;
        }

        public override void OnEnter(AriesEntity entity)
        {
            base.OnEnter(entity);
        }

        public override void OnExit(AriesEntity entity)
        {
            base.OnExit(entity);
        }

        public override void Update(AriesEntity entity,float dt)
        {
            AriesController ctrl = entity.GetAgent().GetComponent<AriesController>();
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            ctrl.UpdateHorizontalMove();

            if (Mathf.Abs(rigid.velocity.x) > 0)
            {
                entity.GetFSM().ChangeState(AriesStateWalk.Instance());
            }
            if (ctrl.m_jumpButton.IsPress())
            {
                entity.GetFSM().ChangeState(AriesStateJump1.Instance());
            }
        }

        public override void FixedUpdate(AriesEntity entity, float dt)
        {
            EnvironmentDetector envDetector = entity.GetAgent().GetComponent<EnvironmentDetector>();
            if(!envDetector.isOnGround)
            {   
                entity.GetFSM().ChangeState(AriesStateJump1.Instance());
            }
        }
    }

}
