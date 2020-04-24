using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public class CanBeBounceAway : MonoBehaviour
    {
        public virtual void OnBeBounced(GameObject other)
        {
            Debug.Log("[CanBeBounceAway][OnBeBounced]");
        }
    }
}
