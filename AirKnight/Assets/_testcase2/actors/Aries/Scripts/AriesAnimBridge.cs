using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    [RequireComponent(typeof(Animator))]
    public class AriesAnimBridge : MonoBehaviour
    {
        private Animator m_animator = null;

        // layer base
        public int isMidAir { set; get; }
        public int airSpeed { set; get; }
        public int horizonAxeHoldTime { set; get; }
        public int isHorizonAxeHold { set; get; }
        public int isUpArrowHold { set; get; }

        // layer attack
        public int atkTrigger { set; get; }

        private void Awake()
        {
            m_animator = GetComponent<Animator>();

            isMidAir = Animator.StringToHash("is_mid_air");
            airSpeed = Animator.StringToHash("air_speed");
            horizonAxeHoldTime = Animator.StringToHash("horizon_axe_hold_time");
            isHorizonAxeHold = Animator.StringToHash("is_horizon_axe_hold");
            isUpArrowHold = Animator.StringToHash("is_up_arrow_hold");
            atkTrigger = Animator.StringToHash("atk_trigger");
        }
    }

}
