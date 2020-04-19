using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesJump : MonoBehaviour
    {
        private ff.AriesController m_ctrl = null;
        private ff.PhysicBridge m_phyBridge = null;
        private ff.EnvironmentDetector m_envDetector = null;

        [Header("Jump Factors")]
        [SerializeField]
        private float m_continouslyJumpMaxPeriod = 0.15f;
        [SerializeField]
        private float m_jumpElapsedPeriod = 0.0f;

        private void Awake()
        {
            m_ctrl = GetComponent<ff.AriesController>();
            m_phyBridge = GetComponent<ff.PhysicBridge>();
            m_envDetector = GetComponent<ff.EnvironmentDetector>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float dt = Time.deltaTime;
            if (m_ctrl.m_jumpButton.IsPress() && m_envDetector.isOnGround)
            {
                m_jumpElapsedPeriod = 0;
                UpdateJump(dt);
            }
            if (m_ctrl.m_jumpButton.IsHold() && m_jumpElapsedPeriod < m_continouslyJumpMaxPeriod)
            {
                UpdateJump(dt);
            }
        }

        private void UpdateJump(float dt)
        {
            float calcJumpDeltaTime = dt;
            if (m_jumpElapsedPeriod + dt >= m_continouslyJumpMaxPeriod)
            {
                calcJumpDeltaTime = calcJumpDeltaTime - (m_jumpElapsedPeriod + dt - m_continouslyJumpMaxPeriod);
            }
            m_jumpElapsedPeriod += dt;
            m_phyBridge.PerformContinouslyJump(calcJumpDeltaTime);
        }

    }

}
