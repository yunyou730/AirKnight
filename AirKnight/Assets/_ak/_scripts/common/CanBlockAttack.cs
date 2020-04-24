using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class CanBlockAttack : MonoBehaviour
    {
        private CanBeHit m_owner = null;

        void Start()
        {

        }

        void Update()
        {

        }

        bool CheckOwner()
        {
            return m_owner != null;
        }

        public void SetOwner(CanBeHit other)
        {
            m_owner = other;
        }

        public CanBeHit GetOwner()
        {
            return m_owner;
        }
    }
}
