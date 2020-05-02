using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ff
{
    public class AriesBeHit : CanBeHit
    {   
        private ff.AriesStateAgent m_stateAgent = null;
        [SerializeField]
        public float m_hitAwaySpeed = 15;
        [SerializeField]
        public float m_angleFactor = 1f;
        [SerializeField]
        public float m_maxHurtPeriod = 0.1f;    

        private void Awake()
        {
            m_stateAgent = GetComponent<AriesStateAgent>();
        }
        
        public override void OnBeHit(GameObject caster)
        {
            TakeDamageExtraInfo info = new TakeDamageExtraInfo();
            info.caster = caster;
            MessageDispatcher.Instance().Dispatch(
                m_stateAgent.GetEntityID(),
                m_stateAgent.GetEntityID(),
                MessageType.MT_TakeDamage,
                info);
        }
    }
}
