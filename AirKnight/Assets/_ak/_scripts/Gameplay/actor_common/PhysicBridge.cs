using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicBridge : MonoBehaviour
    {
        // rigid body
        Rigidbody2D m_rigidBody = null;

        [Header("Move")]
        public float m_moveSpeedValue = 3.5f;

        private Vector2 m_jumpForce;
        private Vector2 m_horizonMoveSpeed;

        private void Awake()
        {
            m_rigidBody = GetComponent<Rigidbody2D>();
        }
    
        public float GetVerticalSpeed()
        {
            return m_rigidBody.velocity.y;
        }

        public void PerformContinouslyJump(float dt,float continouslyJumpValue)
        {
            m_jumpForce.x = 0;
            m_jumpForce.y = continouslyJumpValue * dt;
            m_rigidBody.AddForce(m_jumpForce, ForceMode2D.Impulse);
        }
        
        public void PerformHorizonMove(float controlDirectionValue)
        {
            controlDirectionValue = Mathf.Clamp(controlDirectionValue, -1, 1);
            m_horizonMoveSpeed.x = m_moveSpeedValue * controlDirectionValue;
            m_horizonMoveSpeed.y = m_rigidBody.velocity.y;
            m_rigidBody.velocity = m_horizonMoveSpeed;
        }
    }
}
