using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesStateAgent : MonoBehaviour
    {
        int m_entityId = 0;
        AriesEntity m_entity = null;

        void Awake()
        {
            m_entity = EntityManager.GetInstance().CreateEntity<AriesEntity>();
            m_entityId = m_entity.GetID();
        }

    }
}
