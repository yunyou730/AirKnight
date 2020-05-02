using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class StateMachine<T> where T:BaseEntity
    {
        private T m_owner = null;

        private BaseState<T>    m_currentState = null;
        private BaseState<T>    m_globalState = null;
        private BaseState<T>    m_prevState = null;

        public void SetOwner(T entity)
        {
            m_owner = entity;
        }

        public void Update(float dt)
        { 
            if(m_globalState != null)
            {
                m_globalState.Update(m_owner,dt);
            }
            if(m_currentState != null)
            {
                m_currentState.Update(m_owner,dt);
            }
        }

        public void FixedUpdate(float dt)
        {
            if(m_globalState != null)
            {
                m_globalState.FixedUpdate(m_owner,dt);
            }
            if(m_currentState != null)
            {
                m_currentState.FixedUpdate(m_owner,dt);
            }
        }

        public void FlipState()
        {
            if (m_prevState != null)
            {
                ChangeState(m_prevState,null);
            }
        }

        public void SetCurrenState(BaseState<T> state)
        {
            m_currentState = state;
        }

        public void SetGlobalState(BaseState<T> state)
        {
            m_globalState = state;
        }

        public void ChangeState(BaseState<T> nextState,Telegram msg)
        {
            if(m_currentState != null && nextState == m_currentState)
            {
                return;
            }
            if (m_currentState != null)
            {
                m_currentState.OnExit(m_owner);
                m_prevState = m_currentState;
            }
            m_currentState = nextState;
            m_currentState.OnEnter(m_owner,msg);
        }

        public bool HandleMessage(Telegram msg)
        {
            if (m_currentState != null && m_currentState.HandleMessage(m_owner,msg))
            {
                return true;
            }
            if (m_globalState != null && m_globalState.HandleMessage(m_owner,msg))
            {
                return true;
            }
            return false;
        }


        public string GetCurrentStateName()
        {
            if(m_currentState != null)
            {
                return m_currentState.GetType().Name;
            }
            return "[None]";
        }

        public string GetGlobalStateName()
        {
            if(m_globalState != null)
            {
                return m_globalState.GetType().Name;
            }            
            return "[None]";
        }
    }
}
