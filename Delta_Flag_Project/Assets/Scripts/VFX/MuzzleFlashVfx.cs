using System.Collections;
using UnityEngine;

public class MuzzleFlashVfx : MonoBehaviour
{
    [SerializeField] WeaponBehaviour _weapon = null;
    [SerializeField] GameObject _fx = null;
    [SerializeField] float _duration = 0.05f;
    [SerializeField] float _maxAngle = 15f;
    [SerializeField] float _angleOffset = 45f;
    [SerializeField] Vector2 _scaleRange = new(0.8f, 1.2f);

    [Header("// REAONLY")]
    [SerializeField] Vector3 _defaultScale = default;

    private void Awake()
    {
        _defaultScale = _fx.transform.localScale;
        _fx.SetActive(false);
    }

    private void OnEnable()
    {
        _weapon.OnShoot += Play;
    }

    private void OnDisable()
    {
        _weapon.OnShoot -= Play;
    }

    private void Play()
    {
        StartCoroutine(Play_Routine());
    }

    private IEnumerator Play_Routine()
    {
        _fx.SetActive(true);
        float _randomZ = Random.Range(-_maxAngle, _maxAngle);
        _fx.transform.localRotation = Quaternion.Euler(0f, 0f, _randomZ + _angleOffset);
        _fx.transform.localScale = _defaultScale * Random.Range(_scaleRange.x, _scaleRange.y);

        yield return new WaitForSeconds(_duration);
        _fx.SetActive(false);
    }
}
