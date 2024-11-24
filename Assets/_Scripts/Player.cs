using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRb;
    PlayerControls playerControls;
    [SerializeField] private float speed;
    [SerializeField] private float turnFactor;
    float rotAngle = 0;

    private void Awake() {
        playerControls = new PlayerControls();
        playerControls.GameplayMap.Enable();
    }
    
    private void OnDisable() {
        playerControls.GameplayMap.Disable();
    }
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
       
    }

    
    void Update()
    {
        Movement();
    }

    void Movement() {
        Vector2 movementVector = playerControls.GameplayMap.WSAD.ReadValue<Vector2>();
        
        float accelInput = movementVector.y;
        float steerInput = movementVector.x;

        Vector2 accelVector = transform.up * accelInput * speed;
        playerRb.AddForce(accelVector, ForceMode2D.Force);

        rotAngle -= steerInput * turnFactor;
        playerRb.MoveRotation(rotAngle);
    }
}

