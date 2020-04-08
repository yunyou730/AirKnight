using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AnimStateShowSlash : StateMachineBehaviour
    {
        AriesController m_controller = null;
        public GameObject m_slashPrefab = null;
        public Vector2 m_offset;
        public Vector3 m_euler;


        private Vector3 m_slashPos;
        public bool m_isHorizontalSlash = false;
        
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            m_controller = animator.GetComponent<ff.AriesController>();
            GameObject slash = CreateSlash();
            // attach parent
            slash.transform.parent = m_controller.transform;
        }
        
        private GameObject CreateSlash()
        {
            // create slash
            GameObject slash = GameObject.Instantiate(m_slashPrefab,Vector3.zero,Quaternion.identity);
            if (slash.GetComponent<SlashEnvDetector>() != null)
            {
                slash.GetComponent<SlashEnvDetector>().InitWithOwner(m_controller.gameObject);
            }

            // locate position
            m_slashPos = m_controller.transform.position;
            float horizonOffset = m_offset.x;
            if (m_controller.m_faceDir == FaceDir.LEFT)
            {
                horizonOffset = -horizonOffset;
            }
            m_slashPos.x += horizonOffset;
            m_slashPos.y += m_offset.y;

            // update face
            if (m_isHorizontalSlash && slash.GetComponent<ff.AriesHorizontalSlash>() != null)
            {
                // horizontal slash
                m_slashPos += m_controller.GetFront();
                ff.AriesHorizontalSlash horizonSlash = slash.GetComponent<ff.AriesHorizontalSlash>();
                horizonSlash.InitWithOwner(m_controller);
            }
            slash.transform.position = m_slashPos;
            slash.transform.localEulerAngles = m_euler;
            return slash;
        }
    }
}
