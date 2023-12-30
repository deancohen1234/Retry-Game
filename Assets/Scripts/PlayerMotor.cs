using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerMotor : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxAcceleration = 20f;

    [Space(10)]
    public float jumpHeight = 2f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Space(10)]
    public LayerMask groundCheckMask;

    private bool isGrounded;

    //set on awake
    private float jumpSpeed;
    private Vector3 currentVelocity;

    private Rigidbody body;

    private void Awake()
    {

        body = GetComponent<Rigidbody>();

        jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(body.position, Vector3.down, 1.01f + Mathf.Epsilon, groundCheckMask);
    }

    //take in a move direction
    public void Move(Vector3 moveDirection)
    {
        currentVelocity = body.velocity;

        Vector3 moveDir = moveDirection;
        moveDir *= moveSpeed;

        Vector3 desiredVelocity = (transform.forward * moveDir.z) + (transform.right * moveDir.x);

        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        currentVelocity.x =
            Mathf.MoveTowards(currentVelocity.x, desiredVelocity.x, maxSpeedChange);
        currentVelocity.z =
            Mathf.MoveTowards(currentVelocity.z, desiredVelocity.z, maxSpeedChange);


        body.velocity = currentVelocity;
    }

    public void UpdateJump(bool isJumping)
    {
        if (isGrounded && isJumping)
        {
            body.velocity += Vector3.up * jumpSpeed;
        }

        ////if we are falling
        if (body.velocity.y < 0)
        {
            //minus 1 here because (since this is a multiplier) Unity already applied 1x gravity)
            body.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        //if we are rising AND not holding jump, dampen the jump
        else if (body.velocity.y > 0 && !isJumping)
        {
            //minus 1 here because (since this is a multiplier) Unity already applied 1x gravity)
            body.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 20), "Is Grounded: " + isGrounded);
    }
}
