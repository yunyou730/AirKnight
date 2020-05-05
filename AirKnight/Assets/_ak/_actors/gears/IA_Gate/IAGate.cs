using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ff
{
    public class IAGate : BeIA
    {
        public enum State
        {
            Closed,
            Opening,
            Opened,
            Closing,
        }

        public State   m_state = State.Closed;
        public float    m_openHeight = 2;
        public float    m_openSpeed = 1.0f;

        Vector3 m_closedPos,m_openedPos;


        void Awake()
        {
            m_closedPos = transform.position;
            m_openedPos = new Vector3(m_closedPos.x,m_closedPos.y + m_openHeight,m_closedPos.z);
        }


        public override void OnBeInteracted()
        {
            switch(m_state)
            {
                case State.Closed:
                    ChangeState(State.Opening);
                    break;
                case State.Opened:
                    ChangeState(State.Closing);                
                    break;
            }
        }

        void Update()
        {
            switch(m_state)
            {
                case State.Opening:
                    UpdateOpening(Time.deltaTime);
                    break;
                case State.Closing:
                    UpdateClosing(Time.deltaTime);
                    break;
            }
        }

        void UpdateOpening(float dt)
        {
            Vector3 dest = m_openedPos;
            Vector3 nextPos = transform.position + new Vector3(0,dt * m_openSpeed,0);
            if(nextPos.y > dest.y)
            {
                nextPos.y = dest.y;
                ChangeState(State.Opened);
            }
            transform.position = nextPos;
        }

        void UpdateClosing(float dt)
        {
            Vector3 dest = m_closedPos;
            Vector3 nextPos = transform.position - new Vector3(0,dt * m_openSpeed,0);
            if(nextPos.y < dest.y)
            {
                nextPos.y = dest.y;
                ChangeState(State.Closed);
            }
            transform.position = nextPos;
        }

        void ChangeState(State nextState)
        {
            m_state = nextState;
        }
    }
}
