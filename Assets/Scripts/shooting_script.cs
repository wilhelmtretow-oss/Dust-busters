using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Gun : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public AudioClip shootSound;
    public AudioClip clickSound;
    public AudioClip reloadSound;
    private AudioSource audioSource;

    public int ammo = 100;
    public bool autoFire = true;
    public float shootingRate = 0.2f;
    private float shootingTimer = 0f;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        // One shoot
        if (Input.GetButtonDown("Fire1"))
        {
            if (ammo > 0)
            {
                // Spawn and sets bullet rotation to this rotation
                //GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);

                // Spawn and sets the bullet rotation based on the players last movement direction
                float angle = Mathf.Atan2(playerController.lastDir.y, playerController.lastDir.x) * Mathf.Rad2Deg;
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position + (Vector3)playerController.lastDir, Quaternion.Euler(0, 0, angle));

                ammo--;
                audioSource.PlayOneShot(shootSound);
            }
            else
            {
                audioSource.PlayOneShot(clickSound);
            }
            shootingTimer = shootingRate;
        }
        // Auto fire
        else if (Input.GetButton("Fire1") && autoFire)
        {
            if (ammo > 0 && shootingTimer <= 0f)
            {
                // Spawn and sets bullet rotation to this rotation
                //GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);

                // Spawn and sets the bullet rotation based on the players last movement direction
                float angle = Mathf.Atan2(playerController.lastDir.y, playerController.lastDir.x) * Mathf.Rad2Deg;
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.Euler(0, 0, angle));

                ammo--;
                audioSource.PlayOneShot(shootSound);
                shootingTimer = shootingRate;
            }
            else if (shootingTimer <= 0f)
            {
                audioSource.PlayOneShot(clickSound);
                shootingTimer = shootingRate;
            }
        }
        shootingTimer -= Time.deltaTime;
    }

    public void AddAmmo(int count)
    {
        ammo += count;
        audioSource.PlayOneShot(reloadSound);
    }
}
