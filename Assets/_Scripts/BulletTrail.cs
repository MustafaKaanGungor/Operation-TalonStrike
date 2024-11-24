using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 targetPos;
    private float progress;

    [SerializeField] private float speed = 40f;

    private void Start() {
        startPos = transform.position;
        startPos = new Vector3(startPos.x, startPos.y, -1);
    }

    private void Update() {
        progress += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPos, targetPos, progress);
    }

    public void SetTargetPos(Vector3 targetPos) {
        this.targetPos = targetPos;
        this.targetPos = new Vector3(targetPos.x, targetPos.y, -1);
    }
}
