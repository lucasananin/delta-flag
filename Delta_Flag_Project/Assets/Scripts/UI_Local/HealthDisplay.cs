using TMPro;
using UnityEngine;
//using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    //[SerializeField] Image _fill = null;
    [SerializeField] TextMeshProUGUI _text = null;

    [Header("// READONLY")]
    [SerializeField] HealthBehaviour _health = null;

    private void Awake()
    {
        var _playerHealth = FindFirstObjectByType<PlayerHealth>();
        Init(_playerHealth);
    }

    private void OnDisable()
    {
        _health.OnHurt -= UpdateVisuals;
        _health.OnRestored -= UpdateVisuals;
        _health.OnDead -= UpdateVisuals;
    }

    public void Init(HealthBehaviour _health)
    {
        this._health = _health;
        this._health.OnHurt += UpdateVisuals;
        this._health.OnRestored += UpdateVisuals;
        this._health.OnDead += UpdateVisuals;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        var _normalizedValue = _health.GetNormalizedValue();
        //_fill.fillAmount = _normalizedValue;

        var _percentageValue = Mathf.RoundToInt(_normalizedValue * 100);
        _text.text = $"{_percentageValue}%";
    }
}
