using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    [RequireComponent(typeof(Animator))]
    public class AriesAnimBridge : MonoBehaviour
    {
        public int isJumPress { set; get; }
        public int isMidAir { set; get; }
        public int airSpeed { set; get; }
        public int horizonAxeHoldTime { set; get; }
        public int isHorizonAxeHold { set; get; }


        private void Awake()
        {
            isJumPress = Animator.StringToHash("is_jump_press");
            isMidAir = Animator.StringToHash("is_mid_air");
            airSpeed = Animator.StringToHash("air_speed");
            horizonAxeHoldTime = Animator.StringToHash("horizon_axe_hold_time");
            isHorizonAxeHold = Animator.StringToHash("is_horizon_axe_hold");
        }

        void Start()
        {

        }

        void Update()
        {
            
        }
    }

}
