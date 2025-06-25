using UnityEngine;

public abstract class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] protected WeaponSO _weaponSO = null;
    [SerializeField] protected Transform _muzzle = null;

    [Header("// READONLY")]
    [SerializeField] protected EntityBehaviour _entitySource = null;
    [SerializeField] protected AmmoHandler _ammoHandler = null;
    [SerializeField] protected float _nextFire = 0;
    [SerializeField] protected int _magazineAmmo = 0;

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
        DecreaseAmmo(_weaponSO.ProjectileSO);
        //OnShoot?.Invoke();
    }

    private void PrepareProjectile(ProjectileSO _projectileSO)
    {
        for (int i = 0; i < _weaponSO.Stats.ProjectilesPerShot; i++)
        {
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

    public Vector2 _ammoString = default;

    private void Update()
    {
        var _b = Mathf.Abs(_ammoHandler.GetAmmoQuantity(_weaponSO.ProjectileSO) - _magazineAmmo);
        _ammoString = new(_magazineAmmo, _b);
    }

    private void DecreaseAmmo(ProjectileSO _projectileSO)
    {
        if (_ammoHandler == null) return;
        _magazineAmmo -= _weaponSO.Stats.AmmoPerShot;
        _magazineAmmo = Mathf.Clamp(_magazineAmmo, 0, _weaponSO.Stats.MagazineSize);
        _ammoHandler.DecreaseAmmo(_projectileSO, _weaponSO.Stats.AmmoPerShot);
        //_ammoHandler.DecreaseAmmo(_projectileSO, _weaponSO);
    }

    public bool HasAmmo()
    {
        return _ammoHandler == null || _magazineAmmo >= _weaponSO.Stats.AmmoPerShot;
        //return _ammoHandler == null || _ammoHandler.HasAmmo(_weaponSO.ProjectileSO, _weaponSO);
    }

    public string GetAmmoString()
    {
        return _ammoHandler == null ? $"-" : $"{_ammoHandler.GetAmmoQuantity(_weaponSO.ProjectileSO)}";
    }

    public int GetDamage()
    {
        return _weaponSO.Stats.Damage;
    }
}
