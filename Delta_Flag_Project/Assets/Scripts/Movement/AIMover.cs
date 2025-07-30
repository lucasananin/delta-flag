using UnityEngine;
using UnityEngine.AI;

public class AIMover : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent = null;
    [SerializeField] float _rotationSpeed = 10f;
    [SerializeField] float _minRotationMagnitude = 0.1f;

    [Header("// DEBUG")]
    [SerializeField] Vector2 _moveRateRange = new(1f, 3f);
    [SerializeField] float _moveRate = 0;
    [SerializeField] float _nextMove = 0;

    private void Awake()
    {
        _agent.updateRotation = false;
    }

    private void Update()
    {
        if (!HasReachedDestination()) return;

        _nextMove += Time.deltaTime;

        if (_nextMove > _moveRate)
        {
            _nextMove = 0;
            _moveRate = Random.Range(_moveRateRange.x, _moveRateRange.y);
            MoveToRandomDestination();
        }
    }

    private void LateUpdate()
    {
        RotateToMovement();
    }

    public void MoveToRandomDestination()
    {
        var _randomPoint = NavMeshUtils.GetRandomNavMeshPoint(transform.position, 10f);
        SetDestination(_randomPoint);
        Debug.Log($"MoveToRandomDestination");
    }

    public void SetDestination(Vector3 _position)
    {
        _agent.SetDestination(_position);
    }

    public void RotateToMovement()
    {
        if (_agent.velocity.sqrMagnitude > _minRotationMagnitude)
        {
            var _forward = new Vector3(_agent.velocity.x, 0, _agent.velocity.z).normalized;

            if (_forward != Vector3.zero)
            {
                Quaternion _targetRotation = Quaternion.LookRotation(_forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _rotationSpeed);
            }
        }
    }

    public bool HasReachedDestination()
    {
        return !_agent.hasPath || (_agent.pathPending && _agent.velocity == Vector3.zero);
    }

    public Vector3 GetVelocity()
    {
        return _agent.velocity;
    }
}
