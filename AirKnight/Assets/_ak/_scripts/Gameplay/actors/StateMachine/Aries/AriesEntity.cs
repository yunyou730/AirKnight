using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesEntity : Entity
    {
        StateMachine<AriesEntity>   m_fsm = new StateMachine<AriesEntity>();

        AriesStateAgent m_agent = null;

        public AriesEntity()
        { 
            m_fsm.SetCurrenState(AriesStateIdle.Instance());
        }

        public void SetAgent(AriesStateAgent agent)
        {
            m_agent = agent;
        }

        public AriesStateAgent GetAgent()
        {
            return m_agent;
        }


        public void Update(float dt)
        {

        }

        public void FixedUpdate(float dt)
        {
            
        }
    }

}
