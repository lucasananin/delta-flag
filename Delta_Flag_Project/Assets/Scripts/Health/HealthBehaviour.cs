using System;
using UnityEngine;

public abstract class HealthBehaviour : MonoBehaviour
{
    [SerializeField] protected bool _isInvincible = false;
    [SerializeField] protected int _maxHealth = 100;

    [Header("// READONLY")]
    [SerializeField] protected int _currentHealth = 0;
    [SerializeField] protected bool _wasDamagedThisFrame = false;
    [SerializeField] protected bool _isStaggering = false;
    [SerializeField] protected DamageModel _lastDamageModel = null;

    public bool WasDamagedThisFrame { get => _wasDamagedThisFrame; }
    public bool IsStaggering { get => _isStaggering; set => _isStaggering = value; }
    public DamageModel LastDamageModel { get => _lastDamageModel; private set => _lastDamageModel = value; }

    public event System.Action OnDamageTaken = null;
    public event System.Action OnDead = null;
    public event System.Action OnRestored = null;

    private void Awake()
    {
        RestoreAllHealth();
    }

    private void LateUpdate()
    {
        _wasDamagedThisFrame = false;
    }

    public void TakeDamage(DamageModel _damageModel)
    {
        if (!IsAlive()) return;

        _lastDamageModel = _damageModel;
        _currentHealth -= _damageModel.Value;

        if (_isInvincible)
            RestoreAllHealth();

        if (_currentHealth <= 0)
        {
            OnDead_();
        }
        else
        {
            OnDamageTaken_();
        }
    }

    public void RestoreAllHealth()
    {
        RestoreHealth(999);
    }

    public void RestoreHealth(int _percentage)
    {
        var _value = _maxHealth * (_percentage / 100f);
        _currentHealth += Mathf.RoundToInt(_value);
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        OnRestored?.Invoke();
    }

    protected virtual void OnDead_()
    {
        _wasDamagedThisFrame = true;
        _currentHealth = 0;
        OnDead?.Invoke();
    }

    protected virtual void OnDamageTaken_()
    {
        _wasDamagedThisFrame = true;
        OnDamageTaken?.Invoke();
    }

    public bool IsAlive()
    {
        return _currentHealth > 0;
    }

    public virtual float GetNormalizedValue()
    {
        return _currentHealth / (_maxHealth * 1f);
    }

    internal bool IsFull()
    {
        return _currentHealth >= _maxHealth;
    }
}
