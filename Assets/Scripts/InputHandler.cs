using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    private Vector2 moveInput;
    private Vector2 lookInput;
    private float jumpInput;
    private float fireInput;

    //INPUT READING
    // Update is called once per frame
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        fireInput = context.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        jumpInput = context.ReadValue<float>();
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }

    public Vector2 GetLookInput()
    {
        return lookInput;
    }

    public bool IsJumping()
    {
        return jumpInput > 0;
    }

    public bool IsFiring()
    {
        return fireInput > 0;
    }

}
