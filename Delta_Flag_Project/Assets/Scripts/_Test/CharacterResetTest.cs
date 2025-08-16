using UnityEngine;

public class CharacterResetTest : MonoBehaviour
{
    [SerializeField] Transform _parent = null;
    [SerializeField] Transform _child = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        _parent.SetPositionAndRotation(_child.position, _child.rotation);
        _child.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }
}
