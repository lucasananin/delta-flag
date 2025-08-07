using UnityEngine;
using UnityEngine.AI;

public class AIMover : MonoBehaviour
{
    [SerializeField] NavMeshAgent _navAgent = null;
    [SerializeField] float _rotationSpeed = 10f;
    [SerializeField] float _minRotationMagnitude = 0.1f;

    //[Header("// DEBUG")]
    //[SerializeField] Vector2 _moveRateRange = new(1f, 3f);
    //[SerializeField] float _moveRate = 0;
    //[SerializeField] float _nextMove = 0;

    private void Awake()
    {
        _navAgent.updateRotation = false;
    }

    //private void Update()
    //{
    //    if (!HasReachedDestination()) return;

    //    _nextMove += Time.deltaTime;

    //    if (_nextMove > _moveRate)
    //    {
    //        _nextMove = 0;
    //        _moveRate = Random.Range(_moveRateRange.x, _moveRateRange.y);
    //        MoveToRandomDestination();
    //    }
    //}

    //private void LateUpdate()
    //{
    //    RotateToMovement();
    //}

    public void MoveToRandomDestination()
    {
        var _randomPoint = NavMeshUtils.GetRandomNavMeshPoint(transform.position, 10f);
        SetDestination(_randomPoint);
    }

    public void SetDestination(Vector3 _position)
    {
        _navAgent.SetDestination(_position);
    }

    public void Stop()
    {
        _navAgent.isStopped = true;
    }

    public void RotateToMovement()
    {
        if (_navAgent.velocity.sqrMagnitude > _minRotationMagnitude)
        {
            var _movementDirection = new Vector3(_navAgent.velocity.x, 0, _navAgent.velocity.z).normalized;
            RotateTo(_movementDirection);
        }
    }

    public void RotateTo(Vector3 _direction)
    {
        if (_direction != Vector3.zero)
        {
            Quaternion _targetRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _rotationSpeed);
        }
    }

    public bool HasReachedDestination()
    {
        bool _reachedEndOfPath = GeneralMethods.IsPointCloseToTarget(transform.position, _navAgent.pathEndPosition, 0.1f);
        return !_navAgent.pathPending && (_reachedEndOfPath || !_navAgent.hasPath);
    }

    public Vector3 GetVelocity()
    {
        return _navAgent.velocity;
    }
}
