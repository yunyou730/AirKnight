using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ff
{
    public class AriesStateGlobal : BaseState<AriesEntity>
    {
        Animator    m_animator = null;
        AriesAnimBridge m_animBridge = null;
        public AriesStateGlobal(AriesEntity entity) : base(entity)
        {
            m_animator = entity.GetAgent().GetComponent<Animator>();
            m_animBridge = entity.GetAgent().GetComponent<AriesAnimBridge>();
        }

        public override void OnEnter(AriesEntity entity,Telegram msg)
        {
            base.OnEnter(entity,msg);
        }

        public override void OnExit(AriesEntity entity)
        {
            base.OnExit(entity);
        }

        public override void Update(AriesEntity entity, float dt)
        {
            base.Update(entity, dt);
        }

        public override void FixedUpdate(AriesEntity entity, float dt)
        {
            base.FixedUpdate(entity, dt);
        }

        public override bool HandleMessage(AriesEntity entity, Telegram msg)
        {
            switch(msg.m_msgType)
            {
                case MessageType.MT_TakeDamage:
                {
                    entity.ChangeState(AriesState.Hurt,msg);
                    return true;
                }
                case MessageType.MT_TryAttack:
                {
                    m_animator.SetTrigger(m_animBridge.atkTrigger);
                    return true;
                }
            }
            return false;
        }

    }

}
