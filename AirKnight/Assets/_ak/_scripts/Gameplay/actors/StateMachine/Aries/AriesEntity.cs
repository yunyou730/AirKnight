using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesEntity : BaseEntity
    {
        private StateMachine<AriesEntity>   m_fsm = null;

        AriesStateAgent m_agent = null;

        public AriesEntity()
        { 
            m_fsm = new StateMachine<AriesEntity>();
            m_fsm.SetOwner(this);
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
            m_fsm.Update(dt);
        }

        public void FixedUpdate(float dt)
        {
            m_fsm.FixedUpdate(dt);
        }

        public override bool HandleMessage(Telegram msg)
        {
            return m_fsm.HandleMessage(msg);
        }


        public StateMachine<AriesEntity> GetFSM()
        {
            return m_fsm;
        }
    }

}
