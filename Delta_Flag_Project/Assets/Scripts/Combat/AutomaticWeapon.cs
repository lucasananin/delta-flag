using UnityEngine;

public class AutomaticWeapon : WeaponBehaviour
{
    [SerializeField] bool _isHoldingTrigger = false;

    private void FixedUpdate()
    {
        _nextFire += _nextFire < _weaponSO.Stats.FireRate ? Time.fixedDeltaTime : 0;

        if (!HasAmmo()) return;

        if (_nextFire >= _weaponSO.Stats.FireRate && _isHoldingTrigger)
        {
            Shoot();
        }
    }

    public override void PullTrigger()
    {
        _isHoldingTrigger = true;
        base.PullTrigger();
    }

    public override void ReleaseTrigger()
    {
        _isHoldingTrigger = false;
        base.ReleaseTrigger();
    }
}
