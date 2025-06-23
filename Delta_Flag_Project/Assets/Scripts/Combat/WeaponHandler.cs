using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _currentWeapon = null;

    public void PullTrigger()
    {
        _currentWeapon.PullTrigger();
    }

    public void ReleaseTrigger()
    {
        _currentWeapon.ReleaseTrigger();
    }
}
