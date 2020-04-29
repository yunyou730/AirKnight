using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace ff
{
    public class BaseState<T> where T : BaseEntity
    {
        public virtual void OnEnter(T entity)
        {
            // Debug.Log("[BaseState:OnEnter]" + GetType().Name);
        }

        public virtual void OnExit(T entity)
        {
            // Debug.Log("[BaseState:OnExit]" + GetType().Name);
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
