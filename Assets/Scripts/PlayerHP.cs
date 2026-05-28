using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHP : MonoBehaviour
{
    [Header("HP")]
    public float maxHp = 100f;
    public float currentHp;

    [Header("UI")]
    public Slider hpSlider;

    [Header("Damage Effect")]
    public Renderer playerRenderer;

    private Color originalColor;

    void Start()
    {
        currentHp = maxHp;

        hpSlider.maxValue = maxHp;
        hpSlider.value = currentHp;

        originalColor = playerRenderer.material.color;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        hpSlider.value = currentHp;

        StartCoroutine(DamageFlash());

        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // š ƒXƒRƒA•Û‘¶
        ScoreManager.Instance.SaveScore();

        // š GameOverScene ‚Ö
        SceneManager.LoadScene("GameOverScene");
    }

    IEnumerator DamageFlash()
    {
        playerRenderer.material.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        playerRenderer.material.color = originalColor;
    }
}
