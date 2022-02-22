using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class playerControl : MonoBehaviour
{
    Rigidbody rb;
    public bool MovingCheck;
    public Vector2 RawMovementVectors;

    public float movementSpeed;

    public bool canInteract;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MovingCheck)
        {
            MoveFunction();
        } else
        {
            
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5f)){
            if (hit.transform.gameObject.CompareTag("interactable"))
            {
                Debug.Log(hit.transform.name);
                canInteract = true;
            }
            else
            {
                canInteract = false;
            }
        }
    }

    public void MoveFunction()
    {
        float forwardForce = RawMovementVectors.y * movementSpeed;
        float rightForce = RawMovementVectors.x * movementSpeed;
        rb.AddForce(transform.forward * forwardForce + transform.right * rightForce);
    }

    public void PlayerMove(InputAction.CallbackContext context)
    {
        RawMovementVectors = context.ReadValue<Vector2>();
        if(RawMovementVectors.x > 0.1 || RawMovementVectors.x < -0.1 || RawMovementVectors.y > 0.1 || RawMovementVectors.y < -0.1)
        {
            MovingCheck = true;
        } else
        {
            MovingCheck = false;
        }
    }

    public void interact(InputAction.CallbackContext context)
    {
        if (canInteract)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, 5f)){
                if(hit.transform.gameObject.GetComponent<endDoorScript>() != null)
                {
                    hit.transform.gameObject.GetComponent<endDoorScript>().EndingDoor();
                }
            }
        }
    }
}
