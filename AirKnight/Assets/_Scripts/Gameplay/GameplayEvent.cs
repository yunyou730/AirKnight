using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ak
{
    public class GameplayEvent
    {

        public delegate void PlayerSpawned();
        public static event PlayerSpawned playerSpawnedEvent;
        public static void DispatchPlayerSpawnedEvent()
        {
            playerSpawnedEvent?.Invoke();
        }


    }

}

