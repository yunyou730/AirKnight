using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class HurtCauser : MonoBehaviour
    {
        void Awake()
        {


        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            CanBeHit canBeHit = collision.GetComponent<CanBeHit>();
            if(canBeHit != null)
            {   
                canBeHit.OnBeHit(gameObject);
            }
        }

        public void OnTriggerStay2D(Collider2D collision)
        {
            CanBeHit canBeHit = collision.GetComponent<CanBeHit>();
            if(canBeHit != null)
            {   
                canBeHit.OnBeHit(gameObject);
            }
        }
    }
}
