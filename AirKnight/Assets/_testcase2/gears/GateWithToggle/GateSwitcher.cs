using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSwitcher : MonoBehaviour
{
    Gate    m_gate = null;

    void Awake()
    {
        m_gate = transform.parent.GetComponent<Gate>();
        m_gate.RegisterSwitch(this);
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        m_gate.TurnOn();
    }
}
