using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 7.0f;
    public float jumpForce = 2.0f;
    Vector2 inputDir = new Vector2();


    Rigidbody2D rigidBody = null;
    public Collider2D mainCollider = null;

    public bool bLand = false;


    public Vector3[] footOffsets = null;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
        CollectInput();
    }

    private void FixedUpdate()
    {
        FootLandDetect();
        HandleInput(Time.fixedDeltaTime);
    }

    
    private void FootLandDetect()
    {
        bLand = false;
        for (int i = 0; i < footOffsets.Length; i++)
        {
            Vector2 from = new Vector2();
            from.x = transform.position.x + footOffsets[i].x;
            from.y = transform.position.y + footOffsets[i].y;
            float checkLength = 0.25f;
            string[] checkLayers = { "AK_ground" };
            RaycastHit2D hit = Physics2D.Raycast(from, new Vector2(0, -1), checkLength, LayerMask.GetMask(checkLayers));

            Color col = new Color(0,0,0);
            if (hit.collider != null)
            {
                bLand = true;
                col.r = 1;
            }
            else
            {
                col.g = 1;
            }
            Debug.DrawRay(transform.position + footOffsets[i], new Vector3(0, -checkLength, 0), col);

        }
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
