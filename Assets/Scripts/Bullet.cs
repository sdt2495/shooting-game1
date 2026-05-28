using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffectPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            SpawnHitEffect();

            Destroy(gameObject);
        }
    }

    void SpawnHitEffect()
    {
        GameObject effect = Instantiate(
            hitEffectPrefab,
            transform.position,
            Quaternion.identity
        );

        Destroy(effect, 1f);
    }
}