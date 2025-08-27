using UnityEngine;

[System.Serializable]
public class NoiseModel
{
    [Header("// READONLY")]
    [SerializeField] EntityBehaviour _sourceEntity = null;
    [SerializeField] Transform _transform = null;
    [SerializeField] float _distance = 0f;

    public EntityBehaviour SourceEntity { get => _sourceEntity; }
    public Transform Transform { get => _transform; }
    public float Distance { get => _distance; }

    public NoiseModel(EntityBehaviour _sourceEntity, Transform _transform, float _distance)
    {
        this._sourceEntity = _sourceEntity;
        this._transform = _transform;
        this._distance = _distance;
    }
}
