using UnityEngine;

public class HitVfx : MonoBehaviour
{
    [SerializeField] ProjectileBehaviour _projectileBehaviour = null;
    [SerializeField] ParticleSystem _defaultVfx = null;

    private void OnValidate()
    {
        _projectileBehaviour = GetComponent<ProjectileBehaviour>();
    }

    private void OnEnable()
    {
        _projectileBehaviour.OnRaycastHit += SpawnVfx;
    }

    private void OnDisable()
    {
        _projectileBehaviour.OnRaycastHit -= SpawnVfx;
    }

    private void SpawnVfx(RaycastHit _hitInfo)
    {
        var _holder = _hitInfo.collider.GetComponent<HitVfxHolder>();
        var _prefab = _holder == null ? _defaultVfx : _holder.Prefab;
        var _instance = Instantiate(_prefab, _hitInfo.point, Quaternion.identity);
        _instance.transform.forward = _hitInfo.normal;
    }
}
