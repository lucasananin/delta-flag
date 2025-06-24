using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] WeaponBehaviour _weaponPrefab = null;
    [SerializeField] string _id = null;
    [SerializeField] string _displayName = null;
    [SerializeField] ProjectileSO _projectileSO = null;
    [SerializeField] WeaponStats _stats = null;

    public WeaponBehaviour WeaponPrefab { get => _weaponPrefab; private set => _weaponPrefab = value; }
    public string Id { get => _id; private set => _id = value; }
    public string DisplayName { get => _displayName; private set => _displayName = value; }
    public ProjectileSO ProjectileSO { get => _projectileSO; private set => _projectileSO = value; }
    public WeaponStats Stats { get => _stats; private set => _stats = value; }
}

[System.Serializable]
public class WeaponStats
{
    [SerializeField, Range(0, 12)] int _damage = 1;
    [SerializeField, Range(0.01f, 9f)] float _fireRate = 0.1f;
    [SerializeField, Range(0, 360)] float _shootAngle = 0;
    [SerializeField, Range(1, 36)] int _projectilesPerShot = 1;
    [SerializeField, Range(0, 9)] int _ammoPerShot = 1;
    [SerializeField, Range(0f, 9f)] float _burstRate = 0f;
    [SerializeField, Range(0, 9)] int _shotsPerBurst = 0;

    public int Damage { get => _damage; private set => _damage = value; }
    public float FireRate { get => _fireRate; private set => _fireRate = value; }
    public float SpreadAngle { get => _shootAngle; set => _shootAngle = value; }
    public int ProjectilesPerShot { get => _projectilesPerShot; private set => _projectilesPerShot = value; }
    public int AmmoPerShot { get => _ammoPerShot; set => _ammoPerShot = value; }
    public float BurstRate { get => _burstRate; private set => _burstRate = value; }
    public int ShotsPerBurst { get => _shotsPerBurst; set => _shotsPerBurst = value; }
}
