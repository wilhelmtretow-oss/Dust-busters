using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DirtBall : MonoBehaviour
{
    public float bulletSpeed = 10f;      // hastighet på projektilen
    public int damage = 20;              // skada
    public float lifeTime = 3f;          // hur länge den lever
    public float knockBack = 0f;         // knockback-effekt

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Räkna ut riktning mot musen
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // viktigt, annars kan riktningen bli konstig
        Vector2 direction = (mousePos - transform.position).normalized;

        // Skjut projektilen
        rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

        // Förstöra projektilen efter lifeTime
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Snurra projektilen för "damm-effekt"
        transform.Rotate(0, 0, 360 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore Player
        if (collision.gameObject.CompareTag("Player"))
            return;

        // Hämta EnemyHealth-komponent
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            // Ge skada
            enemyHealth.TakeDamage(damage);

            // Knockback
            if (knockBack > 0f)
            {
                Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                    enemyRb.AddForce(rb.linearVelocity.normalized * knockBack, ForceMode2D.Impulse);
            }
        }

        // Förstöra projektilen vid kollision
        Destroy(gameObject);
    }
}