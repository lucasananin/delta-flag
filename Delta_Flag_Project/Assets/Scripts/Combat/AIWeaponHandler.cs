using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeaponHandler : MonoBehaviour
{
    [SerializeField] EntityBehaviour _entitySource = null;
    [SerializeField] List<AIWeaponModel> _weaponModels = null;

    public event System.Action OnShoot = null;

    private void Awake()
    {
        int _count = _weaponModels.Count;
        for (int i = 0; i < _count; i++)
        {
            _weaponModels[i].ResetTime();
            _weaponModels[i].InitWeapons(_entitySource);
        }
    }

    public void TryShootAll(/*AIEntity _aiEntity*/)
    {
        int _count = _weaponModels.Count;

        for (int i = 0; i < _count; i++)
        {
            var _model = _weaponModels[i];

            //if (_model.ResetTimeOnLostTarget && !_aiEntity.IsTargetOnLineOfSight)
            //{
            //    _model.ResetTime();
            //    continue;
            //}

            if (_model.IsShooting) continue;
            if (!_model.IsShootable()) continue;

            _model.IncreaseTime();

            if (!_model.HasEnoughFireTime()) continue;
            //if (!_model.CanShootWhileMoving && _aiEntity.IsMoving()) continue;
            //if (!_aiEntity.IsCloseToTargetEntity(_model.ShootDistance)) continue;
            //if (_model.OnlyShootOnTargetAcquired && !_aiEntity.IsTargetOnLineOfSight) continue;

            _model.ResetTime();
            StartCoroutine(Shoot_Routine(_model));
        }
    }

    private IEnumerator Shoot_Routine(AIWeaponModel _model)
    {
        _model.IsShooting = true;
        yield return null;

        var _weapon = _model.GetRandomWeapon();
        _weapon.PullTrigger();
        float _waitTime = _weapon.GetPullTriggerTotalTime();
        yield return new WaitForSeconds(_waitTime);

        OnShoot?.Invoke();

        _weapon.ReleaseTrigger();
        _waitTime = _weapon.GetTimeUntilAnotherShot();
        yield return new WaitForSeconds(_waitTime);

        _model.IsShooting = false;
    }

    public void StopShooting()
    {
        StopAllCoroutines();

        int _count = _weaponModels.Count;
        for (int i = 0; i < _count; i++)
        {
            _weaponModels[i].StopShooting();
            _weaponModels[i].ResetTime();
        }
    }

    public List<WeaponBehaviour> GetAllWeapons()
    {
        var _list = new List<WeaponBehaviour>();
        int _count = _weaponModels.Count;

        for (int i = 0; i < _count; i++)
        {
            _list.AddRange(_weaponModels[i].Weapons);
        }

        return _list;
    }
}
