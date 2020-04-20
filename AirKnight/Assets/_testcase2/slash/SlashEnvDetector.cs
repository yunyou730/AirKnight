using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    [RequireComponent(typeof(Collider2D))]
    public class SlashEnvDetector : MonoBehaviour
    {
        private GameObject m_owner = null;
        private Collider2D m_collider = null;

        [SerializeField]
        LayerMask m_checkLayer;

        private void Awake()
        {
            m_collider = GetComponent<Collider2D>();
        }

        void Start()
        {

        }

        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //CheckBounceOwner(collision);
            CheckHurtOther(collision);
        }

        public void InitWithOwner(GameObject owner)
        {
            m_owner = owner;
        }

        private void CheckBounceOwner(Collider2D collision)
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

        private void CheckHurtOther(Collider2D collision)
        {
            if (collision.gameObject != m_owner)
            {
                CanBeHit canBeHit = collision.GetComponent<CanBeHit>();
                if (canBeHit != null)
                {
                    canBeHit.OnBeHit(m_owner);
                }
            }
        }

    }

}
