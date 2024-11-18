using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRb;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();

        
    }

    
    void Update()
    {
        
    }

    public void Movement(InputAction.CallbackContext callbackContext) {
        Vector2 movementVector = callbackContext.ReadValue<Vector2>();

        if(movementVector.x > 0) {
            playerRb.AddForce(movementVector * 10);
        }
    }
}

