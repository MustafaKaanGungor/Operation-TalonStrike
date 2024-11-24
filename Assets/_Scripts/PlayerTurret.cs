using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTurret : MonoBehaviour
{
    PlayerControls playerControls;
    [SerializeField] private Transform aimPoint;
    [SerializeField] private GameObject bulletTrail;
    private float maxRange = 30f;
    private Animator turretAnimator;
    private float reloadTimer;
    private bool isReloaded;
    [SerializeField] float reloadTime = 3f;

    private void Awake() {
        playerControls = new PlayerControls();
        playerControls.GameplayMap.Enable();
        playerControls.GameplayMap.Mouse1.performed += Shoot;

        turretAnimator = GetComponent<Animator>();
    }

    private void OnDisable() {
        playerControls.GameplayMap.Disable();
    }
    
    void Update()
    {
        Look();
        ReloadController();
    }

    void Look() {
        Vector2 mousePos = playerControls.GameplayMap.MousePos.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.up = mousePos - new Vector2(transform.position.x, transform.position.y);
    }

    void ReloadController() {//TODO korotin denenicek
        if(reloadTimer >= reloadTime) {
            if(!isReloaded) {
                isReloaded = true;
            }
        }
        else {
            reloadTimer += Time.deltaTime;//!ÇOK KASTIRIYOR!!!!!!!!
        }
    }

    void Shoot(InputAction.CallbackContext context) {
        if(isReloaded) {
            turretAnimator.SetTrigger("Shoot");

            var hit = Physics2D.Raycast(aimPoint.position, transform.up, maxRange);
            var trail = Instantiate(bulletTrail, aimPoint.position, transform.rotation);
            var trailScript = trail.GetComponent<BulletTrail>();
            
            if(hit.collider != null) {
                trailScript.SetTargetPos(hit.point);

            }
            else {
                Vector3 endPos = aimPoint.position + transform.up * maxRange;
                trailScript.SetTargetPos(endPos);
            }
            isReloaded = false;
            reloadTimer = 0;
        }
    }


}
