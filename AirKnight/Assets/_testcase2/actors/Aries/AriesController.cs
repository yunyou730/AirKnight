using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    [RequireComponent(typeof(AriesAnimBridge),typeof(Animator), typeof(ff.EnvironmentDetector))]
    public class AriesController : MonoBehaviour
    {
        Animator m_animator = null;
        AriesAnimBridge m_animBridge = null;
        
        private ff.InputAxe m_horizontalAxe = null;
        private ff.InputButton m_jumpButton = null;

        private ff.EnvironmentDetector m_envDetector = null;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_animBridge = GetComponent<ff.AriesAnimBridge>();
            m_envDetector = GetComponent<ff.EnvironmentDetector>();

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
            UpdateStateFlag();
        }

        private void FixedUpdate()
        {
            
        }

        private void CollectInput(float dt)
        {
            // horizon move
            m_horizontalAxe.Update(dt);
            m_animator.SetFloat(m_animBridge.horizonAxeHoldTime, m_horizontalAxe.m_holdDuration);
            m_animator.SetBool(m_animBridge.isHorizonAxeHold,m_horizontalAxe.IsHolding());

            // jump
            m_jumpButton.Update(dt);
            if (m_jumpButton.IsPress() && m_envDetector.isOnGround)
            {

                m_envDetector.PerformJump();
            }
        }


        private void UpdateStateFlag()
        {
            // air flag
            m_animator.SetBool(m_animBridge.isMidAir, !m_envDetector.isOnGround);

            // vertical speed
            m_animator.SetFloat(m_animBridge.airSpeed, m_envDetector.GetVerticalSpeed());
        }
    }

}
