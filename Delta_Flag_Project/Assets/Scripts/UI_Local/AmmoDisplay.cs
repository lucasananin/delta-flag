using TMPro;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text = null;

    [Header("// READONLY")]
    [SerializeField] PlayerWeaponHandler _playerWeaponHandler = null;
    [SerializeField] WeaponBehaviour _weapon = null;

    private void Awake()
    {
        _playerWeaponHandler = FindFirstObjectByType<PlayerWeaponHandler>();
    }

    private void OnEnable()
    {
        _playerWeaponHandler.OnWeaponSet += SetWeapon;
    }

    private void OnDisable()
    {
        _playerWeaponHandler.OnWeaponSet -= SetWeapon;
    }

    private void LateUpdate()
    {
        if (_weapon != null)
        {
            _text.text = $"{_weapon.GetAmmoString()}";
        }
    }

    private void SetWeapon(WeaponBehaviour _weaponValue)
    {
        _weapon = _weaponValue;
    }
}
