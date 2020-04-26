using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesStateAgent : MonoBehaviour
    {
        AriesEntity m_entity = null;
        StateMachine<AriesEntity> m_stateMachine = null;

        void Awake()
        {
            m_entity = new AriesEntity();
            m_stateMachine = new StateMachine<AriesEntity>();

            
        }

    }
}
