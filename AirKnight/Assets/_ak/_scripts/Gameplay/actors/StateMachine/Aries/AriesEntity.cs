using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{

    public enum AriesState
    { 
        Global,
        Idle,
        Walk,
        Jump1,
        Jump2,
        Fall,
        Dash,
        Hurt,

        None,
    }

    public class AriesEntity : BaseEntity
    {
        private StateMachine<AriesEntity>   m_fsm = null;
        Dictionary<AriesState, BaseState<AriesEntity>> m_stateMap = new Dictionary<AriesState, BaseState<AriesEntity>>();

        AriesStateAgent m_agent = null;

        public AriesEntity()
        {
            
        }

        public void InitAgent(AriesStateAgent agent)
        {
            m_agent = agent;

            m_stateMap.Add(AriesState.Global,new AriesStateGlobal(this));
            m_stateMap.Add(AriesState.Idle,new AriesStateIdle(this));
            m_stateMap.Add(AriesState.Walk, new AriesStateWalk(this));
            m_stateMap.Add(AriesState.Jump1, new AriesStateJump1(this));
            m_stateMap.Add(AriesState.Jump2, new AriesStateJump2(this));
            m_stateMap.Add(AriesState.Fall, new AriesStateFall(this));
            m_stateMap.Add(AriesState.Dash, new AriesStateDash(this));
            m_stateMap.Add(AriesState.Hurt, new AriesStateHurt(this));

            m_fsm = new StateMachine<AriesEntity>();
            m_fsm.SetOwner(this);
            m_fsm.SetCurrenState(GetState(AriesState.Idle));            
            m_fsm.SetGlobalState(GetState(AriesState.Global));
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

        public void ChangeState(AriesState state,Telegram msg = null)
        {
            BaseState<AriesEntity> nextState = GetState(state);
            GetFSM().ChangeState(nextState,msg);
        }
    }

}
