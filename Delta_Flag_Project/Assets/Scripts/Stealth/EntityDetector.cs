using System.Collections.Generic;
using UnityEngine;

public class EntityDetector : MonoBehaviour
{
    [SerializeField] TagCollectionSO _targetTags = null;
    [SerializeField] TagCollectionSO _obstacleTags = null;
    [SerializeField] SphereCollider _sphere = null;
    [SerializeField] LayerMask _layerMask = default;
    [SerializeField] float _maxDistance = 5f;
    [SerializeField] float _viewAngle = 45f;

    [Header("// READONLY")]
    [SerializeField] List<GameObject> _targets = null;

    private readonly RaycastHit[] _results = new RaycastHit[9];

    private void Awake()
    {
        _sphere.radius = _maxDistance;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_targetTags.HasTag(_other.gameObject))
        {
            _targets.Add(_other.gameObject);
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_targets.Contains(_other.gameObject))
        {
            _targets.Remove(_other.gameObject);
        }
    }

    public bool HasTargetWithin(out GameObject _targetFound)
    {
        int _count = _targets.Count;

        for (int i = 0; i < _count; i++)
        {
            var _target = _targets[i];
            var _directionToTarget = (_target.transform.position - transform.position).normalized;

            float _dot = Vector3.Dot(transform.forward, _directionToTarget);
            float _threshold = Mathf.Cos(_viewAngle * 0.5f * Mathf.Deg2Rad);

            if (_dot < _threshold) continue;
            if (!CanSeeTarget(_target.transform, transform.position)) continue;

            _targetFound = _target;
            return true;
        }

        _targetFound = null;
        return false;
    }

    public bool CanSeeTarget(Transform _target, Vector3 _point)
    {
        var _vector = _target.position - _point + Vector3.up;
        var _direction = _vector.normalized;
        var _distance = _vector.magnitude;
        var _sphereRadius = 0.25f;
        int _hits = Physics.SphereCastNonAlloc(_point, _sphereRadius, _direction, _results, _distance, _layerMask);

        for (int i = 0; i < _hits; i++)
        {
            var _colliderHit = _results[i].collider;

            if (_colliderHit.gameObject == transform.parent.gameObject) continue;
            if (_obstacleTags.HasTag(_colliderHit.gameObject)) return false;
            if (_colliderHit.gameObject == _target.gameObject) return true;
        }

        return false;
    }
}
