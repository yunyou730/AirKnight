using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    Renderer  m_renderer = null;
    Collider2D  m_collider = null;

    bool    m_hasTurnOn = false;

    List<GateSwitcher>  m_switchList = new List<GateSwitcher>();
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_collider = GetComponent<BoxCollider2D>();
    }

    public void TurnOn()
    {
        if(m_hasTurnOn)
        {
            return;
        }
        m_hasTurnOn = true;
        TurnOnGate();
        PlayOpenAnim();
    }


    private void TurnOnGate()
    {
        m_collider.enabled = false;
        foreach(GateSwitcher switcher in m_switchList)
        {
            switcher.gameObject.SetActive(false);
        }
    }

    private void PlayOpenAnim()
    {
        m_renderer.enabled = false;
    }  


    public void  RegisterSwitch(GateSwitcher switcher)
    {
        m_switchList.Add(switcher);
    }  
}
