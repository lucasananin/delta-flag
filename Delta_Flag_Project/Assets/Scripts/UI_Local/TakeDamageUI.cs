using UnityEngine;

public class TakeDamageUI : CanvasView
{
    private void OnEnable()
    {
        PlayerHealth.OnPlayerDamaged += Play;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDamaged -= Play;
    }

    private void Play(PlayerHealth obj)
    {
        InstantShow();
        Hide();
    }
}
