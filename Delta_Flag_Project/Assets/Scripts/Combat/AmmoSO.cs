using UnityEngine;

[CreateAssetMenu(fileName = "AmmoSO", menuName = "Scriptable Objects/AmmoSO")]
public class AmmoSO : ScriptableObject
{
    [SerializeField] string _id = null;
    [SerializeField] int _maxQuantity = 120;

    public string Id { get => _id; private set => _id = value; }
    public int MaxQuantity { get => _maxQuantity; private set => _maxQuantity = value; }
}

[System.Serializable]
public class AmmoModel
{
    [SerializeField] AmmoSO _so = null;
    [SerializeField] int _amount = 0;

    public int Amount { get => _amount; private set => _amount = value; }

    public void DecreaseQuantity(int _value)
    {
        _amount -= _value;
        _amount = Mathf.Clamp(_amount, 0, _so.MaxQuantity);
    }

    public void RestoreQuantity()
    {
        _amount = _so.MaxQuantity;
    }

    public void RestoreQuantity(int _percentage)
    {
        var _value = _so.MaxQuantity * (_percentage / 100f);
        _amount += Mathf.RoundToInt(_value);
        _amount = Mathf.Clamp(_amount, 0, _so.MaxQuantity);
    }

    public bool HasEnoughQuantity(int _value)
    {
        return _amount >= _value;
    }

    public bool IsFull()
    {
        return _amount >= _so.MaxQuantity;
    }

    public string GetId()
    {
        return _so.Id;
    }

    public float GetNormalizedValue()
    {
        return _amount / (_so.MaxQuantity * 1f);
    }
}
