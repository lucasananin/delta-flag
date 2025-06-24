using System.Net;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public abstract class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] protected WeaponSO _weaponSO = null;
    [SerializeField] protected Transform _muzzle = null;

    [Header("// READONLY")]
    [SerializeField] protected EntityBehaviour _entitySource = null;
    [SerializeField] protected AmmoHandler _ammoHandler = null;
    [SerializeField] protected float _nextFire = 0;

    protected virtual void Awake()
    {
        _nextFire = _weaponSO.Stats.FireRate;
    }

    public virtual void Init(EntityBehaviour _entityBehaviour, AmmoHandler _ammoHandler)
    {
        this._ammoHandler = _ammoHandler;
        Init(_entityBehaviour);
    }

    public virtual void Init(EntityBehaviour _entityBehaviour)
    {
        _entitySource = _entityBehaviour;
        //OnInit?.Invoke(this);
    }


    public virtual void PullTrigger()
    {
        //Debug.Log($"{gameObject.name} Pull", this);
    }

    public virtual void ReleaseTrigger()
    {
        //Debug.Log($"{gameObject.name} Release", this);
    }

    public virtual void Shoot()
    {
        _nextFire = 0;
        PrepareProjectile(_weaponSO.ProjectileSO);
        //DecreaseAmmo(_weaponSO.ProjectileSO);
        //OnShoot?.Invoke();
    }

    private void PrepareProjectile(ProjectileSO _projectileSO)
    {
        for (int i = 0; i < _weaponSO.Stats.ProjectilesPerShot; i++)
        {
            //var _position = CalculateProjectilePosition(_projectileSO);
            //var _rotation = CalculateProjectileRotation(i);
            //var _position = _muzzle.position;
            //var _rotation = _muzzle.rotation;

            //Vector3 _shootDirection = GetSpreadDirection(_muzzle.forward, _weaponSO.Stats.ShootAngle);
            //var _rotation = Quaternion.LookRotation(_shootDirection);

            //float angle = Random.Range(0.0f, _weaponSO.Stats.SpreadAngle * 0.5f);
            //Vector2 angleDir = Random.insideUnitCircle * Mathf.Tan(angle * Mathf.Deg2Rad);
            //Vector3 dir = _muzzle.forward + (Vector3)angleDir;
            //dir.Normalize();
            //_rotation = Quaternion.LookRotation(dir);

            //float spreadRatio = _weaponSO.Stats.SpreadAngle / Camera.main.fieldOfView;
            //Vector2 spread = spreadRatio * Random.insideUnitCircle;
            //Ray r = Camera.main.ViewportPointToRay(Vector3.one * 0.5f + (Vector3)spread);
            //_rotation = Quaternion.LookRotation(r.direction);

            //var _cameraTransform = Camera.main.transform;
            //Ray _ray = new(_cameraTransform.position, _cameraTransform.forward);
            //Vector3 _targetPoint;
            //_targetPoint = Physics.Raycast(_ray, out RaycastHit hit, 999) ? hit.point : _ray.GetPoint(999);
            //Vector3 _direction = (_targetPoint - _muzzle.position).normalized;
            //_direction = ApplySpread(_direction, _weaponSO.Stats.SpreadAngle);
            //var _rotation = Quaternion.LookRotation(_direction);

            var _position = GeneratePosition(_projectileSO);
            var _rotation = GenerateRotation();

            //var _parent = _entitySource.transform;
            var _projectile = Instantiate(_projectileSO.Prefab, _position, _rotation/*, _parent*/);
            var _shootModel = new ShootModel(_entitySource, this, _projectileSO);
            _projectile.Init(_shootModel);
        }
    }

    private Vector3 GeneratePosition(ProjectileSO _so)
    {
        var _position = _muzzle.position + _muzzle.forward * _so.ForwardOffset;
        return _position;
    }

    private Quaternion GenerateRotation()
    {
        var _cameraTransform = Camera.main.transform;
        var _ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        var _targetPoint = Physics.Raycast(_ray, out RaycastHit hit, 999) ? hit.point : _ray.GetPoint(999);
        var _direction = (_targetPoint - _muzzle.position).normalized;

        float _spreadInRadians = _weaponSO.Stats.SpreadAngle * Mathf.Deg2Rad;
        float _randomYaw = Random.Range(-_spreadInRadians, _spreadInRadians);
        float _randomPitch = Random.Range(-_spreadInRadians, _spreadInRadians);
        Quaternion _spreadRotation = Quaternion.Euler(_randomPitch * Mathf.Rad2Deg, _randomYaw * Mathf.Rad2Deg, 0);
        _direction = _spreadRotation * _direction;

        return Quaternion.LookRotation(_direction);
    }

    private Vector3 ApplySpread(Vector3 _direction, float _angle)
    {
        float _spreadInRadians = _angle * Mathf.Deg2Rad;
        float _randomYaw = Random.Range(-_spreadInRadians, _spreadInRadians);
        float _randomPitch = Random.Range(-_spreadInRadians, _spreadInRadians);
        Quaternion _spreadRotation = Quaternion.Euler(_randomPitch * Mathf.Rad2Deg, _randomYaw * Mathf.Rad2Deg, 0);
        return _spreadRotation * _direction;
    }

    Vector3 GetSpreadDirection(Vector3 forward, float angle)
    {
        // Random angle offset from the center
        float spreadInRadians = angle * Mathf.Deg2Rad;

        // Create a random direction inside the cone
        float randomYaw = Random.Range(-spreadInRadians, spreadInRadians);
        float randomPitch = Random.Range(-spreadInRadians, spreadInRadians);

        Quaternion rotation = Quaternion.Euler(randomPitch * Mathf.Rad2Deg, randomYaw * Mathf.Rad2Deg, 0);
        return rotation * forward;
    }

    private Quaternion CalculateProjectileAngle(ProjectileSO _projectileSO)
    {
        var _halfAngle = _weaponSO.Stats.SpreadAngle / 2f;
        var _randomAngle = Random.Range(-_halfAngle, _halfAngle);
        return Quaternion.AngleAxis(_randomAngle, _muzzle.forward);
    }

    public string GetId()
    {
        return _weaponSO.Id;
    }

    public bool HasAmmo()
    {
        //return _ammoHandler is null ? true : _ammoHandler.HasAmmo(_weaponSO.ProjectileSO, _weaponSO);
        return true;
    }

    public string GetAmmoString()
    {
        //return _ammoHandler is null ? $"-" : $"{_ammoHandler.GetAmmoQuantity(_weaponSO.ProjectileSO)}";
        return string.Empty;
    }

    public int GetDamage()
    {
        return _weaponSO.Stats.Damage;
    }
}
