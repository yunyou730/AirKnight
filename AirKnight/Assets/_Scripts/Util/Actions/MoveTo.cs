using UnityEngine;
using System.Collections;

namespace ak
{
    public class MoveTo
    {
        Vector3 from, to;
        float duration = 0;

        float elapsedTime = 0;
        bool isStarted = false;

        public delegate void FinishDelegate();
        FinishDelegate callback = null;


        public void Prepare(Vector3 from,Vector3 to,float duration)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
        }

        public void Run(FinishDelegate callback = null)
        {
            elapsedTime = 0;
            this.callback = callback;
        }

        public void Update(float deltaTime)
        {
            float pct = elapsedTime / duration;
            float displayPercent = GetDisplayPercentWithTimePercent(pct);



            elapsedTime += deltaTime;
        }


        private float GetDisplayPercentWithTimePercent(float timePercent)
        {
            return timePercent;
        }
    }
}
