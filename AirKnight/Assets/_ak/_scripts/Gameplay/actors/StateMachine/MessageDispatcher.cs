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

        public void Dispatch(int sender,int receiver,MessageType msgType,Object extraInfo)
        {
            Telegram msg = new Telegram(sender,receiver,msgType,extraInfo);

        }

        private void Discharge(Telegram msg)
        {
            BaseEntity entity = EntityManager.GetInstance().GetEntity(msg.m_receiver);

        }
    }


    public enum MessageType
    {
        MT_LandGround,
        MT_AwayFromGround,
        MT_TryAttack,
        MT_TryJump,
    }

    public class Telegram
    {
        public int m_sender = 0;
        public int m_receiver = 0;
        public MessageType m_msgType;
        public Object m_extraInfo = null;

        public Telegram(int sender,int receiver,MessageType msgType,Object extraInfo)
        {
            m_sender = sender;
            m_receiver = receiver;
            m_msgType = msgType;
            m_extraInfo = extraInfo;
        }
    }
}
