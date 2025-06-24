using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] protected WeaponBehaviour _currentWeapon = null;

    // init weapon

    public void PullTrigger()
    {
        _currentWeapon.PullTrigger();
    }

    public void ReleaseTrigger()
    {
        _currentWeapon.ReleaseTrigger();
    }
}
