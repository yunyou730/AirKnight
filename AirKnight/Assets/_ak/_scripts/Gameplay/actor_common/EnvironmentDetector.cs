using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ff
{
    // 为了调试 动画， 临时增加 环境检测脚本EnvironmentCheck
    // 将来 可能随时替换成其他 物理检测手段
    public class EnvironmentDetector : MonoBehaviour
    {
        // ground checker
        public bool isOnGround = false;
        public LayerMask groundLayer;
        // public float groundCheckerLength = 0.3f;
        // public Vector2[] groundCheckerBeginOffset;
        // private Color groundCheckerLineColor = Color.green;

        public Collider2D m_collider = null;

        void Awake()
        {

        }

        void Start()
        {
            
        }
        
        void Update()
        {
            
        }

        void FixedUpdate()
        {
            // isOnGround = false;
            // for(int idx = 0;idx < groundCheckerBeginOffset.Length;idx++)
            // {
            //     isOnGround = isOnGround || CheckGround(idx);
            // }
            isOnGround = CheckGround();
        }
        
        //public bool CheckGround(int index)
        public bool CheckGround()
        {
            return m_collider.IsTouchingLayers(groundLayer);

            // // isOnGround = false;
            // bool bResult = false;

            // Vector2 groundCheckerFrom = new Vector2(transform.position.x + groundCheckerBeginOffset[index].x,transform.position.y + groundCheckerBeginOffset[index].y);
            // RaycastHit2D hit = Physics2D.Raycast(groundCheckerFrom, -Vector2.up, groundCheckerLength, groundLayer);

            // groundCheckerLineColor = Color.green;
            // if (hit.collider != null)
            // {
            //     groundCheckerLineColor = Color.red;
            //     bResult = true;
            // }
            // Debug.DrawRay(groundCheckerFrom, -Vector2.up * groundCheckerLength, groundCheckerLineColor);
            // return bResult;
        }
    }
}
