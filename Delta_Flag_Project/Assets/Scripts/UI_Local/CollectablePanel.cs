using UnityEngine;

public class CollectablePanel : MonoBehaviour
{
    [SerializeField] CollectableUISlot _prefab = null;
    [SerializeField] RectTransform _content = null;

    private void OnEnable()
    {
        CollectableBehaviour.OnAnyCollected += InitSlot;
    }

    private void OnDisable()
    {
        CollectableBehaviour.OnAnyCollected -= InitSlot;
    }

    private void InitSlot(CollectableBehaviour _collectable, CollectableAgent _agent)
    {
        var _instance = Instantiate(_prefab, _content);
        _instance.Init(_collectable.Description);
    }
}
