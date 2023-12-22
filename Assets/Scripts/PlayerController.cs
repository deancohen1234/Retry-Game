using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputHandler inputHandler;
    public PlayerMotor playerMotor;
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector2 input = inputHandler.GetMoveInput();

        playerMotor.Move(input);

        playerMotor.UpdateJump(inputHandler.IsJumping());

    }
}
