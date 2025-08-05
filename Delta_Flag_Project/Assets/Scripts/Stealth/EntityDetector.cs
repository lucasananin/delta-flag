using System.Collections.Generic;
using UnityEngine;

public class EntityDetector : MonoBehaviour
{
    [SerializeField] TagCollectionSO _tags = null;
    [SerializeField] SphereCollider _sphere = null;
    [SerializeField] float _maxDistance = 5f;
    [SerializeField] float _viewAngle = 45f;

    [Header("// READONLY")]
    [SerializeField] List<GameObject> _targets = null;

    private void Awake()
    {
        _sphere.radius = _maxDistance;
    }

    //private void Update()
    //{
    //    if (HasTargetWithin())
    //    {
    //        transform.localScale = Vector3.one * 1.5f;
    //    }
    //}

    public bool HasTargetWithin()
    {
        int _count = _targets.Count;

        for (int i = 0; i < _count; i++)
        {
            var _target = _targets[i];
            var _directionToTarget = (_target.transform.position - transform.position).normalized;

            float _dot = Vector3.Dot(transform.forward, _directionToTarget);
            float _threshold = Mathf.Cos(_viewAngle * 0.5f * Mathf.Deg2Rad);

            return _dot > _threshold;
        }

        return false;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_tags.HasTag(_other.gameObject))
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
}
