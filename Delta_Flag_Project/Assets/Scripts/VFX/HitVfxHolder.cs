using UnityEngine;

public class HitVfxHolder : MonoBehaviour
{
    [SerializeField] ParticleSystem _prefab = null;
    //[SerializeField] bool _block = false;

    public ParticleSystem Prefab { get => _prefab; }
    //public bool Block { get => _block; }
}
