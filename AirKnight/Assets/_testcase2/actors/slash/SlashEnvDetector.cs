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
        
        List<GameObject> m_hitList = new List<GameObject>();

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

        public void InitWithOwner(GameObject owner)
        {
            m_owner = owner;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == m_owner)
            {
                return;
            }
            if (collision.GetComponent<CanBeHit>() || collision.GetComponent<CanBlockAttack>())
            {
                GameObject other = collision.gameObject;
                if (m_hitList.IndexOf(other) == -1)
                {
                    m_hitList.Add(other);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            
        }

        public void OnJudgeHurtFrame()
        {
            // key: victim object id, value: victim CanBeHit component
            Dictionary<int, CanBeHit> hurtMap = new Dictionary<int, CanBeHit>();

            // key: be blocked victim id, value: block attack object CanBlockAttack component
            Dictionary<int, CanBlockAttack> blockMap = new Dictionary<int, CanBlockAttack>();
            
            foreach (GameObject other in m_hitList)
            {
                CanBeHit cbh = other.GetComponent<CanBeHit>();
                if(cbh != null)
                {
                    hurtMap.Add(other.GetInstanceID(), cbh);
                }

                CanBlockAttack cba = other.GetComponent<CanBlockAttack>();
                if (cba != null && cba.GetOwner() != null)
                {
                    CanBeHit owner = cba.GetOwner();
                    blockMap.Add(owner.GetInstanceID(),cba);
                }
            }

            foreach (int otherId in hurtMap.Keys)
            {
                if (blockMap.ContainsKey(otherId))
                {
                    OnBlocked();
                }
                else
                {
                    DoHurtOther(hurtMap[otherId].gameObject);
                }
            }
        }

        private void DoBounceOwner(GameObject other)
        {
            CanBeBounceAway canBeBounceAway = m_owner.GetComponent<CanBeBounceAway>();
            if (canBeBounceAway != null)
            {
                canBeBounceAway.DoBounce(gameObject);
            }
        }

        private void DoHurtOther(GameObject other)
        {
            CanBeHit canBeHit = other.GetComponent<CanBeHit>();
            if (canBeHit != null)
            {
                canBeHit.OnBeHit(m_owner);
            }
        }

        private void OnBlocked()
        {
            Debug.LogWarning("[OnBlocked] ---- ");
        }

    }

}
