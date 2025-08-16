using UnityEngine;

public class WeaponPushback : MonoBehaviour
{
    [SerializeField] Transform _transform = null;
    [SerializeField] LayerMask _layerMask = default;
    [SerializeField] float _maxPushback = 2f;
    [SerializeField] float _pushbackSpeed = 50f;
    [SerializeField] float _checkDistance = 1f;
    [SerializeField] float _checkRadius = 0.2f;

    [Header("// READONLY")]
    [SerializeField] Vector3 _defaultLocalPosition;

    private void Awake()
    {
        _defaultLocalPosition = _transform.localPosition;
    }

    private void LateUpdate()
    {
        var _cameraTransform = Camera.main.transform;
        var _cameraPos = _cameraTransform.position;
        var _cameraForward = _cameraTransform.forward;
        var _pushbackAmount = 0f;

        if (Physics.SphereCast(_cameraPos, _checkRadius, _cameraForward, out RaycastHit hit, _checkDistance, _layerMask))
        {
            float _distance = hit.distance;
            _pushbackAmount = Mathf.Clamp(_checkDistance - _distance, 0f, _maxPushback);
        }

        var _targetLocalPos = _defaultLocalPosition - new Vector3(0f, 0f, _pushbackAmount);
        _transform.localPosition = Vector3.MoveTowards(_transform.localPosition, _targetLocalPos, Time.deltaTime * _pushbackSpeed);
    }
}
