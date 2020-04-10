using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ff
{
    public interface Recorder
    {
        TimeFrame DoRecord();
        void PlayBack(TimeFrame timeFrame);
    }
}

