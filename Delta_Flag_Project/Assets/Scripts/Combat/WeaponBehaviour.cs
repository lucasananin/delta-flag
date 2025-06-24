using UnityEngine;

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
            var _position = _muzzle.position;
            var _rotation = _muzzle.rotation;
            //var _parent = _entitySource.transform;
            var _projectile = Instantiate(_projectileSO.Prefab, _position, _rotation/*, _parent*/);
            var _shootModel = new ShootModel(_entitySource, this, _projectileSO);
            _projectile.Init(_shootModel);
        }
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
