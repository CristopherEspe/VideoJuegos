using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 20f;
    public float shootRate = 0.5f;
    private float shootCooldown = 0f;

    public GameObject muzzleFlashPrefab;
    public Camera playerCamera;
    private AmmoManager ammoManager;

    void Start()
    {
        ammoManager = FindObjectOfType<AmmoManager>();
    }

    void Update()
    {
        if (ammoManager == null)
        {
            ammoManager = FindObjectOfType<AmmoManager>();
        }

        shootCooldown -= Time.deltaTime;
        if (Input.GetButton("Fire1") && shootCooldown <= 0f)
        {
            if (ammoManager != null && ammoManager.currentAmmo > 0)
            {
                Shoot();
                ammoManager.UseAmmo(1);
                shootCooldown = shootRate;
            }
            else
            {
                ammoManager.Reload(1.5f);
            }
        }
    }

    void Shoot()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = playerCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;
        direction.Normalize();

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = direction * bulletSpeed;

        if (muzzleFlashPrefab != null)
        {
            GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Destroy(muzzleFlash, 0.5f);
        }

        AudioManager.instance.Play("Shoot");
        Destroy(bullet, 2.0f);
    }
}
