using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestEventListener : MonoBehaviour
{
    UnityAction act = null;
    UnityAction<SpawnArg> act2 = null;

    // Start is called before the first frame update
    void Start()
    {
        act = OnEvt1;
        act2 = OnEvt2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventTest.evt1.Invoke();
            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            EventTest.evt1.AddListener(act);
            EventTest.evt2.AddListener(act2);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            EventTest.evt1.RemoveListener(act);
            EventTest.evt2.RemoveListener(act2);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            int cnt = EventTest.evt1.GetPersistentEventCount();
            for (int i = 0;i < cnt;i++)
            {
                string target = EventTest.evt1.GetPersistentTarget(i).ToString();
                string method = EventTest.evt1.GetPersistentMethodName(i);
                Debug.Log("[target][method]" + target + " " + method);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnArg arg = new SpawnArg();
            arg.pos = new Vector3(100, 200, 300);
            arg.type = "spawn_arg_11";
            EventTest.evt2.Invoke(arg);
        }
    }



    private void OnEvt1()
    {
        Debug.Log("OnEvt1");
    }

    private void OnEvt2(SpawnArg arg)
    {
        Debug.Log("OnEvt2");
        Debug.Log(arg.pos.ToString());
        Debug.Log(arg.type);
    }
}
