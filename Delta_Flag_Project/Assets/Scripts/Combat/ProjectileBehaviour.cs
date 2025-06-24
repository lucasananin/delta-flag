using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour
{
    [Header("// READONLY")]
    [SerializeField] protected ShootModel _shootModel = null;
    [SerializeField] protected ProjectileSO _projectileSO = null;
    [SerializeField] protected float _timeUntilDestroy = 0f;
    [SerializeField] protected float _destroyTimer = 0f;
    [SerializeField] protected List<string> _tagsList = null;

    //private Collider2D[] _explosionResults = new Collider2D[9];
    //private RaycastHit2D[] _obstacleResults = new RaycastHit2D[9];
    //private List<EntityBehaviour> _entitiesFound = new List<EntityBehaviour>();

    //public event System.Action<ShootModel> OnInit = null;
    //public event System.Action<RaycastHit2D> OnRaycastHit = null;
    //public event System.Action OnDestroy_TimerEnd = null;
    //public event System.Action OnDestroy_Stop = null;
    //public event System.Action OnExplode = null;

    const string PROJECTILE_TAG = "Projectile";

    public ShootModel ShootModel { get => _shootModel; }
    public float TimeUntilDestroy { get => _timeUntilDestroy; }

    public virtual void Init(ShootModel _newShootModel)
    {
        _shootModel = _newShootModel;
        _projectileSO = _newShootModel.ProjectileSO;
        //_stats = _newShootModel.WeaponSource.WeaponSO.ProjectileStats;
        //UpdateTagsList();
        SetDestroyTimer();
        //SetDestroyTimer();
    }

    protected void SendInitEvent()
    {
        //OnInit?.Invoke(_shootModel);
    }

    protected void SendRaycastHitEvent(RaycastHit _value)
    {
        //OnRaycastHit?.Invoke(_value);
    }

    protected void CheckDestroyTime()
    {
        _destroyTimer += Time.fixedDeltaTime;

        if (_destroyTimer >= _timeUntilDestroy)
        {
            DestroyByTime();
        }
    }

    protected void SetDestroyTimer()
    {
        //_timeUntilDestroy = Random.Range(_stats.DestroyTimeRange.x, _stats.DestroyTimeRange.y);
        _timeUntilDestroy = 10;
        _destroyTimer = 0f;
    }

    //private bool HasObstacleBetween(Vector3 _origin, Vector3 _targetPoint)
    //{
    //    var _vector = _targetPoint - _origin;
    //    var _rayHitCount = Physics2D.RaycastNonAlloc(_origin, _vector.normalized, _obstacleResults, _vector.magnitude, _projectileSO.ObstacleLayerMask);
    //    bool _hitObstacle = _rayHitCount > 0;
    //    return _hitObstacle;
    //}

    //protected void UpdateTagsList()
    //{
    //    _tagsList.Clear();
    //    _tagsList.AddRange(_shootModel.EntitySource.GetProjectileHitTags());
    //    if (_stats.CanDamageProjectiles) _tagsList.Add(PROJECTILE_TAG);
    //}

    public bool HasHitSource(GameObject _gameobjectHit)
    {
        return _gameobjectHit == _shootModel.EntitySource.gameObject;
    }

    public bool HasAvailableTag(GameObject _gameObjectHit)
    {
        //return GeneralMethods.HasAvailableTag(_gameObjectHit, _tagsList);
        return true;
    }

    //protected IEnumerator DestroyRoutine()
    //{
    //    yield return new WaitForSeconds(_timeUntilDestroy);
    //    DestroyByTime();
    //}

    public void DestroyByTime()
    {
        //OnDestroy_TimerEnd?.Invoke();
        DestroyThis();
    }

    //public void DestroyByStop()
    //{
    //    OnDestroy_Stop?.Invoke();
    //    DestroyThis();
    //}

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
