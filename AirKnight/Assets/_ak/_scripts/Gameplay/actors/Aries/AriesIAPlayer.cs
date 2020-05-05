using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ff
{
    public class AriesIAPlayer : IAPlayer
    {
        AriesController m_ctrl = null;
        void Awake()
        {
            m_ctrl = GetComponent<AriesController>();
        }

        public override bool CheckIAButon()
        {
            return m_ctrl.m_interactButton.IsPress();
        }
    }

}
