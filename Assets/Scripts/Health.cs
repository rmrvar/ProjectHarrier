using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField]
    public int MaxValue { get; private set; }

    public int Value { get; private set; }

    public event System.Action OnHealed;
    public event System.Action OnDamaged;
    public event System.Action OnKilled;

    private void Awake()
    {
        Value = MaxValue;
    }

    public void Hurt(int value)
    {
        Value = Math.Max(Value - value, 0);
        OnDamaged?.Invoke();
        if (Value <= 0)
        {
            OnKilled?.Invoke();
        }
    }

    public void Heal(int value)
    {
        Value = Math.Min(Value + value, MaxValue);
        OnHealed?.Invoke();
    }

    public void TopOff()
    {
        Value = MaxValue;
    }
}
