using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : ProjectileBehaviour
{
    [SerializeField] List<Collider> _collidersHit = default;
    [SerializeField] Vector3 _lastPosition = default;

    [Header("// REFERENCES")]
    [SerializeField] SphereCollider _dummyCollider = null;

    private readonly RaycastHit[] _results = new RaycastHit[9];

    private void FixedUpdate()
    {
        CheckDestroyTime();
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        Vector3 displacement = _projectileSO.MoveSpeed * Time.fixedDeltaTime * transform.forward;
        var _hits = Physics.SphereCastNonAlloc(_lastPosition, _dummyCollider.radius, displacement.normalized, _results, displacement.magnitude, _projectileSO.LayerMask);

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
            break;
        }

        SetLastPosition();
        transform.position += displacement;
    }

    public override void Init(ShootModel _newShootModel)
    {
        base.Init(_newShootModel);
        SetLastPosition();
        _collidersHit.Clear();
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
