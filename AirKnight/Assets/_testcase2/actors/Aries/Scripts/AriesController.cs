using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesController : MonoBehaviour
    {
        Animator m_animator = null;
        AriesAnimBridge m_animBridge = null;
        
        private ff.InputAxe m_horizontalAxe = null;
        private ff.InputAxe m_verticalAxe = null;
        private ff.InputButton m_jumpButton = null;
        private ff.InputButton m_attackButton = null;

        private ff.EnvironmentDetector m_envDetector = null;
        private ff.PhysicBridge m_phyBridge = null;
        private SpriteRenderer m_spriteRenderer = null;

        public ff.FaceDir m_faceDir { get; set; } = ff.FaceDir.LEFT;     // depend on origin image 

        [Header("Jump Factors")]
        [SerializeField]
        private float m_continouslyJumpMaxPeriod = 0.8f;
        [SerializeField]
        private float m_jumpElapsedPeriod = 0.0f;


        [Header("Input Config")]
        [SerializeField]
        private string HORIZONTAL_KEY = "1p_horizontal";
        [SerializeField]
        private string VERTICAL_KEY = "1p_vertical";
        [SerializeField]
        private string JUMP_KEY = "1p_jump";
        [SerializeField]
        private string ATK_KEY = "1p_atk";

        // avoid new Vector3 each frame,declare one for re-use in each frame
        private Vector3 m_front;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_animBridge = GetComponent<ff.AriesAnimBridge>();
            m_envDetector = GetComponent<ff.EnvironmentDetector>();
            m_phyBridge = GetComponent<ff.PhysicBridge>();
            m_spriteRenderer = GetComponent<SpriteRenderer>();

            /*
            m_horizontalAxe = new ff.InputAxe("Horizontal");
            m_jumpButton = new ff.InputButton("Jump");
            m_attackButton = new ff.InputButton("Fire1");
            */

            m_horizontalAxe = new ff.InputAxe(HORIZONTAL_KEY);
            m_verticalAxe = new ff.InputAxe(VERTICAL_KEY);
            m_jumpButton = new ff.InputButton(JUMP_KEY);
            m_attackButton = new ff.InputButton("Fire1");

        }

        void Start()
        {
            
        }

        void Update()
        {
            float dt = Time.deltaTime;
            CollectInput(dt);
            UpdateStateFlag();

            // horizon move
            m_phyBridge.PerformHorizonMove(m_horizontalAxe.GetValue());


            // jump
            if (m_jumpButton.IsPress() && m_envDetector.isOnGround)
            {
                m_jumpElapsedPeriod = 0;
                UpdateJump(dt);
            }
            if (m_jumpButton.IsHold() && m_jumpElapsedPeriod < m_continouslyJumpMaxPeriod)
            {
                UpdateJump(dt);
            }
        }

        private void FixedUpdate()
        {
            float dt = Time.fixedDeltaTime;
        }

        private void CollectInput(float dt)
        {
            m_horizontalAxe.Update(dt);
            m_verticalAxe.Update(dt);
            m_jumpButton.Update(dt);

            // horizon move
            m_animator.SetFloat(m_animBridge.horizonAxeHoldTime, m_horizontalAxe.m_holdDuration);
            m_animator.SetBool(m_animBridge.isHorizonAxeHold,m_horizontalAxe.IsHolding());
            

            // face direction
            if (m_horizontalAxe.GetValue() > 0 && m_faceDir != FaceDir.RIGHT)
            {
                m_faceDir = FaceDir.RIGHT;
                m_spriteRenderer.flipX = true;
            }
            else if (m_horizontalAxe.GetValue() < 0 && m_faceDir != FaceDir.LEFT)
            {
                m_faceDir = FaceDir.LEFT;
                m_spriteRenderer.flipX = false;
            }
            

            // attack
            m_attackButton.Update(dt);
            if (m_attackButton.IsPress())
            {
                m_animator.SetTrigger(m_animBridge.atkTrigger);
            }

            // vertical direction
            bool bUpHolding = false;
            bool bDownHolding = false;
            float verticalAxeValue = m_verticalAxe.GetValue();//Input.GetAxis("Vertical");
            Debug.Log("[vertical axe] " + verticalAxeValue);
            if (verticalAxeValue > 0)
            {
                bUpHolding = true;
            }
            else if (verticalAxeValue < 0)
            {
                bDownHolding = true;
            }
            m_animator.SetBool(m_animBridge.isUpArrowHold,bUpHolding);
            m_animator.SetBool(m_animBridge.isDownArrowHold,bDownHolding);
        }


        private void UpdateStateFlag()
        {
            // air flag
            m_animator.SetBool(m_animBridge.isMidAir, !m_envDetector.isOnGround);
            // vertical speed
            m_animator.SetFloat(m_animBridge.airSpeed, m_phyBridge.GetVerticalSpeed());
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

        public Vector3 GetFront()
        {
            switch (m_faceDir)
            {
                case FaceDir.LEFT:
                    m_front.x = -1;
                    break;
                case FaceDir.RIGHT:
                    m_front.x = 1;
                    break;
            }
            return m_front;
        }
    }
}
