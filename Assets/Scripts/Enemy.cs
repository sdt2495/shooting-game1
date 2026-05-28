using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("HP")]
    public float maxHp = 100f;
    private float currentHp;

    [Header("Move")]
    public float moveSpeed = 2f;
    public float stopDistance = 3f;

    private Transform player;
    private PlayerHP playerHP;

    [Header("Attack")]
    public float attackDamage = 10f;
    public float attackInterval = 1f;
    private float attackTimer;

    [Header("HP Bar")]
    public Slider hpSlider;
    public GameObject hpBarObject;
    public float visibleTime = 3f;
    private float hpBarTimer;

    [Header("Damage Text")]
    public GameObject damageTextPrefab;
    public Transform damageTextPoint;
    public Transform canvasTransform;

    private bool isDead = false;

    void Start()
    {
        currentHp = maxHp;

        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHp;
            hpSlider.value = currentHp;
        }

        if (hpBarObject != null)
            hpBarObject.SetActive(false);

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerHP = playerObj.GetComponent<PlayerHP>();
        }
    }

    void Update()
    {
        if (isDead) return;

        MoveToPlayer();
        HandleHPBarTimer();
    }

    void MoveToPlayer()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
        }
        else
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            if (playerHP != null)
            {
                playerHP.TakeDamage(attackDamage);
            }

            attackTimer = attackInterval;
        }
    }

    void HandleHPBarTimer()
    {
        if (hpBarObject == null) return;

        if (hpBarObject.activeSelf)
        {
            hpBarTimer -= Time.deltaTime;

            if (hpBarTimer <= 0f)
            {
                hpBarObject.SetActive(false);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHp -= damage;

        if (hpSlider != null)
            hpSlider.value = currentHp;

        ShowHPBar();
        ShowDamageText(damage);

        if (currentHp <= 0f)
        {
            Die();
        }
    }

    void ShowHPBar()
    {
        if (hpBarObject == null) return;

        hpBarObject.SetActive(true);
        hpBarTimer = visibleTime;
    }

    void ShowDamageText(float damage)
    {
        if (damageTextPrefab == null || damageTextPoint == null) return;

        GameObject textObj = Instantiate(
            damageTextPrefab,
            damageTextPoint.position,
            Quaternion.identity,
            canvasTransform
        );

        DamageText damageText = textObj.GetComponent<DamageText>();

        if (damageText != null)
        {
            damageText.SetDamage(damage);
        }
    }

    void Die()
    {
        isDead = true;
        ScoreManager.Instance.AddScore(10);
        Debug.Log("Enemy Dead");

        Destroy(gameObject);
    }
}