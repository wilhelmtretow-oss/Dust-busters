using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public int damage;
    public float lifeTime = 3f;
    public float knockBack = 0f;

    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // return if bullet is colliding with player
        if (collision.gameObject.CompareTag("Player"))
            return;

        // Try get Health component from colliding object
        Health health = collision.gameObject.GetComponent<Health>();

        // Hits object with Health component
        if (health)
        {
            health.TakeDamage(damage);
            if (knockBack > 0f)
            {
                // Try get colliding objects rigid body and add knock back.
                Rigidbody2D rb = collision.gameObject.GetComponentInParent<Rigidbody2D>();
                if (rb != null)
                    rb.AddForce(GetComponent<Rigidbody2D>().linearVelocity.normalized * knockBack);
            }
        }
        // Hits other object
        else
        {
            // Do something
        }

        Destroy(gameObject);
    }
}
