using DG.Tweening;
using UnityEngine;

public class BloodPoolVFX : MonoBehaviour
{
    [SerializeField] Transform _transform = null;
    [SerializeField] Transform _origin = null;
    [SerializeField] SpriteRenderer _renderer = null;
    [SerializeField] LayerMask _layerMask = default;
    [SerializeField] Vector3 _offset = new(0f, 0.01f, 0f);
    [Space]
    [SerializeField] Vector2 _scaleRange = new(1.5f, 2f);
    [SerializeField] float _scaleDuration = 5f;
    [SerializeField] Vector2 _rotationRange = new(0f, 360f);
    [SerializeField] Sprite[] _sprites = null;

    private readonly RaycastHit[] _results = new RaycastHit[3];

    private void Awake()
    {
        _transform.localScale = Vector3.zero;
    }

    public void PlayWithDelay(float _delay)
    {
        Invoke(nameof(Play), _delay);
    }

    public void Play()
    {
        var _hits = Physics.RaycastNonAlloc(_origin.position, Vector3.down, _results, 1f, _layerMask);

        for (int i = 0; i < _hits; i++)
        {
            _transform.position = _results[i].point + _offset;
            break;
        }

        _transform.DOScale(Random.Range(_scaleRange.x, _scaleRange.y), _scaleDuration);
        _transform.rotation = Quaternion.Euler(0f, Random.Range(_rotationRange.x, _rotationRange.y), 0f);
        _renderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
    }
}
