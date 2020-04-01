using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    [RequireComponent(typeof(AriesAnimBridge),typeof(Animator))]
    public class AriesController : MonoBehaviour
    {
        Animator m_animator = null;
        AriesAnimBridge m_animBridge = null;

        public bool isMidAir = false;
        

        private ff.InputAxe m_horizontalAxe = null;
        private ff.InputButton m_jumpButton = null;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_animBridge = GetComponent<AriesAnimBridge>();

            m_horizontalAxe = new ff.InputAxe("Horizontal");
            m_jumpButton = new ff.InputButton("Jump");
        }

        void Start()
        {

        }

        void Update()
        {
            float dt = Time.deltaTime;
            CollectInput(dt);
        }

        private void CollectInput(float dt)
        {
            m_horizontalAxe.Update(dt);
            m_animator.SetFloat(m_animBridge.horizonAxeHoldTime, m_horizontalAxe.m_holdDuration);
            m_animator.SetBool(m_animBridge.isHorizonAxeHold,m_horizontalAxe.IsHolding());

            m_jumpButton.Update(dt);
        }
    }

}
