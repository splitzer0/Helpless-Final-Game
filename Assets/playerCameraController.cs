using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class playerCameraController : MonoBehaviour
{
    public Vector2 MouseVector;
    public Transform PlayerBody;
    public float mouseSensitivity;
    public float XRot;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //limits mouse speed

        MouseVector = Vector2.ClampMagnitude(MouseVector, 1);

        MouseVector = MouseVector * 5;

        Vector2 ModifiedMouseVector = MouseVector * mouseSensitivity * Time.deltaTime;

        XRot -= ModifiedMouseVector.y;
        XRot = Mathf.Clamp(XRot, -90f, 90f);
        transform.localRotation = Quaternion.Euler(XRot, 0, 0);

        PlayerBody.Rotate(PlayerBody.up * ModifiedMouseVector.x);
    }

    public void CameraRot(InputAction.CallbackContext context)
    {
        MouseVector = context.ReadValue<Vector2>();
    }
}
