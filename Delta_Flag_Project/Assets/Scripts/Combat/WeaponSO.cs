using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] string _id = null;
    [SerializeField] WeaponBehaviour _weaponPrefab = null;
    [SerializeField] ProjectileSO _projectileSO = null;
    [SerializeField] WeaponStats _stats = null;

    public WeaponBehaviour WeaponPrefab { get => _weaponPrefab; private set => _weaponPrefab = value; }
    public string Id { get => _id; private set => _id = value; }
    public ProjectileSO ProjectileSO { get => _projectileSO; private set => _projectileSO = value; }
    public WeaponStats Stats { get => _stats; private set => _stats = value; }
}

[System.Serializable]
public class WeaponStats
{
    [SerializeField, Range(1, 12)] int _damage = 1;
    [Space]
    [SerializeField, Range(0.01f, 3f)] float _fireRate = 0.1f;
    [SerializeField, Range(0.01f, 3f)] float _burstRate = 1f;
    [SerializeField, Range(1, 12)] int _shotsPerBurst = 1;
    [Space]
    [SerializeField, Range(0, 45)] float _spreadAngle = 0;
    [SerializeField, Range(1, 12)] int _projectilesPerShot = 1;
    [SerializeField, Range(1, 12)] int _ammoPerShot = 1;

    public int Damage { get => _damage; private set => _damage = value; }
    public float FireRate { get => _fireRate; private set => _fireRate = value; }
    public float SpreadAngle { get => _spreadAngle; set => _spreadAngle = value; }
    public int ProjectilesPerShot { get => _projectilesPerShot; private set => _projectilesPerShot = value; }
    public int AmmoPerShot { get => _ammoPerShot; set => _ammoPerShot = value; }
    public float BurstRate { get => _burstRate; private set => _burstRate = value; }
    public int ShotsPerBurst { get => _shotsPerBurst; set => _shotsPerBurst = value; }
}
