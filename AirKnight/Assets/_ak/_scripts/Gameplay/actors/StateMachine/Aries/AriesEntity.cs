using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{

    public enum AriesState
    { 
        Idle,
        Walk,
        Jump1,
        Jump2,
        Fall,

        None,
    }

    public class AriesEntity : BaseEntity
    {
        private StateMachine<AriesEntity>   m_fsm = null;
        Dictionary<AriesState, BaseState<AriesEntity>> m_stateMap = new Dictionary<AriesState, BaseState<AriesEntity>>();

        AriesStateAgent m_agent = null;

        public AriesEntity()
        {
            m_stateMap.Add(AriesState.Idle,new AriesStateIdle());
            m_stateMap.Add(AriesState.Walk, new AriesStateWalk());
            m_stateMap.Add(AriesState.Jump1, new AriesStateJump1());
            m_stateMap.Add(AriesState.Jump2, new AriesStateJump2());
            m_stateMap.Add(AriesState.Fall, new AriesStateFall());

            m_fsm = new StateMachine<AriesEntity>();
            m_fsm.SetOwner(this);
            m_fsm.SetCurrenState(GetState(AriesState.Idle));
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

        public BaseState<AriesEntity> GetState(AriesState state)
        {
            if (m_stateMap.ContainsKey(state))
            {
                return m_stateMap[state];
            }
            return null;
        }

        public void ChangeState(AriesState state)
        {
            BaseState<AriesEntity> nextState = GetState(state);
            GetFSM().ChangeState(nextState);
        }
    }

}
