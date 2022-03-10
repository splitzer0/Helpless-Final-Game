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

    public bool FlashLightOn;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.InventoryOpen)
        {
            return;
        }
        else
        {
            //Debug.Log(rb.velocity.magnitude);

            CheckIfInteractable();

            AdjustAudioEmitterSize();

            MoveFunction();
        }
    }

    public void ToggleFlashLight()
    {
        if (GameManager.instance.InventoryOpen)
        {
            return;
        }
        else
        {
            if (GameManager.instance.flashlightPickedUp)
            {
                GameManager.instance.FlashLight.SetActive(FlashLightOn);
                if (!FlashLightOn)
                {
                    GameManager.instance.Enemy.GetComponent<killerAI>().canSeeLight = false;
                }
            }
            else
            {
                return;
            }
        }
    }

    //Interactable Check
    public void CheckIfInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5f))
        {
            if (hit.transform.gameObject.GetComponent<InteractableItem>() != null)
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


    //Audio Emitter Scaler
    public void AdjustAudioEmitterSize()
    {
        if (rb.velocity.magnitude > 0.1)
        {
            if (isSprinting)
            {
                AudioEmitter.localScale = new Vector3(30, 30, 30);
            }
            else if (isSneaking && !isSprinting)
            {
                AudioEmitter.localScale = new Vector3(5, 5, 5);
            }
            else if (!isSneaking && !isSprinting)
            {
                AudioEmitter.localScale = new Vector3(15, 15, 15);
            }
        }
        else if (rb.velocity.magnitude < 0.1)
        {
            AudioEmitter.localScale = Vector3.zero;
        }
    }

    //Player Movement

    public void MoveFunction()
    {
        if (MovingCheck)
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
        }
        else
        {
            return;
        }
        

        float forwardForce = RawMovementVectors.y * movementSpeed;
        float rightForce = RawMovementVectors.x * movementSpeed;
        rb.AddForce(transform.forward * forwardForce + transform.right * rightForce);

    }

    //Inventory
    
    public void ToggleInventoryUI(InputAction.CallbackContext context)
    {
        GameManager.instance.ToggleInventory();
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

    //on Interact button Pressed
    public void interact(InputAction.CallbackContext context)
    {
        if (!GameManager.instance.InventoryOpen)
        {
            if (canInteract)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 5f))
                {
                    if (hit.transform.gameObject.GetComponent<InteractableItem>() != null)
                    {
                        hit.transform.gameObject.GetComponent<InteractableItem>().Interact();
                    }
                }
            }
        }
    }

    //On Sprint Pressed
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

    //On Sneak Button Pressed
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

    //Flashlight button Pressed
    public void flashlightButton(InputAction.CallbackContext context)
    {
        if (!FlashLightOn)
        {
            FlashLightOn = true;
        }
        else if (FlashLightOn)
        {
            FlashLightOn = false;
        }

        ToggleFlashLight();
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
