using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeChain : MonoBehaviour
{
    LineRenderer m_lineRender = null;
    Transform   m_ballTransform = null;
    Material m_lineMaterial = null;
    SpriteRenderer m_spriteRender = null;

    bool m_hasSetTiling = false;

    void Awake()
    {
        m_lineRender = GetComponent<LineRenderer>();
        m_lineMaterial = m_lineRender.material;
        m_ballTransform = transform.Find("Spiked Ball");
        m_spriteRender = GetComponent<SpriteRenderer>();

        Debug.Log("[sr size]" + m_spriteRender.size);
        Debug.Log("[ball sr size]" + m_ballTransform.GetComponent<SpriteRenderer>().size);
        InitLine();
    }

    void Update()
    {
        UpdateLinePoints();
    }

    private void InitLine()
    {
        m_lineRender.positionCount = 2;



    //pre.GetComponent<Renderer>().material.SetTextureScale("_MainTex",new Vector2(tiling_X,tiling_Y));


    }

    private void UpdateLinePoints()
    {
        Vector3 fromPos = new Vector3(0,0,-1);
        Vector3 toPos = m_ballTransform.position - transform.position;
        toPos.z = -1;
        m_lineRender.SetPosition(0,fromPos);
        m_lineRender.SetPosition(1,toPos);  

        if(!m_hasSetTiling)
        {
            m_hasSetTiling = true;
            float lineLength = (toPos - fromPos).magnitude;
            int ringsCount = (int)Mathf.Ceil(lineLength / m_spriteRender.size.y);
            m_lineMaterial.SetTextureScale("_MainTex",new Vector2(ringsCount,1));
        }
    }
}
