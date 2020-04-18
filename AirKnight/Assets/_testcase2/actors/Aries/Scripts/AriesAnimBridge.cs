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
        public int isDownArrowHold { set; get; }

        // layer attack
        public int atkTrigger { set; get; }

        // layer dash
        public int dashTrigger { set; get; }
        public int dashRecoverTrigger { set; get; }

        // layer hurt
        public int hurtTrigger { set; get; }
        public int hurtRecoverTrigger { set; get; }

        private void Awake()
        {
            m_animator = GetComponent<Animator>();

            isMidAir = Animator.StringToHash("is_mid_air");
            airSpeed = Animator.StringToHash("air_speed");
            horizonAxeHoldTime = Animator.StringToHash("horizon_axe_hold_time");
            isHorizonAxeHold = Animator.StringToHash("is_horizon_axe_hold");
            isUpArrowHold = Animator.StringToHash("is_up_arrow_hold");
            isDownArrowHold = Animator.StringToHash("is_down_arrow_hold");
            atkTrigger = Animator.StringToHash("atk_trigger");
            hurtTrigger = Animator.StringToHash("hurt_trigger");
            hurtRecoverTrigger = Animator.StringToHash("hurt_recover");
            dashTrigger = Animator.StringToHash("dash_trigger");
            dashRecoverTrigger = Animator.StringToHash("dash_recover");
        }
    }
}
