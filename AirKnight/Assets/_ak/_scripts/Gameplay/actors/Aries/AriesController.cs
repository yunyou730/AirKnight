using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public enum PlayerCtrlSource
    {
        PCS_1P,
        PCS_2P,
    }

    public class AriesController : MonoBehaviour
    {
        Animator m_animator = null;
        ff.AriesAnimBridge m_animBridge = null;
        ff.AriesJump m_jump = null;
        ff.AriesDash m_dash = null;
        ff.AriesBeHit m_beHit = null;
        ff.AriesBeBounceAway m_bounceAway = null;
        
        
        public ff.InputAxe m_horizontalAxe = null;
        public ff.InputAxe m_verticalAxe = null;
        public ff.InputButton m_jumpButton = null;
        public ff.InputButton m_attackButton = null;
        public ff.InputButton m_dashButton = null;

        private ff.EnvironmentDetector m_envDetector = null;
        private ff.PhysicBridge m_phyBridge = null;
        private SpriteRenderer m_spriteRenderer = null;

        public ff.FaceDir m_faceDir { get; set; } = ff.FaceDir.LEFT;     // depend on origin image 

        [Header("Input Config")]
        private string HORIZONTAL_KEY = "p_horizontal";
        private string VERTICAL_KEY = "p_vertical";
        private string JUMP_KEY = "p_jump";
        private string ATK_KEY = "p_atk_1";
        private string DASH_KEY = "p_dash";
        [SerializeField]
        PlayerCtrlSource m_ctrlSource = PlayerCtrlSource.PCS_1P;

        // avoid new Vector3 each frame,declare one for re-use in each frame
        private Vector3 m_front;


        // State Agent
        private AriesStateAgent m_stateAgent = null;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_animBridge = gameObject.AddComponent<ff.AriesAnimBridge>();
            m_envDetector = GetComponent<ff.EnvironmentDetector>();
            m_phyBridge = GetComponent<ff.PhysicBridge>();
            m_spriteRenderer = GetComponent<SpriteRenderer>();
            m_jump = gameObject.AddComponent<ff.AriesJump>();
            m_dash = gameObject.AddComponent<ff.AriesDash>();
            m_beHit = gameObject.AddComponent<ff.AriesBeHit>();
            m_bounceAway = gameObject.AddComponent<ff.AriesBeBounceAway>();


            m_stateAgent = GetComponent<AriesStateAgent>();

            string ctrlSourcePrefix = "1";
            if (m_ctrlSource == PlayerCtrlSource.PCS_2P)
            {
                ctrlSourcePrefix = "2";
            }
            m_horizontalAxe = new ff.InputAxe(ctrlSourcePrefix + HORIZONTAL_KEY);
            m_verticalAxe = new ff.InputAxe(ctrlSourcePrefix + VERTICAL_KEY);
            m_jumpButton = new ff.InputButton(ctrlSourcePrefix + JUMP_KEY);
            m_attackButton = new ff.InputButton(ctrlSourcePrefix + ATK_KEY);
            m_dashButton = new ff.InputButton(ctrlSourcePrefix + DASH_KEY);

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

        private void CollectInput(float dt)
        {
            m_horizontalAxe.Update(dt);
            m_verticalAxe.Update(dt);
            m_jumpButton.Update(dt);
            m_dashButton.Update(dt);
            
            ApplyVerticalInput();

            // @miao @todo


            // attack
            m_attackButton.Update(dt);
            if (m_attackButton.IsPress())
            {
                m_animator.SetTrigger(m_animBridge.atkTrigger);
            }

            // dash
            if (m_dashButton.IsPress())
            {
                m_dash.StartDash();
            }
        }


        public void UpdateHorizontalMove()
        {
            // horizon move
            m_animator.SetFloat(m_animBridge.horizonAxeHoldTime, m_horizontalAxe.m_holdDuration);
            m_animator.SetBool(m_animBridge.isHorizonAxeHold,m_horizontalAxe.IsHolding());
            

            // face direction
            if (m_horizontalAxe.GetValue() > 0 && m_faceDir != FaceDir.RIGHT)
            {
                SetFace(FaceDir.RIGHT);
            }
            else if (m_horizontalAxe.GetValue() < 0 && m_faceDir != FaceDir.LEFT)
            {
                SetFace(FaceDir.LEFT);
            }

            // do move distance
            m_phyBridge.PerformHorizonMove(m_horizontalAxe.GetValue());
        }

        public void ApplyVerticalInput()
        {
            // vertical direction
            bool bUpHolding = false;
            bool bDownHolding = false;
            float verticalAxeValue = m_verticalAxe.GetValue();//Input.GetAxis("Vertical");
            //Debug.Log("[vertical axe] " + verticalAxeValue);
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

        public void SetFace(FaceDir nextFace)
        {
            if (m_faceDir != nextFace)
            {
                m_faceDir = nextFace;
                m_spriteRenderer.flipX = m_faceDir == FaceDir.LEFT ? false : true;
            }
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
