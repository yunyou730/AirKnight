using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ak
{
    // all event instance
    public class GameplayEvent
    {
        public static PlayerSpawnedEvent playerSpawnedEvent = new PlayerSpawnedEvent();
    }
    
    // event 

    // event define
    public class PlayerSpawnedArg
    {
        public GameObject playerGo = null;
    }

    public class PlayerSpawnedEvent : UnityEvent<PlayerSpawnedArg>
    {
    }
}

