using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{

    public enum State
    {
        State_Aries_Idle,
        State_Aries_Walk,
        State_Aries_Jump_1,
        State_Aries_Jump_2,
        State_Aries_Hurt,
        State_Aries_Dash,
    }

    public class BaseState<T> where T : Entity
    {
        public virtual void OnEnter(T entity)
        {

        }

        public virtual void OnExit(T entity)
        {
            
        }

        public virtual void OnExecute(T entity)
        {

        }
    }
}
