﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class AriesRecorder : MonoBehaviour,Recorder
    {
        private Rigidbody2D m_rigid = null;
        private SpriteRenderer m_spriteRender = null;

        void Awake()
        {
            m_rigid = GetComponent<Rigidbody2D>();
            m_spriteRender = GetComponent<SpriteRenderer>();
        }
        
        public TimeFrame DoRecord()
        {
            AriesTimeFrame frame = new AriesTimeFrame();
            frame.m_pos = transform.position;
            frame.m_velocity = m_rigid.velocity;
            frame.m_isFlipX = m_spriteRender.flipX;
            frame.m_spriteFrame = m_spriteRender.sprite;
            return frame;
        }

        public void PlayBack(TimeFrame timeFrame)
        {
            AriesTimeFrame frame = (AriesTimeFrame)timeFrame;
            transform.position = frame.m_pos;
            m_rigid.velocity = frame.m_velocity;
            m_spriteRender.flipX = frame.m_isFlipX;
            m_spriteRender.sprite = frame.m_spriteFrame;
        }
    }

}
