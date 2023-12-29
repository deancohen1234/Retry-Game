using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    #region Variables

    private PlayerController controller;

    public float intensity = 5f;
    public float smooth = 4f;

    private Quaternion origin_rotation;

    #endregion



    #region MonoBehaviour Callbacks

    private void Start()
    {
        controller = GetComponentInParent<PlayerController>();

        origin_rotation = transform.localRotation;
    }

    private void Update()
    {
        UpdateSway();
    }

    #endregion



    #region Private Methods

    private void UpdateSway()
    {
        //controls
        float t_x_mouse = controller.inputHandler.GetLookInput().x;
        float t_y_mouse = -controller.inputHandler.GetLookInput().y;

        //calculate target rotation
        Quaternion t_x_adj = Quaternion.AngleAxis(-intensity * t_x_mouse, Vector3.up);
        Quaternion t_y_adj = Quaternion.AngleAxis(intensity * t_y_mouse, Vector3.right);
        Quaternion target_rotation = origin_rotation * t_x_adj * t_y_adj;

        //rotate towards target rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * smooth);
    }

    #endregion
}
