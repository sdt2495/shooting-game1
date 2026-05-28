using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairUI : MonoBehaviour
{
    public Camera mainCamera;

    [Header("Crosshair Size")]
    public Vector3 normalSize = Vector3.one;
    public Vector3 targetSize = Vector3.one * 1.5f;

    [Header("Settings")]
    public float detectDistance = 100f;
    public float smoothSpeed = 10f;

    void Update()
    {
        transform.position = Mouse.current.position.ReadValue();

        CheckEnemyTarget();
    }

    void CheckEnemyTarget()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        Vector3 targetScale = normalSize;

        if (Physics.Raycast(ray, out RaycastHit hit, detectDistance))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                targetScale = targetSize;
            }
        }

        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * smoothSpeed
        );
    }
}