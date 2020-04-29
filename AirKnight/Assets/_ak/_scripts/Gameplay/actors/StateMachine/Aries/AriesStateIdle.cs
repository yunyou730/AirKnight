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
            ctrl.UpdateHorizontalMove();
            //if (ctrl.m_jumpButton.IsPress() || ctrl.m_jumpButton.IsHold())
            if (ctrl.m_jumpButton.IsPress())
            {
                AriesJump jumpComp = entity.GetAgent().GetComponent<AriesJump>();
                jumpComp.UpdateJump(dt);
                entity.GetFSM().ChangeState(AriesStateJump1.Instance());
            }
        }

        public override void FixedUpdate(AriesEntity entity, float dt)
        {

        }
    }

}
