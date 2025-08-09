using UnityEngine;

public class RotateProjectileFX : MonoBehaviour
{
    //[SerializeField] Rigidbody _rb = null;
    [SerializeField] Transform _transform = null;
    [SerializeField] float _twistsPerSecond = 1f;
    [SerializeField] Vector2 _yRange = new(-1f, 1f);
    //[SerializeField] float _defaultMagnitude = 20f;

    private void LateUpdate()
    {
        var _axis = new Vector3(1, Random.Range(_yRange.x, _yRange.y), 0);
        var _euler = GetRotationSpeed() * Time.deltaTime * _axis;
        _transform.Rotate(_euler);
    }

    private float GetRotationSpeed()
    {
        return _twistsPerSecond * 360f /** (_rb.linearVelocity.magnitude / _defaultMagnitude)*/;
    }
}
