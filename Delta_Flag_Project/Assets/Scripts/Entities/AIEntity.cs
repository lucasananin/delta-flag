using UnityEngine;
using UnityEngine.AI;

public class AIEntity : EntityBehaviour
{
    //[SerializeField] AIPath _aiPath = null;
    [SerializeField] NavMeshAgent _navAgent = null;
    [SerializeField] TagCollectionSO _obstacleTags = null;
    [SerializeField] LayerMask _layerMask = default;
    [SerializeField] float _detectionHeight = 1.5f;

    [Header("// READONLY")]
    [SerializeField] EntityBehaviour _targetEntity = null;
    //[SerializeField] bool _isFleeing = false;
    //[SerializeField] bool _isCowering = false;
    //[SerializeField] bool _isWaitingBullCharge = false;
    //[SerializeField] bool _isBullCharging = false;
    [SerializeField] bool _isTargetOnLineOfSight = false;
    [SerializeField] float _timeUntilSearchPath = 0f;
    [SerializeField] float _searchPathTimer = 0f;

    private readonly RaycastHit[] _results = new RaycastHit[9];

    //public AIPath AiPath { get => _aiPath; private set => _aiPath = value; }
    //public bool IsFleeing { get => _isFleeing; set => _isFleeing = value; }
    //public bool IsCowering { get => _isCowering; set => _isCowering = value; }
    //public bool IsWaitingBullCharge { get => _isWaitingBullCharge; set => _isWaitingBullCharge = value; }
    //public bool IsBullCharging { get => _isBullCharging; set => _isBullCharging = value; }
    public bool IsTargetOnLineOfSight { get => _isTargetOnLineOfSight; private set => _isTargetOnLineOfSight = value; }

    private void Update()
    {
        _isTargetOnLineOfSight = HasTargetEntity() && CanSeeTargetFromPoint(transform.position);
    }

    public void SetTargetEntity(EntityBehaviour _entityValue)
    {
        _targetEntity = _entityValue;
    }

    public EntityBehaviour GetTargetEntity()
    {
        return _targetEntity;
    }

    public bool HasTargetEntity()
    {
        return _targetEntity != null && _targetEntity.IsAlive();
    }

    public Vector3 GetTargetEntityPosition()
    {
        return _targetEntity.transform.position;
    }

    //public Vector3 PickRandomPointAround(float _radius)
    //{
    //    Vector3 _point = Random.insideUnitCircle * _radius;
    //    _point += _aiPath.position;
    //    return _point;
    //}

    public Vector3 PickRandomPointAround(Vector2 _range)
    {
        Vector3 _point = GeneralMethods.GetRandomInCircle(_range.x, _range.y);
        _point += _navAgent.transform.position;
        return _point;
    }

    public Vector3 PickRandomPointNearTarget(Vector2 _minMaxValue)
    {
        //Vector3 _point = GeneralMethods.GetRandomInCircle(_minMaxValue.x, _minMaxValue.y);
        var _point = GeneralMethods.GetRandomInSphere(_minMaxValue.x, _minMaxValue.y);
        var _targetPosition = GetTargetEntityPosition();
        _point += _targetPosition;
        _point.y = _targetPosition.y;
        return _point;
    }

    public Vector3 PickTargetFlank(Vector2 _range, float _distance)
    {
        var _direction = (transform.position - GetTargetEntityPosition()).normalized;
        var _cross = Vector3.Cross(_direction, transform.forward);
        _cross *= Random.Range(0, 2) == 0 ? 1f : -1f;

        Vector3 _point = GeneralMethods.GetRandomInCircle(_range.x, _range.y);
        _point += GetTargetEntityPosition();
        _point += _cross * _distance;
        return _point;
    }

    public Vector3 PickRandomPointAwayFromTarget(Vector2 _range, float _distance)
    {
        Vector3 _point = GeneralMethods.GetRandomInCircle(_range.x, _range.y);
        _point += GetTargetEntityPosition();
        _point += (transform.position - GetTargetEntityPosition()).normalized * _distance;
        return _point;
    }

    public bool IsCloseToTargetEntity(float _minDistance)
    {
        return IsPointCloseToTargetEntity(transform.position, _minDistance);
    }

    public bool IsPointCloseToTargetEntity(Vector3 _point, float _minDistance)
    {
        return GeneralMethods.IsPointCloseToTarget(_point, GetTargetEntityPosition(), _minDistance);
    }

    public void SetAIPathDestination(Vector3 _position)
    {
        //_aiPath.destination = _position;
        //_aiPath.SearchPath();
        _navAgent.SetDestination(_position);
    }

    public void StopAiPath()
    {
        if (!IsMoving()) return;
        SetAIPathDestination(transform.position);
    }

    public bool HasReachedPathEnding()
    {
        //return !_aiPath.pathPending && (_aiPath.reachedEndOfPath || !_aiPath.hasPath);
        return !_navAgent.hasPath || (_navAgent.pathPending && _navAgent.velocity == Vector3.zero);
    }

    public bool CanSeeTargetFromPoint(Vector3 _point)
    {
        var _offset = GetTargetEntityPosition() - _point;
        var _direction = _offset.normalized;
        var _distance = _offset.magnitude;
        var _sphereRadius = 0.25f;
        int _hits = Physics.SphereCastNonAlloc(_point + GetDetectionOffset(), _sphereRadius, _direction, _results, _distance, _layerMask);

        for (int i = 0; i < _hits; i++)
        {
            var _colliderHit = _results[i].collider;

            if (_colliderHit.gameObject == gameObject) continue;
            if (_obstacleTags.HasTag(_colliderHit.gameObject)) return false;
            if (_colliderHit.gameObject == _targetEntity.gameObject) return true;
        }

        return false;
    }

    public Vector3 GetDetectionOffset()
    {
        return Vector3.up * _detectionHeight;
    }

    public bool IsTargetEntity(GameObject _gameObject)
    {
        return _gameObject == _targetEntity.gameObject;
    }

    public bool IsWaitingToSearchPath()
    {
        _searchPathTimer += Time.deltaTime;
        return _searchPathTimer < _timeUntilSearchPath;
    }

    public void ResetTimeUntilSearchPath()
    {
        Vector2 _minMaxValue = GetEntitySO<AIEntitySO>().MoveRateRange;
        _timeUntilSearchPath = Random.Range(_minMaxValue.x, _minMaxValue.y);
        _searchPathTimer = 0;
    }

    public override bool IsMoving()
    {
        return _navAgent.velocity != Vector3.zero;
    }
}
