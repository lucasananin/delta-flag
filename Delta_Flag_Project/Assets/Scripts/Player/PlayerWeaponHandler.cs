using StarterAssets;
using UnityEngine;

public class PlayerWeaponHandler : WeaponHandler
{
    [SerializeField] StarterAssetsInputs _input = null;

    private void Update()
    {
        if (_input.Shoot.action.WasPerformedThisFrame())
        {
            PullTrigger();
        }
        else if (_input.Shoot.action.WasReleasedThisFrame())
        {
            ReleaseTrigger();
        }
    }
}
