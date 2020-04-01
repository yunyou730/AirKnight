using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ak
{
    public class Patrol : MonoBehaviour
    {
        public Transform[] points = null;
        private int curPointIndex = 0;


        private Vector3[] dests = null;


        MoveTo moveTo = null;

        void Start()
        {
            dests = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                dests[i] = points[i].position;
                Destroy(points[i].gameObject);
            }
            points = null;


            SetPositionTo(0);
            SetCurrentPointIndex(0);
            ApproachNext();
        }

        
        void Update()
        {
            //float dt = Time.deltaTime;
            //if (moveTo != null)
            //{
            //    moveTo.Update(dt);
            //    Vector2 pos = moveTo.GetCur();
            //    transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            //}
        }

        private void FixedUpdate()
        {
            float dt = Time.fixedDeltaTime;
            if (moveTo != null)
            {
                moveTo.Update(dt);
                Vector2 pos = moveTo.GetCur();
                transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            }
        }

        private void SetPositionTo(int pointIndex)
        {
            if (pointIndex < 0 || pointIndex >= dests.Length)
            {
                return;
            }
            transform.position = dests[pointIndex];
        }

        private void ApproachTo(int pointIndex)
        {
            if (pointIndex < 0 || pointIndex >= dests.Length)
            {
                return;
            }
            Vector3 dest = dests[pointIndex];
            moveTo = new MoveTo();
            moveTo.Prepare(transform.position, dest, 3);
            moveTo.Run(()=> {
                ApproachNext();
            });
        }
        
        private void NextPointIndex()
        {
            int next = curPointIndex + 1;
            next = next == dests.Length ? 0 : next;
            curPointIndex = next;
        }

        private void SetCurrentPointIndex(int pointIndex)
        {
            curPointIndex = pointIndex;
        }

        private void ApproachNext()
        {

            StartCoroutine(DoApproachStart());
        }

        private IEnumerator DoApproachStart()
        {
            //yield return new Waitf(0.5f);
            yield return 0;
            NextPointIndex();
            ApproachTo(curPointIndex);
        }
        
    }

}
