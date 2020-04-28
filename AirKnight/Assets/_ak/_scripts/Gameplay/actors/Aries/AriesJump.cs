using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesJump : MonoBehaviour
    {
        public enum JumpState
        {
            None,
            Jump1,
            Jump2,

            Invalid,
        }


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
        private JumpState m_jumpState = JumpState.Invalid;


        [SerializeField]
        public float m_jump1Value = 110;
        [SerializeField]
        public float m_jump2Value = 110;


        bool m_bHasReleaseJumpBtnInJump1 = false;
        //bool m_bHasReleaseJumpBtnInJump2 = false;

        public bool m_bHasRaised = false;

        public void ResetForJump1()
        {
            m_jumpElapsedPeriod = 0;
            m_bHasReleaseJumpBtnInJump1 = false;
            m_bHasRaised = false;
        }

        public void JumpBtnReleasedForJump1()
        {
            m_bHasReleaseJumpBtnInJump1 = true;
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

        
        private void Awake()
        {
            m_ctrl = GetComponent<ff.AriesController>();
            m_phyBridge = GetComponent<ff.PhysicBridge>();
            m_envDetector = GetComponent<ff.EnvironmentDetector>();
            m_rigidBody = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        void Start()
        {
            // if (m_envDetector.isOnGround)
            // {
            //     SwitchState(JumpState.None);
            // }
            // else
            // {
            //     SwitchState(JumpState.Jump1);
            // }
        }

        /*
        void Update()
        {
            float dt = Time.deltaTime;

            switch (m_jumpState)
            {
                case JumpState.None:
                    {
                        if (m_ctrl.m_jumpButton.IsPress())
                        {
                            SwitchState(JumpState.Jump1);
                            UpdateJump(dt);
                        }
                        else if (!m_envDetector.isOnGround)
                        {
                            SwitchState(JumpState.Jump1);
                        }
                    }
                    break;
                case JumpState.Jump1:

                    if (m_ctrl.m_jumpButton.IsRelease())
                    {
                        m_bHasReleaseJumpBtnInJump1 = true;
                    }

                    Vector2 curVelocity = m_rigidBody.velocity;
                    if(curVelocity.y >= 0)
                    {
                        if (m_ctrl.m_jumpButton.IsHold()
                            && m_jumpElapsedPeriod < m_continouslyJumpMaxPeriod)
                        {
                            UpdateJump(dt);
                        }
                    }
                    else
                    {
                        if (m_ctrl.m_jumpButton.IsPress()
                            || (m_ctrl.m_jumpButton.IsHold() && m_bHasReleaseJumpBtnInJump1))
                        {
                            SwitchState(JumpState.Jump2);
                        }
                    }
                    

                    if (m_rigidBody.velocity.y <= 0 && m_envDetector.isOnGround)
                    {
                        SwitchState(JumpState.None);
                    }
                    break;
                case JumpState.Jump2:
                    if (m_ctrl.m_jumpButton.IsHold()
                        && m_jump2ElapsedPeriod < m_continouselyJump2MaxPeriod)
                    {

                        UpdateJump2(dt);
                    }


                    if (m_rigidBody.velocity.y <= 0 && m_envDetector.isOnGround)
                    {
                        SwitchState(JumpState.None);
                    }
                    break;
            }
        }
        */

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


        private void UpdateJump2(float dt)
        {
            float calcJumpDeltaTime = dt;
            if (m_jump2ElapsedPeriod + dt >= m_continouselyJump2MaxPeriod)
            {
                calcJumpDeltaTime = calcJumpDeltaTime - (m_jump2ElapsedPeriod + dt - m_continouselyJump2MaxPeriod);
            }
            m_jump2ElapsedPeriod += dt;
            m_phyBridge.PerformContinouslyJump(calcJumpDeltaTime,m_jump2Value);
        }

        /*
        private void SwitchState(JumpState nextState)
        {
            if (m_jumpState != nextState)
            {
                OnExitState(m_jumpState);
                m_jumpState = nextState;
                OnEnterState(m_jumpState);
            }
        }
        */

        // private void OnEnterState(JumpState state)
        // {
        //     switch (state)
        //     {
        //         case JumpState.None:
        //             {
        //                 m_jumpElapsedPeriod = 0;
        //                 m_jump2ElapsedPeriod = 0;
        //                 m_bHasReleaseJumpBtnInJump1 = false;
        //             }
        //             break;
        //         case JumpState.Jump1:
        //             {
        //                 m_jumpElapsedPeriod = 0;
        //                 m_bHasReleaseJumpBtnInJump1 = false;
        //             }
        //             break;
        //         case JumpState.Jump2:
        //             {
        //                 //Debug.LogWarning("[Enter Jump2]");
        //                 m_jump2ElapsedPeriod = 0;

        //                 // Stop vertical velocity
        //                 Vector2 newVelocity = m_rigidBody.velocity;
        //                 newVelocity.y = 0;
        //                 m_rigidBody.velocity = newVelocity;
        //             }
        //             break;
        //     }
        // }


        // private void OnExitState(JumpState state)
        // {

        // }

    }

}
