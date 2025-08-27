using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.Events;

public abstract class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] protected WeaponSO _weaponSO = null;
    [SerializeField] protected Transform _muzzle = null;
    [SerializeField] protected Transform _alignmentOrigin = null;

    [Header("// READONLY")]
    [SerializeField] protected EntityBehaviour _entitySource = null;
    [SerializeField] protected AmmoHandler _ammoHandler = null;
    [SerializeField] protected float _nextFire = 0;
    [SerializeField] protected int _magazineAmmo = 0;

    public event UnityAction<WeaponBehaviour> OnInit = null;
    public event UnityAction OnShoot = null;
    public event UnityAction OnPullTrigger = null;
    public event UnityAction OnReleaseTrigger = null;

    public EntityBehaviour EntitySource { get => _entitySource; }
    public WeaponSO WeaponSO { get => _weaponSO; }

    protected virtual void Awake()
    {
        _nextFire = _weaponSO.Stats.FireRate;
    }

    public virtual void Init(EntityBehaviour _entityBehaviour, AmmoHandler _ammoHandler)
    {
        this._ammoHandler = _ammoHandler;
        Init(_entityBehaviour);
        ReloadMagazine();
    }

    public virtual void Init(EntityBehaviour _entityBehaviour)
    {
        _entitySource = _entityBehaviour;
        OnInit?.Invoke(this);
    }


    public virtual void PullTrigger()
    {
        OnPullTrigger?.Invoke();
    }

    public virtual void ReleaseTrigger()
    {
        OnReleaseTrigger?.Invoke();
    }

    public virtual void Shoot()
    {
        _nextFire = 0;
        PrepareProjectile(_weaponSO.ProjectileSO);
        DecreaseAmmo(_weaponSO.ProjectileSO);
        OnShoot?.Invoke();
    }

    private void PrepareProjectile(ProjectileSO _projectileSO)
    {
        for (int i = 0; i < _weaponSO.Stats.ProjectilesPerShot; i++)
        {
            var _position = GeneratePosition(_projectileSO);
            var _rotation = GenerateRotation();
            var _projectile = Instantiate(_projectileSO.Prefab, _position, _rotation);
            var _shootModel = new ShootModel(EntitySource, this, _projectileSO);
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
        var _alignmentTransform = _alignmentOrigin != null ? _alignmentOrigin : Camera.main.transform;
        var _ray = new Ray(_alignmentTransform.position, _alignmentTransform.forward);
        var _targetPoint = Physics.Raycast(_ray, out RaycastHit hit, 99) ? hit.point : _ray.GetPoint(99);
        var _direction = (_targetPoint - _muzzle.position).normalized;

        var _dot = Vector3.Dot(_alignmentTransform.forward, _direction);
        var _isPointBehindMuzzle = _dot < 0.1f;
        if (_isPointBehindMuzzle)
            _direction = (_ray.GetPoint(99) - _muzzle.position).normalized;

        var _spread = _weaponSO.Stats.SpreadAngle;
        var _x = Random.Range(-_spread, _spread);
        var _y = Random.Range(-_spread, _spread);
        return Quaternion.LookRotation(_direction) * Quaternion.Euler(_x, _y, 0f);
    }

    public void SetLookToAlignment(Vector3 _point)
    {
        if (_alignmentOrigin == null) return;
        var _position = _point + Vector3.up * _alignmentOrigin.localPosition.y;
        _alignmentOrigin.LookAt(_position, Vector3.up);
    }

    public float GetPullTriggerTotalTime()
    {
        return _weaponSO.GetPullTriggerTotalTime();
    }

    public float GetTimeUntilAnotherShot()
    {
        return _weaponSO.GetTimeUntilAnotherShot();
    }

    public string GetId()
    {
        return _weaponSO.Id;
    }

    public void ReloadMagazine()
    {
        if (_magazineAmmo >= _weaponSO.Stats.MagazineSize) return;

        var _amountRequired = Mathf.Abs(_magazineAmmo - _weaponSO.Stats.MagazineSize);
        var _amountAvailable = Mathf.Abs(_ammoHandler.GetAmmoQuantity(_weaponSO.ProjectileSO) - _magazineAmmo);

        if (_amountAvailable <= 0) return;

        if (_amountRequired <= _amountAvailable)
        {
            _magazineAmmo += _amountRequired;
        }
        else
        {
            _magazineAmmo += _amountAvailable;
        }
    }

    private void DecreaseAmmo(ProjectileSO _projectileSO)
    {
        if (_ammoHandler == null) return;
        if (_ammoHandler.InfiniteAmmo) return;
        _magazineAmmo -= _weaponSO.Stats.AmmoPerShot;
        _magazineAmmo = Mathf.Clamp(_magazineAmmo, 0, _weaponSO.Stats.MagazineSize);
        _ammoHandler.DecreaseAmmo(_projectileSO, _weaponSO.Stats.AmmoPerShot);
    }

    public bool HasAmmo()
    {
        return _ammoHandler == null || _magazineAmmo >= _weaponSO.Stats.AmmoPerShot || _ammoHandler.InfiniteAmmo;
    }

    public string GetAmmoString()
    {
        return _ammoHandler == null ? $"-" : $"{_ammoHandler.GetAmmoQuantity(_weaponSO.ProjectileSO)}";
    }

    public int GetDamage()
    {
        return _weaponSO.Stats.Damage;
    }

    public virtual void ForceStop()
    {
    }

    //public Vector2 _ammoString = default;
    //private void Update()
    //{
    //    var _totalAmmo = Mathf.Abs(_ammoHandler.GetAmmoQuantity(_weaponSO.ProjectileSO) - _magazineAmmo);
    //    _ammoString = new(_magazineAmmo, _totalAmmo);
    //}
}
