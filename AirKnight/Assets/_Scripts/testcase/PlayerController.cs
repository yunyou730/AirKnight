using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 7.0f;
    public float jumpForce = 2.0f;
    Vector2 inputDir = new Vector2();


    Rigidbody2D rigidBody = null;
    //bool bJumpButtonDown = false;


    bool bLand = false;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        UpdateStateFlags();
        CollectInput();
    }

    private void FixedUpdate()
    {
        HandleInput(Time.fixedDeltaTime);
    }

    private void UpdateStateFlags()
    {

    }


    private void CollectInput()
    {
        inputDir.x = Input.GetAxisRaw("Horizontal");
        inputDir.Normalize();
        //bJumpButtonDown = Input.GetButtonDown("Jump");
    }

    private void HandleInput(float dt)
    {
        // horizon move
        float offset = moveSpeed * dt * inputDir.x;
        transform.position = transform.position + new Vector3(offset, 0, 0);
        // jump
        if(bLand && Input.GetButtonDown("Jump"))
        {
            rigidBody.AddForce(new Vector2(0, jumpForce));
        }
    }
}
