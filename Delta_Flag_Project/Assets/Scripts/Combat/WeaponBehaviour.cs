using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] protected WeaponSO _weaponSO = null;
    [SerializeField] protected Transform _muzzle = null;

    [Header("// READONLY")]
    [SerializeField] protected EntityBehaviour _entitySource = null;
    [SerializeField] protected AmmoHandler _ammoHandler = null;
    [SerializeField] protected float _nextFire = 0;

    public virtual void PullTrigger()
    {

    }

    public virtual void ReleaseTrigger()
    {

    }
}
