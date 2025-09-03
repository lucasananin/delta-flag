using DG.Tweening;
using UnityEngine;

public class LootDropper : MonoBehaviour
{
    [SerializeField] Transform[] _loots = null;

    [Header("// JUMP TWEEN")]
    [SerializeField] float _jumpPower = 1f;
    [SerializeField] float _jumpDuration = 1f;
    [SerializeField] Ease _ease = default;

    [ContextMenu("Fodase()")]
    private void Fodase()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
        Invoke(nameof(Drop), 1f);
    }

    public void Drop()
    {
        var _randomLoot = _loots[Random.Range(0, _loots.Length)];
        var _instance = Instantiate(_randomLoot, transform.position, Quaternion.identity);
        _instance.
            DOJump(transform.position + transform.forward, _jumpPower, 1, _jumpDuration).
            SetEase(_ease);
    }
}
