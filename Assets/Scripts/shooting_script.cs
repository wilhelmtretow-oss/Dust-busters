using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;           // DirtBall projektil
    public Transform bulletSpawnPoint;        // vart projektilen spawnar

    public int ammo = 100;                    // ammo count
    public bool autoFire = true;              // auto fire toggle
    public float shootingRate = 0.2f;         // cooldown mellan skott
    private float shootingTimer = 0f;

    void Update()
    {
        shootingTimer -= Time.deltaTime;

        // One shoot
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        // Auto fire
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

        // Hämta musposition i världens koordinater
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - bulletSpawnPoint.position).normalized;

        // Beräkna vinkel för rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Skapa projektil och rotera mot musen
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.Euler(0, 0, angle));

        ammo--;
        shootingTimer = shootingRate;
    }

    // Lägg till ammo (reload)
    public void AddAmmo(int count)
    {
        ammo += count;
    }
}