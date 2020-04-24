using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AriesHorizontalSlash : MonoBehaviour
    {
        AriesController m_controller = null;

        private void Awake()
        {
            
        }
        
        public void InitWithOwner(AriesController controller)
        {
            m_controller = controller;
            switch (m_controller.m_faceDir)
            {
                case FaceDir.LEFT:
                    transform.localScale = new Vector3(-1,1,1);
                    break;
                case FaceDir.RIGHT:
                    transform.localScale = new Vector3(1, 1, 1);
                    break;
            }
        }
    }
}
