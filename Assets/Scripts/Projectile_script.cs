using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile_script : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public int damage = 20;
    public float lifeTime = 3f;
    public float knockBack = 0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // fixad - var bortkommenterad
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Rotate(0, 0, 360 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            return;

        Debug.Log(collision.gameObject.name);

        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            if (knockBack > 0f)
            {
                Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                    enemyRb.AddForce(rb.linearVelocity.normalized * knockBack, ForceMode2D.Impulse);
            }
        }
        Destroy(gameObject);
    }
}