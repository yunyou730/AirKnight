using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class MessageDispatcher
    {
        private static MessageDispatcher s_instance = null;
        public static MessageDispatcher Instance()
        {
            if (s_instance == null)
            {
                s_instance = new MessageDispatcher();
            }
            return s_instance;
        }

        public void Dispatch(int sender,int receiver,MessageType msgType,System.Object extraInfo)
        {
            Telegram msg = new Telegram(sender,receiver,msgType,extraInfo);
            Discharge(msg);
        }

        private void Discharge(Telegram msg)
        {
            BaseEntity entity = EntityManager.GetInstance().GetEntity(msg.m_receiver);
            if(entity !=  null)
            {
                entity.HandleMessage(msg);
            }
        }
    }


    public enum MessageType
    {
        // MT_TryAttack,
        // MT_TryJump,

        MT_TryDash,
        MT_BreakDash,           // @todo
        MT_TakeDamage,
        MT_BreakHurt,
    }

    public class Telegram
    {
        public int m_sender = 0;
        public int m_receiver = 0;
        public MessageType m_msgType;
        public System.Object m_extraInfo = null;

        public Telegram(int sender,int receiver,MessageType msgType,System.Object extraInfo)
        {
            m_sender = sender;
            m_receiver = receiver;
            m_msgType = msgType;
            m_extraInfo = extraInfo;
        }
    }


    public class TakeDamageExtraInfo
    {
        public GameObject caster = null;

    }
}
