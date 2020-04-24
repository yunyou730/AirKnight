using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class FireTrap : MonoBehaviour
    {
        public float m_fireFrequecy = 2.0f;
        public float m_fireKeep = 3.0f;
        Animator    m_animator = null;
        int m_fireTrigger;
        int m_stopTrigger;

        void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_fireTrigger = Animator.StringToHash("fire_trigger");
            m_stopTrigger = Animator.StringToHash("stop_trigger");
        }
        
        void Start()
        {
            StartCoroutine(PrepareNextFire());
        }
        
        private IEnumerator PrepareNextFire()
        {
            yield return new WaitForSeconds(m_fireFrequecy);
            m_animator.SetTrigger(m_fireTrigger);
            StartCoroutine(StopFireAndPrepareNext());
        }

        public IEnumerator StopFireAndPrepareNext()
        {
            yield return new WaitForSeconds(m_fireKeep);
            m_animator.SetTrigger(m_stopTrigger);
            StartCoroutine(PrepareNextFire());
        }
    }
}
