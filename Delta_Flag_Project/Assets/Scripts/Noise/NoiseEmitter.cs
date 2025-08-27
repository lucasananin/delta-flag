using UnityEngine;
using UnityEngine.Events;

public class NoiseEmitter : MonoBehaviour
{
    [SerializeField] protected float _distance = 10f;
    [SerializeField] protected UnityEvent<NoiseModel> _onEmitted = null;

    public static event UnityAction<NoiseModel> OnEmitted = null;

    public void Emit(NoiseModel _model)
    {
        _onEmitted?.Invoke(_model);
        OnEmitted?.Invoke(_model);
    }
}
