using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTest// : MonoBehaviour
{
    static public UnityEvent evt1 = new UnityEvent();
    static public SpawnEvent evt2 = new SpawnEvent();
}


public class SpawnArg
{
    public Vector3 pos;
    public string type;
}

public class SpawnEvent :  UnityEvent<SpawnArg>
{

}
