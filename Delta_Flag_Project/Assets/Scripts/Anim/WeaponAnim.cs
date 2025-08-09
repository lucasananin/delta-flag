using UnityEngine;

public class WeaponAnim : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _weapon = null;
    [SerializeField] Animator _anim = null;

    private void OnEnable()
    {
        _weapon.OnShoot += TriggerFire;
    }

    private void OnDisable()
    {
        _weapon.OnShoot -= TriggerFire;
    }

    private void TriggerFire()
    {
        _anim.SetTrigger("fire");
    }
}
