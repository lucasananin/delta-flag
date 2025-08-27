using UnityEngine;
using UnityEngine.Events;

public class NoiseListener : MonoBehaviour
{
    [SerializeField] float _distance = 10f;

    public event UnityAction<NoiseModel> OnHeardSomething = null;

    private void OnEnable()
    {
        NoiseEmitter.OnEmitted += CheckNoise;
    }

    private void OnDisable()
    {
        NoiseEmitter.OnEmitted -= CheckNoise;
    }

    private void CheckNoise(NoiseModel _model)
    {
        var _sqrMagnitude = (_model.Transform.position - transform.position).sqrMagnitude;
        var _totalDistance = _distance + _model.Distance;

        if (_sqrMagnitude < _totalDistance * _totalDistance)
        {
            Debug.Log($"I {gameObject.name} heard!");
            OnHeardSomething?.Invoke(_model);
        }
    }
}
