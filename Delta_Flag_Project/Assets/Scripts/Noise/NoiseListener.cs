using UnityEngine;
using UnityEngine.Events;

public class NoiseListener : MonoBehaviour
{
    [SerializeField] AIEntity _entity = null;
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
        if (_entity.HasTargetEntity()) return;
        if (!_entity.IsAlive()) return;

        var _sqrMagnitude = (_model.Transform.position - transform.position).sqrMagnitude;
        var _totalDistance = _distance + _model.Distance;

        if (_sqrMagnitude < _totalDistance * _totalDistance)
        {
            _entity.SetTargetEntity(_model.SourceEntity);
            //Debug.Log($"I {gameObject.name} heard!");
        }
    }
}
