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
        public Quaternion m_rotation;

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
            GameObject slash = GameObject.Instantiate(m_slashPrefab,Vector3.zero,Quaternion.identity);
            //GameObject slash = GameObject.Instantiate(m_slashPrefab, Vector3.zero, m_rotation);
            m_slashPos = m_controller.transform.position;
            m_slashPos.x += m_offset.x;
            m_slashPos.y += m_offset.y;
            if (slash.GetComponent<ff.AriesHorizontalSlash>() != null)
            {
                m_slashPos += m_controller.GetFront();
                ff.AriesHorizontalSlash horizonSlash = slash.GetComponent<ff.AriesHorizontalSlash>();
                horizonSlash.InitWithOwner(m_controller);
            }
            slash.transform.position = m_slashPos;
            return slash;
        }
    }
}
