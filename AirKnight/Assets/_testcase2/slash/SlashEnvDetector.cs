using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SlashEnvDetector : MonoBehaviour
    {
        private GameObject m_owner = null;
        private BoxCollider2D m_collider = null;

        private void Awake()
        {
            m_collider = GetComponent<BoxCollider2D>();
        }

        void Start()
        {

        }

        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject != m_owner)
            {
                CanBeBounceAway canBeBounceAway = m_owner.GetComponent<CanBeBounceAway>();
                if (canBeBounceAway != null)
                {
                    canBeBounceAway.DoBounce(gameObject);
                }
            }
            



        }

        public void InitWithOwner(GameObject owner)
        {
            m_owner = owner;
        }
    }

}
