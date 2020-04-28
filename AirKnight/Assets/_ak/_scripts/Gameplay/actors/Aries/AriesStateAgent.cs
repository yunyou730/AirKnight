using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesStateAgent : MonoBehaviour
    {
        int m_entityId = 0;
        AriesEntity m_entity = null;

        // for debug ,display state name in Inspector
        [Header("debug")]
        public string m_currentStateName = "";
        public string m_globalStateName = "";

        void Awake()
        {
            m_entity = EntityManager.GetInstance().CreateEntity<AriesEntity>();
            m_entity.SetAgent(this);
            m_entityId = m_entity.GetID();
        }

        void Update()
        {
            m_entity.Update(Time.deltaTime);
            m_currentStateName = m_entity.GetFSM().GetCurrentStateName();
            m_globalStateName = m_entity.GetFSM().GetGlobalStateName();
        }

        void FixedUpdate()
        {
            m_entity.FixedUpdate(Time.fixedDeltaTime);
        }

        public int GetEntityID()
        {
            return m_entity.GetID();
        }
    }
}
