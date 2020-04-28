﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class BaseState<T> where T : BaseEntity
    {
        public virtual void OnEnter(T entity)
        {

        }

        public virtual void OnExit(T entity)
        {
            
        }

        public virtual void Update(T entity,float dt)
        {

        }

        public virtual void FixedUpdate(T entity,float dt)
        {
            
        }

        public virtual bool HandleMessage(Telegram msg)
        {
            return false;
        }
    }
}
