using UnityEngine;
using System.Collections;

namespace ff
{
    public class InputAxe
    {
        public float m_holdDuration = 0.0f;
        public float m_value = 0;
        public string m_axeKey;
        

        public InputAxe(string axeKey)
        {
            m_axeKey = axeKey;
        }
        
        public void Update(float dt)
        {
            m_value = Input.GetAxis(m_axeKey);
            if (Mathf.Abs(m_value) > 0)
            {
                m_holdDuration += dt;
            }
            else
            {
                m_holdDuration = dt;
            }
        }

        public bool IsHolding()
        {
            return Mathf.Abs(m_value) > 0;
        }
    }

}
