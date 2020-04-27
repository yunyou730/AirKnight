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

        }

    }

}
