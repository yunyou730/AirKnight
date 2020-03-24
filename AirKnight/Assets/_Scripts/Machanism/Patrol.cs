using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ak
{
    public class Patrol : MonoBehaviour
    {
        public Transform[] points = null;
        private int curPointIndex = 0;

        void Start()
        {
            SetPositionTo(0);
            SetCurrentPointIndex(curPointIndex);
        }

        void Update()
        {

        }

        private void SetPositionTo(int pointIndex)
        {
            if (pointIndex < 0 || pointIndex >= points.Length)
            {
                return;
            }
            Transform point = points[pointIndex];
            // @miao @todo
        }

        private void ApproachTo(int pointIndex)
        {
            if (pointIndex < 0 || pointIndex >= points.Length)
            {
                return;
            }
            Transform point = points[pointIndex];
            // @miao @todo
        }


        private int GetPrevPointIndex(int pointIndex)
        {
            int prev = pointIndex - 1;
            prev = prev < 0 ? points.Length - 1 : prev;
            return prev;
        }


        private int GetNextPointIndex(int pointIndex)
        {
            int next = pointIndex + 1;
            next = next == points.Length ? 0 : next;
            return next;
        }

        private void SetCurrentPointIndex(int pointIndex)
        {
            curPointIndex = pointIndex;
        }
    }

}
