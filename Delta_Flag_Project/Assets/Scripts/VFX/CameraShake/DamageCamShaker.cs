using UnityEngine;

public class DamageCamShaker : CameraShaker
{
    [SerializeField] HealthBehaviour _health = null;
    [SerializeField] float _force = 1f;

    private void OnEnable()
    {
        _health.OnDamageTaken += Shake;
    }

    private void OnDisable()
    {
        _health.OnDamageTaken -= Shake;
    }

    public override void Shake()
    {
        SetShape(Cinemachine.CinemachineImpulseDefinition.ImpulseShapes.Explosion);
        _impulseSource.GenerateImpulse(_force);
    }
}
