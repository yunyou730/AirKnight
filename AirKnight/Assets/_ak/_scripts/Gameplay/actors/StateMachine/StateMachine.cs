using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class StateMachine<T> where T:Entity
    {
        private T m_owner = null;

        private BaseState<T>    m_currentState = null;
        private BaseState<T>    m_globalState = null;
        private BaseState<T>    m_prevState = null;

        public void SetOwner(T entity)
        {
            m_owner = entity;
        }

        public void Execute()
        { 
            
        }

        public void FlipState()
        {
        }

        public void ChangeState(BaseState<T> nextState)
        {
            if (m_currentState != null)
            {
                m_currentState.OnExit(m_owner);
                m_prevState = m_currentState;
            }
            m_currentState = nextState;
            m_currentState.OnEnter(m_owner);
        }
    }
}
