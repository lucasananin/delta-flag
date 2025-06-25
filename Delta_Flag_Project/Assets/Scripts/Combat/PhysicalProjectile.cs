using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : ProjectileBehaviour
{
    [SerializeField] List<Collider> _collidersHit = default;
    [SerializeField] Vector3 _lastPosition = default;

    [Header("// REFERENCES")]
    [SerializeField] Rigidbody _rb = null;
    [SerializeField] SphereCollider _dummyCollider = null;

    private readonly RaycastHit[] _results = new RaycastHit[9];

    private void FixedUpdate()
    {
        CheckDestroyTime();
        CheckCollisions();
    }

    //private void LateUpdate()
    //{
    //    CheckCollisions();
    //}

    private void CheckCollisions()
    {
        //Vector3 currentPosition = transform.position;
        //Vector3 moveDirection = transform.forward;
        //Vector3 displacement = _projectileSO.MoveSpeed * Time.fixedDeltaTime * moveDirection;

        //var _h = Physics.SphereCastNonAlloc(_lastPosition, _dummyCollider.radius, displacement.normalized, _results, displacement.magnitude, _projectileSO.LayerMask);
        ////var _r = Physics.SphereCastAll(_lastPosition, _dummyCollider.radius, displacement.normalized, displacement.magnitude, _projectileSO.LayerMask);

        //if (_h > 0)
        //{
        //    //Debug.Log($"{_h}");
        //    for (int i = 0; i < _h; i++)
        //    {
        //        var _raycastHit = _results[i];
        //        var _colliderHit = _raycastHit.collider;

        //        if (HasHitSource(_colliderHit.gameObject)) continue;
        //        if (_collidersHit.Contains(_colliderHit)) continue;
        //        //if (_colliderHit.TryGetComponent(out HealthBehaviour _healthBehaviour) && !HasAvailableTag(_colliderHit.gameObject)) continue;

        //        _collidersHit.Add(_colliderHit);
        //        //TryDamage(_healthBehaviour, _raycastHit);
        //        SendRaycastHitEvent(_raycastHit);
        //        DestroyThis();
        //        Debug.Log($"hit");
        //        break;
        //    }
        //}
        //else
        //{
        //    //Debug.Log($"{_h}");
        //}

        //transform.position += displacement;
        //_lastPosition = transform.position;

        //if (_r.Length > 0)
        //{
        //    Debug.Log($"r.length = {_r.Length}");
        //}
        //else
        //{
        //    Debug.Log($"r.length = {_r.Length}");
        //}

        // Sphere cast from previous to new position
        //if (Physics.SphereCast(_lastPosition, _dummyCollider.radius, displacement.normalized, out RaycastHit hit, displacement.magnitude, _projectileSO.LayerMask))
        //{
        //    //Debug.Log("Hit " + hit.collider.name);
        //    Debug.Log($"hit");
        //    // Optionally: place the projectile at hit.point
        //    //transform.position = hit.point;
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    // No hit, continue moving
        //    transform.position += displacement;
        //    _lastPosition = transform.position;
        //}

        //return;

        //float _distance = Vector2.Distance(_lastPosition, transform.position);
        Vector3 displacement = _projectileSO.MoveSpeed * Time.fixedDeltaTime * transform.forward;
        //var _hits = Physics.SphereCastNonAlloc(_lastPosition, _dummyCollider.radius, transform.forward, _results, _distance, _projectileSO.LayerMask);
        var _hits = Physics.SphereCastNonAlloc(_lastPosition, _dummyCollider.radius, displacement.normalized, _results, displacement.magnitude, _projectileSO.LayerMask);
        //Debug.Log($"{_hits}");

        for (int i = 0; i < _hits; i++)
        {
            var _raycastHit = _results[i];
            var _colliderHit = _raycastHit.collider;

            if (HasHitSource(_colliderHit.gameObject)) continue;
            if (_collidersHit.Contains(_colliderHit)) continue;
            //if (_colliderHit.TryGetComponent(out HealthBehaviour _healthBehaviour) && !HasAvailableTag(_colliderHit.gameObject)) continue;

            _collidersHit.Add(_colliderHit);
            //TryDamage(_healthBehaviour, _raycastHit);
            SendRaycastHitEvent(_raycastHit);
            DestroyThis();
            Debug.Log($"hit");
            break;
        }

        SetLastPosition();
        transform.position += displacement;

        //if (_hits == 0)
        //    SetLastPosition();
    }

    public override void Init(ShootModel _newShootModel)
    {
        base.Init(_newShootModel);
        SetLastPosition();
        //_rb.linearVelocity = transform.forward * _projectileSO.MoveSpeed;
        //_rb.AddForce(transform.forward * _projectileSO.MoveSpeed, ForceMode.Impulse);
        _collidersHit.Clear();
        Debug.Log($"shoot");
    }

    //private void TryDamage(HealthBehaviour _healthBehaviour, RaycastHit2D _raycastHit)
    //{
    //    if (_healthBehaviour is null) return;

    //    var _damage = _shootModel.GetDamage();
    //    var _damageModel = new DamageModel(_shootModel.EntitySource, _raycastHit.point, _damage);
    //    _healthBehaviour.TakeDamage(_damageModel);
    //}

    private void SetLastPosition()
    {
        _lastPosition = transform.position;
    }
}
