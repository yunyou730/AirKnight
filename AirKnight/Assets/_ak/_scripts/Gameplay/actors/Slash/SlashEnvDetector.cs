﻿using System.Collections;
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

        /*
            攻击判定时，
            看是否同时接触了对方本体 ，以及对方的刀光
            如果同时接触了对方本体 和 刀光， 则认为 此次攻击是“拼刀”,
            不对对方本体造成伤害
        */
        public void OnJudgeHurtFrame()
        {
            // key: victim object id, value: victim CanBeHit component
            Dictionary<int, CanBeHit> hurtMap = new Dictionary<int, CanBeHit>();
            
            // key: be blocked victim id, value: block attack object CanBlockAttack component
            Dictionary<int, CanBlockAttack> blockMap = new Dictionary<int, CanBlockAttack>();

            // The first block attack object, determine which direction owner shall be bounced
            CanBlockAttack  firstBlockAttack = null;
            
            foreach (GameObject other in m_hitList)
            {
                CanBeHit cbh = other.GetComponent<CanBeHit>();
                if(cbh != null)
                {
                    hurtMap.Add(other.GetInstanceID(), cbh);
                    //DoLog("OnJudgeHurtFrame [hurtMap] " + other.GetInstanceID());
                }

                CanBlockAttack cba = other.GetComponent<CanBlockAttack>();
                if (cba != null && cba.GetOwner() != null)
                {
                    CanBeHit owner = cba.GetOwner();
                    int goInstanceId = owner.gameObject.GetInstanceID();
                    blockMap.Add(goInstanceId, cba);
                }

                if(cba != null && firstBlockAttack == null)
                {
                    firstBlockAttack = cba;
                }
            }

            foreach (int otherId in hurtMap.Keys)
            {
                if (blockMap.ContainsKey(otherId))
                {
                    GameObject other = blockMap[otherId].gameObject;
                    OnBlocked(other);
                }
                else
                {
                    DoHurtOther(hurtMap[otherId].gameObject);
                }
            }
            if(firstBlockAttack != null)
            {
                OnBounced(firstBlockAttack.gameObject);
            }
        }

        private void DoBounceOwner(GameObject other)
        {
            CanBeBounceAway canBeBounceAway = m_owner.GetComponent<CanBeBounceAway>();
            if (canBeBounceAway != null)
            {
                canBeBounceAway.OnBeBounced(gameObject);
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

        private void OnBlocked(GameObject other)
        {
            
        }

        private void OnBounced(GameObject other)
        {
            DoBounceOwner(other);
        }
    }
}
