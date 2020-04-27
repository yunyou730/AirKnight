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
            m_entity.SetAgent(this);
            m_entityId = m_entity.GetID();
        }

        void Update()
        {
            m_entity.Update(Time.deltaTime);
        }

        void FixedUpdate()
        {
            m_entity.FixedUpdate(Time.fixedDeltaTime);
        }

    }
}
