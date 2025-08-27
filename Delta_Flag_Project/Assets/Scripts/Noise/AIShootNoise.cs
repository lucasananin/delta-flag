using UnityEngine;

public class AIShootNoise : NoiseEmitter
{
    [Header("// SHOOT NOISE")]
    [SerializeField] AIEntity _entity = null;
    [SerializeField] WeaponBehaviour _weapon = null;

    private void OnEnable()
    {
        _weapon.OnShoot += SendNoise;
    }

    private void OnDisable()
    {
        _weapon.OnShoot -= SendNoise;
    }

    private void SendNoise()
    {
        var _model = new NoiseModel(_entity.GetTargetEntity(), transform, _distance);
        Emit(_model);
    }
}
