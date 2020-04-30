﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesStateWalk : BaseState<AriesEntity>
    {
        public override void Update(AriesEntity entity, float dt)
        {
            AriesController ctrl = entity.GetAgent().GetComponent<AriesController>();
            Rigidbody2D rigid = entity.GetAgent().GetComponent<Rigidbody2D>();
            ctrl.UpdateHorizontalMove();

            if (Mathf.Abs(rigid.velocity.x) == 0)
            {
                entity.ChangeState(AriesState.Idle);
            }
            if (ctrl.m_jumpButton.IsPress())
            {
                entity.ChangeState(AriesState.Jump1);
            }
        }

        public override void FixedUpdate(AriesEntity entity, float dt)
        {
            EnvironmentDetector envDetector = entity.GetAgent().GetComponent<EnvironmentDetector>();
            if (!envDetector.isOnGround)
            {
                entity.ChangeState(AriesState.Jump1);
            }
        }
    }
}
