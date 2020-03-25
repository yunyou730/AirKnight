using UnityEngine;
using System.Collections;

namespace ak
{
    public class MoveTo
    {
        Vector3 from, to,cur;
        float duration = 0;

        float elapsedTime = 0;

        public delegate void FinishDelegate();
        FinishDelegate callback = null;

        bool isFinish = false;


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
            if (isFinish)
                return;

            float pct = elapsedTime / duration;
            float displayPercent = GetDisplayPercentWithTimePercent(pct);

            float x = from.x + (to.x - from.x) * displayPercent;
            float y = from.y + (to.y - from.y) * displayPercent;
            cur.x = x;
            cur.y = y;

            elapsedTime += deltaTime;

            if(displayPercent >= 1 && callback != null)
            {
                callback();
                isFinish = true;
            }
        }


        private float GetDisplayPercentWithTimePercent(float timePercent)
        {
            return Mathf.Clamp(timePercent, 0, 1);
        }

        public Vector2 GetCur()
        {
            return cur;
        }
    }
}
