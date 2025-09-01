using UnityEngine;
using UnityEngine.Events;

public class CollectableBehaviour : MonoBehaviour
{
    [SerializeField] string _description = null;
    [SerializeField] UnityEvent<CollectableAgent> _onCollected = null;

    public string Description { get => _description;}

    public event UnityAction<CollectableAgent> OnCollected = null;
    public static event UnityAction<CollectableBehaviour, CollectableAgent> OnAnyCollected = null;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.TryGetComponent(out CollectableAgent _agent))
        {
            Collect(_agent);
        }
    }

    public virtual void Collect(CollectableAgent _agent)
    {
        _onCollected?.Invoke(_agent);
        OnCollected?.Invoke(_agent);
        OnAnyCollected?.Invoke(this, _agent);
        Destroy(gameObject);
    }

    public virtual string GetString()
    {
        return string.Empty;
    }
}
