using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class InputButton
    {
        public bool m_isPress = false;
        public bool m_isHold = false;
        public bool m_isRelease = false;

        string m_buttonKey;


        public InputButton(string buttonKey)
        {
            m_buttonKey = buttonKey;
        }

        public void Update(float dt)
        {
            // clear & reset value
            m_isPress = Input.GetButtonDown(m_buttonKey);
            m_isHold = Input.GetButton(m_buttonKey);
            m_isRelease = Input.GetButtonUp(m_buttonKey);
        }

        public bool IsPress()
        {
            return m_isPress;
        }

        public bool IsHold()
        {
            return m_isHold;
        }

        public bool IsRelease()
        {
            return m_isRelease;
        }
    }

}
