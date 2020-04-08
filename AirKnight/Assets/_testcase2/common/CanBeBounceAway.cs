using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CanBeBounceAway : MonoBehaviour
    {
        [SerializeField]
        private float m_bounceAwayForce = 100;

        Rigidbody2D m_rigid = null;

        private void Awake()
        {
            m_rigid = GetComponent<Rigidbody2D>();
        }

        public void DoBounce(GameObject source)
        {
            Vector3 dir = transform.position - source.transform.position;
            dir.Normalize();
            m_rigid.AddForce(dir * m_bounceAwayForce,ForceMode2D.Impulse);
        }
    }
}
