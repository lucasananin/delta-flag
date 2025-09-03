using UnityEngine;

public class PhysicalProjectile : ProjectileBehaviour
{
    //[SerializeField] List<Collider> _collidersHit = default;

    [Header("// REFERENCES")]
    [SerializeField] Rigidbody _rb = null;
    [SerializeField] SphereCollider _dummyCollider = null;
    //[SerializeField] GameObject _prefab = null;

    private readonly RaycastHit[] _results = new RaycastHit[9];

    private void FixedUpdate()
    {
        CheckDestroyTime();
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        var _displacement = _projectileSO.MoveSpeed * Time.fixedDeltaTime * transform.forward;
        var _hits = Physics.SphereCastNonAlloc(transform.position, _dummyCollider.radius, _displacement.normalized, _results, _displacement.magnitude, _projectileSO.LayerMask);
        var _nextPosition = _rb.position + _displacement;

        for (int i = 0; i < _hits; i++)
        {
            var _raycastHit = _results[i];
            var _colliderHit = _raycastHit.collider;

            if (HasHitSource(_colliderHit.gameObject)) continue;
            //if (_collidersHit.Contains(_colliderHit)) continue;
            if (_colliderHit.TryGetComponent(out HealthBehaviour _healthBehaviour) && !_shootModel.EntitySource.HasOpponentTag(_colliderHit.gameObject)) continue;

            //_collidersHit.Add(_colliderHit);
            TryDamage(_healthBehaviour, _raycastHit);
            SendRaycastHitEvent(_raycastHit);
            DestroyThis();
            //Instantiate(_prefab, _raycastHit.point, Quaternion.identity);
            return;
        }

        _rb.MovePosition(_nextPosition);

        //if (_hits > 1)
        //    Debug.Log($"{_hits}");
    }

    //public override void Init(ShootModel _newShootModel)
    //{
    //    base.Init(_newShootModel);
    //    _collidersHit.Clear();
    //}

    private void TryDamage(HealthBehaviour _healthBehaviour, RaycastHit _raycastHit)
    {
        if (_healthBehaviour == null) return;

        var _damage = _shootModel.GetDamage();
        var _damageModel = new DamageModel(_shootModel.EntitySource, _raycastHit.point, _damage);
        _healthBehaviour.TakeDamage(_damageModel);
    }
}
