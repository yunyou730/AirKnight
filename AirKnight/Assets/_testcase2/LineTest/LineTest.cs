using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTest : MonoBehaviour
{
    public GameObject m_followTarget = null;
    LineRenderer m_lineRenderer = null;

    public float m_pointPeriod = 3.0f;
    public float m_spawnSpan = 0.3f;
    private float m_timeCounter = 0;

    class PointRecord
    {
        public Vector3 pos { set; get; }
        float period;
        bool bDead = false;

        public PointRecord(Vector3 pos,float period)
        {
            this.pos = pos;
            this.period = period;
        }

        public void ElapseTime(float dt)
        {
            if (bDead)
            {
                return;
            }
            period -= dt;
            if (period <= 0)
            {
                bDead = true;
            }
        }

        public bool IsDead()
        {
            return bDead;
        }
    }

    List<PointRecord> m_points = new List<PointRecord>();

    private void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_lineRenderer.positionCount = 0;
    }

    public void Update()
    {
        float dt = Time.deltaTime;

        m_timeCounter += dt;
        if (m_timeCounter >= m_spawnSpan)
        {
            m_timeCounter -= m_spawnSpan;
            SpawnPoint();
        }
        CheckDeadPoint(dt);
        ApplyPoints();
    }

    private void SpawnPoint()
    {
        PointRecord point = new PointRecord(m_followTarget.transform.position, m_pointPeriod);
        m_points.Add(point);
    }

    private void CheckDeadPoint(float dt)
    {
        List<PointRecord> toRemoveList = new List<PointRecord>();
        foreach (PointRecord record in m_points)
        {
            record.ElapseTime(dt);
            if (record.IsDead())
            {
                toRemoveList.Add(record);
            }
        }
        foreach (PointRecord record in toRemoveList)
        {
            m_points.Remove(record);
        }
    }

    private void ApplyPoints()
    {
        m_lineRenderer.positionCount = m_points.Count;
        m_lineRenderer.startWidth = 0;
        m_lineRenderer.endWidth = 0.5f;
        for (int i = 0;i < m_lineRenderer.positionCount;i++)
        {
            PointRecord record = m_points[i];
            m_lineRenderer.SetPosition(i, record.pos);
        }
    }
}
