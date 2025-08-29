using UnityEngine;

public class RotateFX : MonoBehaviour
{
    [SerializeField] Transform _transform = null;
    [SerializeField] Vector3 _axis = Vector3.up;
    [SerializeField] float _twistsPerSecond = 1f;

    private void LateUpdate()
    {
        var _euler = GetRotationSpeed() * Time.deltaTime * _axis;
        _transform.Rotate(_euler);
    }

    private float GetRotationSpeed()
    {
        return _twistsPerSecond * 360f;
    }
}
