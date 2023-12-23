using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputHandler inputHandler;
    public PlayerMotor playerMotor;
    public CameraLook cameraLook;
    public RifleShooter rifleShooter;

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

        //calculate move direction from input
        Vector3 moveDirection = (cameraLook.transform.forward * input.y) + (cameraLook.transform.right * input.x);
        moveDirection = Vector3.ProjectOnPlane(moveDirection, Vector3.up).normalized;

        playerMotor.Move(moveDirection);

        playerMotor.UpdateJump(inputHandler.IsJumping());

        //handle firing
        if (inputHandler.IsFiring())
        {
            Debug.Log("Is Firing");
            rifleShooter.Fire();
        }
    }
}
