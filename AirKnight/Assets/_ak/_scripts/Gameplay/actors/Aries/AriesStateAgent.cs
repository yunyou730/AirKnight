using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ff
{
    public class AriesStateAgent : MonoBehaviour
    {
        AriesEntity m_entity = null;
        public Text   m_stateLabel = null;

        // for debug ,display state name in Inspector
        [Header("debug")]
        public string m_currentStateName = "";
        public string m_globalStateName = "";

        void Awake()
        {
            
        }

        void Start()
        {
            m_entity = EntityManager.GetInstance().CreateEntity<AriesEntity>();
            m_entity.InitAgent(this);
        }

        void Update()
        {
            m_entity.Update(Time.deltaTime);
            m_currentStateName = m_entity.GetFSM().GetCurrentStateName();
            m_globalStateName = m_entity.GetFSM().GetGlobalStateName();
            if(m_stateLabel != null)
            {
                m_stateLabel.text = m_currentStateName;
            }
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
