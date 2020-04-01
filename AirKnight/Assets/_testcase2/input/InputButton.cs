using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class InputButton
    {
        public bool isPress = false;
        public bool isHold = false;
        public bool isRelease = false;

        string m_buttonKey;

        public InputButton(string buttonKey)
        {
            m_buttonKey = buttonKey;
        }

        public void Update()
        {
            isPress = Input.GetButtonDown(m_buttonKey);
            isHold = Input.GetButton(m_buttonKey);
            isRelease = Input.GetButtonUp(m_buttonKey);
        }
    }

}
