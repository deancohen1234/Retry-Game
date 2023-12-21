using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    private Vector2 moveInput;
    private Vector2 lookInput;

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

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }

    public Vector2 GetLookInput()
    {
        return lookInput;
    }

}
