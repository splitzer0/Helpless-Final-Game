using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class playerControl : MonoBehaviour
{
    Rigidbody rb;
    public bool MovingCheck;
    public Vector2 RawMovementVectors;

    public bool isSneaking;
    public bool isSprinting;

    public float sneakSpeed;
    public float walkingSpeed;
    public float runningSpeed;

    public float movementSpeed;

    public bool canInteract;

    public Transform AudioEmitter;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(rb.velocity.magnitude);

        if (rb.velocity.magnitude > 0.1)
        {
            if (isSprinting)
            {
                AudioEmitter.localScale = new Vector3(30, 30, 30);
            }
            else if(isSneaking && !isSprinting){
                AudioEmitter.localScale = new Vector3(5, 5, 5);
            }
            else if(!isSneaking && !isSprinting)
            {
                AudioEmitter.localScale = new Vector3(15, 15, 15);
            }
        }
        else if (rb.velocity.magnitude < 0.1)
        {
            AudioEmitter.localScale = Vector3.zero;
        }

        if (MovingCheck)
        {
            MoveFunction();
        } else
        {
            return;
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
        if (isSprinting)
        {
            movementSpeed = runningSpeed;
        }
        if (isSneaking)
        {
            movementSpeed = sneakSpeed;
        }
        if (!isSprinting && !isSneaking)
        {
            movementSpeed = walkingSpeed;
        }

        float forwardForce = RawMovementVectors.y * movementSpeed;
        float rightForce = RawMovementVectors.x * movementSpeed;
        rb.AddForce(transform.forward * forwardForce + transform.right * rightForce);

    }

    //Controller Stuff
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

    public void sprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSprinting = true;
        }
        if (context.canceled)
        {
            isSprinting = false;
        }
    }

    public void sneak(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSneaking = true;
        }
        if (context.canceled)
        {
            isSneaking = false;
        }
    }

    //Triggers

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<killerAI>().canHearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<killerAI>().canHearPlayer = false;
        }
    }

}
