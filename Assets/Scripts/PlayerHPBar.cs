using UnityEngine;

public class PlayerHPBar : MonoBehaviour
{
    private Transform player;

    public Vector3 offset = new Vector3(0, 2f, 0);

    private Camera mainCamera;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        mainCamera = Camera.main;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 worldPos = player.position + offset;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

        transform.position = screenPos;
    }
}