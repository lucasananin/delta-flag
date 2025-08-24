using UnityEngine;

public class BandolierLine : MonoBehaviour
{
    [SerializeField] Transform _shoulderPoint = null;
    [SerializeField] Transform _weaponPoint = null;
    [SerializeField] LineRenderer _line = null;

    private void Awake()
    {
        _line.useWorldSpace = true;
    }

    private void LateUpdate()
    {
        SetPositions();
    }

    [ContextMenu("SetPositions()")]
    public void SetPositions()
    {
        _line.SetPosition(0, _shoulderPoint.position);
        _line.SetPosition(1, _weaponPoint.position);
    }
}
