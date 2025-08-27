using UnityEngine;

public class ShootNoise : NoiseEmitter
{
    [Header("// SHOOT NOISE")]
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
        var _model = new NoiseModel(_weapon.EntitySource, transform, _distance);
        Emit(_model);
    }
}
