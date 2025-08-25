using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIWeaponModel
{
    [SerializeField] List<WeaponBehaviour> _weapons = null;
    [SerializeField] Vector2 _shootRateRange = default;

    [Header("// READONLY")]
    [SerializeField] bool _isShooting = false;
    [SerializeField] float _timeUntilShoot = 0f;
    [SerializeField] float _shootTimer = 0f;

    public bool IsShooting { get => _isShooting; set => _isShooting = value; }
    public List<WeaponBehaviour> Weapons { get => _weapons; }

    public void InitWeapons(EntityBehaviour _entitySource)
    {
        int _count = _weapons.Count;
        for (int i = 0; i < _count; i++)
        {
            _weapons[i].Init(_entitySource);
        }
    }

    public void ResetTime()
    {
        _timeUntilShoot = Random.Range(_shootRateRange.x, _shootRateRange.y);
        _shootTimer = 0;
    }

    public void IncreaseTime()
    {
        _shootTimer += Time.deltaTime;
    }

    public bool HasEnoughFireTime()
    {
        return _shootTimer > _timeUntilShoot;
    }

    public bool IsShootable()
    {
        return _shootRateRange.x + _shootRateRange.y > 0;
    }

    public WeaponBehaviour GetRandomWeapon()
    {
        int _randomIndex = Random.Range(0, _weapons.Count);
        return _weapons[_randomIndex];
    }

    public void StopShooting()
    {
        _isShooting = false;

        int _count = _weapons.Count;
        for (int i = 0; i < _count; i++)
        {
            _weapons[i].ReleaseTrigger();
            _weapons[i].ForceStop();
        }
    }

    public void SetAlignmentLook(Vector3 _targetPoint)
    {
        int _count = _weapons.Count;
        for (int i = 0; i < _count; i++)
        {
            _weapons[i].SetLookToAlignment(_targetPoint);
        }
    }
}