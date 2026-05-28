using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour
{
    public Camera mainCamera;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 30f;

    [Header("Shoot")]
    public float damage = 10f;
    public float shootDistance = 100f;

    [Header("Fire Rate")]
    public float fireRate = 0.1f;
    private float fireTimer;

    [Header("Effect")]
    public GameObject muzzleFlashPrefab;

    [Header("Ammo")]
    public int maxAmmo = 30;
    private int currentAmmo;
    public Text ammoText;

    [Header("Reload")]
    public float reloadTime = 1.5f;   
    private bool isReloading = false; 
    public Animator animator;        

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip shootSE;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;

        // リロード中は何もできない
        if (isReloading) return;

        // Rキーでリロード開始
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            StartCoroutine(Reload());
            return;
        }

        // 左クリックで射撃
        if (Mouse.current.leftButton.isPressed && fireTimer <= 0)
        {
            Shoot();
            fireTimer = fireRate;
        }
    }

    void Shoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("No Ammo");
            return;
        }

        audioSource.PlayOneShot(shootSE);

        currentAmmo--;
        UpdateAmmoUI();

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, shootDistance))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                SpawnMuzzleFlash();
                SpawnBullet(hit.point);

                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("Reloading...");

        if (animator != null)
            animator.SetTrigger("Reload");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        UpdateAmmoUI();

        Debug.Log("Reload Complete");

        isReloading = false;
    }

    void SpawnBullet(Vector3 targetPos)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        Vector3 dir = (targetPos - firePoint.position).normalized;
        rb.linearVelocity = dir * bulletSpeed;

        Destroy(bullet, 3f);
    }

    void SpawnMuzzleFlash()
    {
        GameObject effect = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
        Destroy(effect, 1f);
    }

    void UpdateAmmoUI()
    {
        ammoText.text = currentAmmo + " / " + maxAmmo;
    }
}
