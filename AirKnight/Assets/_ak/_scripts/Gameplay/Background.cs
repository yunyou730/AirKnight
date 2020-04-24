using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class Background : MonoBehaviour
    {
        MeshRenderer    m_meshRenderer = null;
        Material        m_material = null;

        public float m_horizontalMoveSpeed = 1.0f;
        public float m_verticalMoveSpeed = 1.0f;

        void Start()
        {
            m_meshRenderer = GetComponent<MeshRenderer>();
            m_material = m_meshRenderer.material;
        }

        // Update is called once per frame
        void Update()
        {
            float dt = Time.deltaTime;

            Vector2 curTextureOffset = m_material.GetTextureOffset("_MainTex");
            curTextureOffset.x += m_horizontalMoveSpeed * dt;
            curTextureOffset.y += m_verticalMoveSpeed * dt;
            if(curTextureOffset.x > 1)
            {
                curTextureOffset.x -= 1;
            }
            if(curTextureOffset.y > 1)
            {
                curTextureOffset.y -= 1;
            }
            m_material.SetTextureOffset("_MainTex",curTextureOffset);
        }
    }


}
