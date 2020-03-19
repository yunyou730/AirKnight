using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveTest : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    private Vector2 moveDir = new Vector2();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        if (moveDir.magnitude > 0)
        {
            moveDir = moveDir.normalized * moveSpeed * Time.deltaTime;
            Vector3 pos = gameObject.transform.position;
            pos.x += moveDir.x;
            pos.y += moveDir.y;
            gameObject.transform.position = pos;
        }

    }
}
