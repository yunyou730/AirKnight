using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AnimStateShowSlash : StateMachineBehaviour
    {
        AriesController m_controller = null;
        public GameObject m_slashPrefab = null;
        public float m_slashPeriod = 0.8f;
        public Vector2 m_offset;

        private Vector3 m_slashPos;


        public bool m_isHorizontalSlash = false;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            m_controller = animator.GetComponent<ff.AriesController>();
            m_controller.StartCoroutine(ShowSlash());
        }

        private IEnumerator ShowSlash()
        {
            GameObject slash = CreateSlash();
            yield return new WaitForSeconds(m_slashPeriod);
            GameObject.Destroy(slash);
        }

        private GameObject CreateSlash()
        {
            GameObject slash = GameObject.Instantiate(m_slashPrefab,Vector3.zero,Quaternion.identity);
            m_slashPos = m_controller.transform.position;
            m_slashPos.x += m_offset.x;
            m_slashPos.y += m_offset.y;
            slash.transform.position = m_controller.GetFront() + m_slashPos;
            if (slash.GetComponent<ff.AriesHorizontalSlash>() != null)
            {
                ff.AriesHorizontalSlash horizonSlash = slash.GetComponent<ff.AriesHorizontalSlash>();
                horizonSlash.InitWithOwner(m_controller);
            }
            return slash;
        }
    }
}
