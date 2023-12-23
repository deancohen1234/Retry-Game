using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public InputHandler inputHandler;
    public float sensitivity = 30f;
    public Vector2 clampRange = new Vector2(-80f, 80f);

    private float xRotation, yRotation = 0f;

    private void LateUpdate()
    {
        ProcessInput(inputHandler.GetLookInput());
    }

    public void ProcessInput(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation += (-mouseY * Time.deltaTime * sensitivity);
        xRotation = Mathf.Clamp(xRotation, clampRange.x, clampRange.y);

        yRotation += (mouseX * Time.deltaTime * sensitivity);

        //only apply x axis rotation so the rotations dont stack
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);

        //move the input handlers y rotation so character can spin
        Quaternion yawRotation = Quaternion.Euler(0, yRotation, 0);

        //inputHandler.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * mouseX * sensitivity);
        //inputHandler.gameObject.transform.rotation = yawRotation;
    }
}
