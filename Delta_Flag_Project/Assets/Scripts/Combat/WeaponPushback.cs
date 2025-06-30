using UnityEngine;

public class WeaponPushback : MonoBehaviour
{
    public Transform weaponTransform; // Reference to weapon or arms
    public float maxPushback = 0.3f;  // How far back weapon can be pushed
    public float pushbackSpeed = 10f; // How fast it moves back/forward
    public float checkDistance = 1f;  // Distance to start pushing back
    public LayerMask wallMask;        // Only collide with walls or environment

    private Vector3 defaultLocalPosition;

    void Start()
    {
        if (weaponTransform == null)
            weaponTransform = transform;

        defaultLocalPosition = weaponTransform.localPosition;
    }

    void Update()
    {
        // Raycast from camera forward
        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 cameraForward = Camera.main.transform.forward;

        float pushbackAmount = 0f;

        //if (Physics.Raycast(cameraPos, cameraForward, out RaycastHit hit, checkDistance, wallMask))
        if (Physics.SphereCast(cameraPos, 0.2f, cameraForward, out RaycastHit hit, checkDistance, wallMask))
        {
            float distance = hit.distance;
            pushbackAmount = Mathf.Clamp(checkDistance - distance, 0f, maxPushback);
        }

        // Target position with pushback
        Vector3 targetLocalPos = defaultLocalPosition - new Vector3(0f, 0f, pushbackAmount);

        // Smoothly move the weapon
        weaponTransform.localPosition = Vector3.Lerp(
            weaponTransform.localPosition,
            targetLocalPos,
            Time.deltaTime * pushbackSpeed
        );
    }
}
