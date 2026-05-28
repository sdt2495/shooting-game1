using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0, 2f, 0);

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (target == null) return;

        Vector3 screenPos =
            mainCamera.WorldToScreenPoint(
                target.position + offset
            );

        transform.position = screenPos;
    }
}