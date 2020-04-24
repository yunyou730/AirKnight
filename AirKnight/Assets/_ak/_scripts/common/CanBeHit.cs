using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class CanBeHit : MonoBehaviour
    {
        public virtual void OnBeHit(GameObject caster)
        {
            Debug.Log("[CanBeHit][OnBeHit]");
        }
    }
}
