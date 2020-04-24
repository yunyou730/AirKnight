using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ff
{
	public class AriesBeBounceAway : CanBeBounceAway
	{
        Rigidbody2D m_rigid = null;
        
        [SerializeField]
        private float m_forceValue = 50.0f;

        void Awake()
        {
            m_rigid = GetComponent<Rigidbody2D>();
        }

        public override void OnBeBounced(GameObject other)
        {
            Debug.Log("[CanBeBounceAway][OnBeBounced]");
            Vector2 dir = new Vector2(1,0);
            if(other.transform.position.x > transform.position.x)
            {
                dir.x = -1;
            }
            m_rigid.AddForce(dir * m_forceValue,ForceMode2D.Impulse);
        }
	}	
}
