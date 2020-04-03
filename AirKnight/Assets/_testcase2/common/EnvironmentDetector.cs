using UnityEngine;
using System.Collections;


namespace ff
{
    // 为了调试 动画， 临时增加 环境检测脚本EnvironmentCheck
    // 将来 可能随时替换成其他 物理检测手段
    public class EnvironmentDetector : MonoBehaviour
    {
        // ground checker
        public bool isOnGround = false;
        public LayerMask groundLayer;
        public float groundCheckerLength = 0.3f;
        public Vector2 groundCheckerBeginOffset;
        private Vector2 groundCheckerFrom;
        private Color groundCheckerLineColor = Color.green;
        

        void Start()
        {
            
        }
        
        void Update()
        {
            
        }

        void FixedUpdate()
        {
            CheckGround();
        }
        
        public void CheckGround()
        {
            isOnGround = false;

            groundCheckerFrom.x = transform.position.x + groundCheckerBeginOffset.x;
            groundCheckerFrom.y = transform.position.y + groundCheckerBeginOffset.y;
            RaycastHit2D hit = Physics2D.Raycast(groundCheckerFrom, -Vector2.up, groundCheckerLength, groundLayer);

            groundCheckerLineColor = Color.green;
            if (hit.collider != null)
            {
                groundCheckerLineColor = Color.red;
                isOnGround = true;
            }
            Debug.DrawRay(groundCheckerFrom, -Vector2.up * groundCheckerLength, groundCheckerLineColor);
        }



        
    }
}
