using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesJump : MonoBehaviour
    {
        public enum Phase
        {
            None,
            Jump1,
            Jump2,
        }

        public Phase jumpPhase = Phase.None;

        private ff.AriesController m_ctrl = null;
        private ff.PhysicBridge m_phyBridge = null;
        private ff.EnvironmentDetector m_envDetector = null;
        private Rigidbody2D m_rigidBody = null;
    
        [SerializeField]
        public float m_continouslyJumpMaxPeriod = 0.18f;
        private float m_jumpElapsedPeriod = 0.0f;

        [SerializeField]
        public float m_continouselyJump2MaxPeriod = 0.18f;
        private float m_jump2ElapsedPeriod = 0.0f;


        [SerializeField]
        public float m_jump1Value = 110;
        [SerializeField]
        public float m_jump2Value = 110;

        bool m_bHasReleaseJumpBtn = false;


        int m_hasDashCount = 0;

        private void Awake()
        {
            m_ctrl = GetComponent<ff.AriesController>();
            m_phyBridge = GetComponent<ff.PhysicBridge>();
            m_envDetector = GetComponent<ff.EnvironmentDetector>();
            m_rigidBody = GetComponent<Rigidbody2D>();
        }        

        public void ResetForJump1()
        {
            m_jumpElapsedPeriod = 0;
            m_bHasReleaseJumpBtn = false;
            m_hasDashCount = 0;
        }

        public void JumpBtnReleased()
        {
            m_bHasReleaseJumpBtn = true;
        }

        public float GetLeftAvailableHoldDurationForJump1()
        {
            if(m_jumpElapsedPeriod >= m_continouslyJumpMaxPeriod)
            {
                return 0;
            }
            return m_continouslyJumpMaxPeriod - m_jumpElapsedPeriod;
        }
        
        /// --------------------------------------------
        public void ResetForJump2()
        {
            m_jump2ElapsedPeriod = 0;
            m_bHasReleaseJumpBtn = false;
        }

        public float GetLeftAvailableHoldDurationForJump2()
        {
            if(m_jump2ElapsedPeriod >= m_continouselyJump2MaxPeriod)
            {
                return 0;
            }
            return m_continouselyJump2MaxPeriod - m_jump2ElapsedPeriod;
        }
        //  ------------------------------


        /// ---------------------------
        public void UpdateJump(float dt)
        {
            float calcJumpDeltaTime = dt;
            if (m_jumpElapsedPeriod + dt >= m_continouslyJumpMaxPeriod)
            {
                calcJumpDeltaTime = calcJumpDeltaTime - (m_jumpElapsedPeriod + dt - m_continouslyJumpMaxPeriod);
            }
            m_jumpElapsedPeriod += dt;
            m_phyBridge.PerformContinouslyJump(calcJumpDeltaTime, m_jump1Value);
        }


        public void UpdateJump2(float dt)
        {
            float calcJumpDeltaTime = dt;
            if (m_jump2ElapsedPeriod + dt >= m_continouselyJump2MaxPeriod)
            {
                calcJumpDeltaTime = calcJumpDeltaTime - (m_jump2ElapsedPeriod + dt - m_continouselyJump2MaxPeriod);
            }
            m_jump2ElapsedPeriod += dt;
            m_phyBridge.PerformContinouslyJump(calcJumpDeltaTime,m_jump2Value);
        }

        public bool HasJumpBtnReleased()
        {
            return m_bHasReleaseJumpBtn;
        }


        public void AddDashCount()
        {
            m_hasDashCount++;
        }

        public int GetDashCount()
        {
            return m_hasDashCount;
        }
    }

}
