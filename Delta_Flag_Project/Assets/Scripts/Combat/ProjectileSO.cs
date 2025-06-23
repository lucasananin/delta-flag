using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSO", menuName = "Scriptable Objects/ProjectileSO")]
public class ProjectileSO : ScriptableObject
{
    [SerializeField] ProjectileBehaviour _prefab = null;
    [SerializeField] AmmoSO _ammoSO = null;
    [SerializeField] LayerMask _layerMask = default;
    [SerializeField] Vector2 _spawnPositionOffset = default;

    public ProjectileBehaviour Prefab { get => _prefab; private set => _prefab = value; }
    public AmmoSO AmmoSO { get => _ammoSO; private set => _ammoSO = value; }
    public LayerMask LayerMask { get => _layerMask; private set => _layerMask = value; }
    public Vector2 SpawnPositionOffset { get => _spawnPositionOffset; private set => _spawnPositionOffset = value; }
}
