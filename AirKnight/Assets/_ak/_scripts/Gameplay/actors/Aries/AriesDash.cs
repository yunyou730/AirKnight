using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesDash : MonoBehaviour
    {
        Rigidbody2D m_rigid = null;
        Animator m_animator = null;
        ff.AriesAnimBridge m_animBridge = null;
        ff.AriesController m_ctrl = null;
        ff.EnvironmentDetector m_envDetector = null;

        [SerializeField]
        public float m_dashKeepTime = 0.3f;
        [SerializeField]
        public float m_dashSpeed = 20.0f;

        // float m_startGravityScale = 0.0f;
        // bool m_bDashing = false;

        // private int m_dashCounter = 0;

        private void Awake()
        {
            // m_rigid = GetComponent<Rigidbody2D>();
            // m_animator = GetComponent<Animator>();
            // m_animBridge = GetComponent<ff.AriesAnimBridge>();
            // m_ctrl = GetComponent<ff.AriesController>();
            // m_envDetector = GetComponent<ff.EnvironmentDetector>();
        }

        // public void StartDash()
        // {
        //     m_startGravityScale = m_rigid.gravityScale;
        //     m_rigid.gravityScale = 0.0f;
        //     m_animator.SetTrigger(m_animBridge.dashTrigger);
        //     StartCoroutine(StopDash());
        // }

        // private IEnumerator StopDash()
        // {
        //     yield return new WaitForSeconds(m_dashKeepTime);
        //     DoStopDash();
        // }

        // private void DoStopDash()
        // {
        //     // m_bDashing = false;
        //     m_rigid.gravityScale = m_startGravityScale;
        //     m_animator.SetTrigger(m_animBridge.dashRecoverTrigger);
        // }

        // private void FixedUpdate()
        // {
            // if(m_bDashing)
            // {
            //     Vector3 dir = m_ctrl.GetFront();
            //     m_rigid.velocity = dir * m_dashSpeed;
            // }

            // if (m_envDetector.isOnGround)
            // {
            //     m_dashCounter = 0;
            // }
        // }


        // private bool IsCanDash()
        // {
        //     if (m_bDashing)
        //         return false;
        //     return m_dashCounter == 0;
        // }
    }
}
