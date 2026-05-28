using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public float moveSpeed = 50f;

    public float lifeTime = 1f;

    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position +=
            Vector3.up * moveSpeed * Time.deltaTime;
    }

    public void SetDamage(float damage)
    {
        text.text = damage.ToString();
    }
}