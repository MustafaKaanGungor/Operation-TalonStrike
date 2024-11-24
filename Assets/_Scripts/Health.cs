using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int currentHp;

    void Awake()
    {
        currentHp = maxHp;
    }

    public UnityEvent<int> Healed;
    public UnityEvent<int> Damaged;
    public UnityEvent<int> Died;

    public int _Hp 
    {
        get => currentHp;
        set {
            var isDamage = value < currentHp;
            currentHp = Mathf.Clamp(value, 0, maxHp);
            if(isDamage) {
                Damaged.Invoke(currentHp);
            }
            else {
                Healed.Invoke(currentHp);
            }

            if(currentHp <= 0) {
                Died.Invoke(currentHp); 
            }
        }
    }

    public void Damage(int amount) {
        _Hp -= amount;
    }

    public void Heal(int amount) {
        _Hp += amount;
    }

    public void HealFull() {
        _Hp = maxHp;
    }

    public void Kill() {
        _Hp = 0;
    }

    public void Adjust(int amount) {
        _Hp = amount;
    }

}