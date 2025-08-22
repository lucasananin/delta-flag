using UnityEngine;

public class CapFlyVFX : MonoBehaviour
{
    [SerializeField] Transform _headTop = null;
    [SerializeField] GameObject _defaultCap = null;
    [SerializeField] Rigidbody _physicsCap = null;
    [SerializeField] Collider _capCollider = null;
    [Space]
    [SerializeField] Vector2 _forceRange = new(3f, 5f);
    [SerializeField] Vector2 _xForceRange = new(0.4f, 0.6f);
    [SerializeField] Vector2 _torqueRange = new(1f, 2f);

    private void Awake()
    {
        _physicsCap.gameObject.SetActive(false);
    }

    public void Play()
    {
        _defaultCap.SetActive(false);
        _physicsCap.gameObject.SetActive(true);
        _physicsCap.transform.position = _headTop.position;

        var _yForce = Random.Range(_forceRange.x, _forceRange.y);
        var _xForce = Random.Range(_xForceRange.x, _xForceRange.y) * (Random.Range(0, 2) == 1 ? 1 : -1);
        var _zForce = Random.Range(_xForceRange.x, _xForceRange.y) * (Random.Range(0, 2) == 1 ? 1 : -1);
        var _force = new Vector3(_xForce, _yForce, _zForce);
        _physicsCap.AddForce(_force, ForceMode.Impulse);

        var _torque = (transform.right + transform.forward) * Random.Range(_torqueRange.x, _torqueRange.y);
        _physicsCap.AddTorque(_torque, ForceMode.Impulse);

        Invoke(nameof(DisablePhysics), 3f);
    }

    private void DisablePhysics()
    {
        _physicsCap.isKinematic = true;
        _capCollider.enabled = false;
    }
}
