using UnityEngine;

public class HitVfxHolder : MonoBehaviour
{
    [SerializeField] ParticleSystem _prefab = null;

    public ParticleSystem Prefab { get => _prefab; }
}
