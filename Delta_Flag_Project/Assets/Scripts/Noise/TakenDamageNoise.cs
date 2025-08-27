using UnityEngine;

public class TakenDamageNoise : NoiseEmitter
{
    [Header("// HEALTH")]
    [SerializeField] HealthBehaviour _health = null;

    private void OnEnable()
    {
        _health.OnDamageTaken += SendNoise;
    }

    private void OnDisable()
    {
        _health.OnDamageTaken -= SendNoise;
    }

    private void SendNoise()
    {
        var _sourceEntity = _health.LastDamageModel.EntitySource;
        var _model = new NoiseModel(_sourceEntity, transform, _distance);
        Emit(_model);
    }
}
