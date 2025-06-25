using StarterAssets;
using UnityEngine;

public class PlayerWeaponHandler : WeaponHandler
{
    [Header("// REFERENCES")]
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

        if (_input.Swap.action.WasPerformedThisFrame())
        {
            SwapThroughInput(1);
        }
    }
}
