using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{

    public class AriesStateFall : BaseState<AriesEntity>
    {
        private static AriesStateFall s_instance = null;

        public static AriesStateFall Instance()
        {
            if(s_instance == null)
            {
                s_instance = new AriesStateFall();
            }
            return s_instance;
        }

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
                entity.GetFSM().ChangeState(AriesStateJump2.Instance());
            }
        }

        public override void FixedUpdate(AriesEntity entity, float dt)
        {
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            EnvironmentDetector envDetector = entity.GetAgent().GetComponent<EnvironmentDetector>();
            if(envDetector.isOnGround)
            {
                entity.GetFSM().ChangeState(AriesStateIdle.Instance());
            }
        }
    }
}
