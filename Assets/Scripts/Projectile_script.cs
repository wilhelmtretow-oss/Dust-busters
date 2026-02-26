using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DirtBall : MonoBehaviour
{
    public float bulletSpeed = 10f;      // hastighet p� projektilen
    public int damage = 20;              // skada
    public float lifeTime = 3f;          // hur l�nge den lever
    public float knockBack = 0f;         // knockback-effekt

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // R�kna ut riktning mot musen
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;

        // Skjut projektilen
        rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

        // F�rst�ra projektilen efter lifeTime
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Snurra projektilen f�r "damm-effekt"
        transform.Rotate(0, 0, 360 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore Player
        if (collision.gameObject.CompareTag("Player"))
            return;

        // H�mta Health-komponent
        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null)
        {
            // Ge skada
            health.TakeDamage(damage);

            // Knockback
            if (knockBack > 0f)
            {
                Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                    enemyRb.AddForce(rb.linearVelocity.normalized * knockBack, ForceMode2D.Impulse);
            }
        }

        // F�rst�ra projektilen vid kollision
        Destroy(gameObject);
    }
}