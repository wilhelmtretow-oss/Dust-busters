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

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
      

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

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        mousePos.z = 0f;

        Vector2 direction = (mousePos - bulletSpawnPoint.position).normalized;
        Debug.Log("direction: " + direction);
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.Euler(0, 0, angle));
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Projectile_script pScript = bullet.GetComponent<Projectile_script>();
        if(pScript != null)
        {
            PlayerController pc = GetComponentInParent<PlayerController>();
            if (pc != null)
            {
                pScript.damage = pc.currentDamage;
            }
        }

        bullet.GetComponent<Rigidbody2D>().AddForce(direction * 10f, ForceMode2D.Impulse);
        ammo--;
        shootingTimer = shootingRate;
    }

    public void AddAmmo(int count)
    {
        ammo += count;
    }
}