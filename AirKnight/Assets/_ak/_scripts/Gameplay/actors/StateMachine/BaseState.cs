using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class BaseState<T> where T : Entity
    {
        public virtual void OnEnter(T entity)
        {

        }

        public virtual void OnExit(T entity)
        {
            
        }

        // public virtual void OnExecute(T entity)
        // {

        // }

        public virtual void Update(T entity,float dt)
        {

        }

        public virtual void FixedUpdate(T entity,float dt)
        {
            
        }


    }
}
