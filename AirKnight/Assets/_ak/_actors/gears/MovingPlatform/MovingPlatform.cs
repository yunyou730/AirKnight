using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    enum DirType
    {
        StartToEnd,
        EndToStart,
    }
    private DirType m_dirType = DirType.StartToEnd;
    private Vector3 m_startPos,m_endPos;

    public float m_speed = 1.0f;
    
    void Awake()
    {
        Transform startTransform = transform.Find("start");
        Transform endTransform = transform.Find("end");
        m_startPos = startTransform.position;
        m_endPos = endTransform.position;
        Destroy(startTransform.gameObject);
        Destroy(endTransform.gameObject);

        transform.position = m_startPos;
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        switch(m_dirType)
        {
            case DirType.StartToEnd:
                {
                    Vector3 delta = (m_endPos - m_startPos).normalized * m_speed * dt;
                    if(delta.magnitude >= (m_endPos - transform.position).magnitude)
                    {
                        delta = (m_endPos - m_startPos).normalized * (m_endPos - transform.position).magnitude;
                        m_dirType = DirType.EndToStart;
                    }
                    transform.Translate(delta);
                }
                break;
            case DirType.EndToStart:
                {
                    Vector3 delta = (m_startPos - m_endPos).normalized * m_speed * dt;
                    if(delta.magnitude >= (m_startPos - transform.position).magnitude)
                    {
                        delta = (m_startPos - m_endPos).normalized * (m_startPos - transform.position).magnitude;
                        m_dirType = DirType.StartToEnd;
                    }
                    transform.Translate(delta);   
                }
                break;
        }
    }


    void OnCollisionEnter2D(Collision2D collison)
    {
        Debug.Log("111");
    }

}
