using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

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
    [SerializeField] GameObject hitEffect;
    private CinemachineImpulseSource impulseSource;

    private void Awake() {
        playerControls = new PlayerControls();
        playerControls.GameplayMap.Enable();
        playerControls.GameplayMap.Mouse1.performed += Shoot;

        turretAnimator = GetComponent<Animator>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
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

    void ReloadController() {
        if(reloadTimer >= reloadTime) {
            if(!isReloaded) {
                isReloaded = true;
            }
        }
        else {
            reloadTimer += Time.deltaTime;
        }
    }

    void Shoot(InputAction.CallbackContext context) {
        if(isReloaded) {
            turretAnimator.SetTrigger("Shoot");

            var hit = Physics2D.Raycast(aimPoint.position, transform.up, maxRange);
            var trail = Instantiate(bulletTrail, aimPoint.position, transform.rotation);
            var trailScript = trail.GetComponent<BulletTrail>();
            impulseSource.GenerateImpulseWithForce(0.5f);

            
            if(hit.collider != null) {
                trailScript.SetTargetPos(hit.point);

                Ray2D r = new Ray2D(aimPoint.position, hit.point);

                float angle = Vector2.Angle(hit.normal, r.direction);
                Debug.Log(angle);
                Debug.DrawLine(hit.point, hit.point + hit.normal, Color.yellow, 5, false);
                Debug.DrawRay(aimPoint.position, hit.point, Color.blue, 5, false);

                Instantiate(hitEffect,hit.point, transform.rotation);
                if(hit.collider.gameObject.TryGetComponent<Health>(out var health)) {
                    health.Damage(25);
                }
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
