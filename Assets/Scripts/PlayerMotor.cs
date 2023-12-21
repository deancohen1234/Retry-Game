using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{
    public float moveSpeed = 5f;

    private bool isGrounded;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = characterController.isGrounded;
    }

    public void Move(Vector2 input)
    {
        Vector3 moveDir = new Vector3(input.x, 0, input.y);
        moveDir *= moveSpeed;

        characterController.Move(transform.TransformDirection(moveDir));
    }
}
