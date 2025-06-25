using UnityEngine;

public class WeaponWallChecker : MonoBehaviour
{
    [SerializeField] Transform _origin = null;
    [SerializeField] LayerMask _layerMask = default;
    [SerializeField] float _radius = 0.1f;
    [SerializeField] float _maxDistance = 1f;

    [SerializeField] Animator _anim = null;
    [SerializeField] bool _isHolding = false;

    private void Update()
    {
        if (Physics.SphereCast(_origin.position, _radius, _origin.forward, out RaycastHit _hitInfo, _maxDistance, _layerMask))
        {
            Debug.Log($"hit {_hitInfo.collider.name}", this);
            _anim.Play("Hold");
            _isHolding = true;
        }
        else
        {
            if (!_isHolding) return;
            _anim.Play("idle");
            _isHolding = false;
        }
    }
}
