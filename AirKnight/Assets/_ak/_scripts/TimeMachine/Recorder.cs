using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public interface Recorder
    {
        TimeFrame DoRecord();
        void DoPlayBack(TimeFrame timeFrame);

        void StartRecord();
        void StartPlayBack();
    }
}

