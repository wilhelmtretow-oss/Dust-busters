using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    public int ammo = 100;
    public bool autoFire = true;
    public float shootingRate = 0.2f;
    private float shootingTimer = 0f;

    void Update()
    {
        shootingTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        else if (Input.GetButton("Fire1") && autoFire)
        {
            if (shootingTimer <= 0f)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (ammo <= 0)
            return;

        // 🔒 Säkerhetskontroller
        if (bulletPrefab == null || bulletSpawnPoint == null)
        {
            Debug.LogError("Bullet prefab or spawn point not assigned!");
            return;
        }

        if (Camera.main == null)
        {
            Debug.LogError("No camera tagged as MainCamera!");
            return;
        }

        // Hämta musposition i världens koordinater
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // viktigt i 2D

        Vector2 direction = (mousePos - bulletSpawnPoint.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.Euler(0, 0, angle));

        ammo--;
        shootingTimer = shootingRate;
    }

    public void AddAmmo(int count)
    {
        ammo += count;
    }
}