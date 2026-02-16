using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class Health : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    [HideInInspector] public bool isDead = false;
    public GameObject deathEffect;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public UnityEvent onTakeDamage;
    public UnityEvent onDeath;
    private AudioSource audioSource;
    private Animator animator;
    private NavMeshAgent nav;


    void Awake()
    {
        // Get component references
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        nav = GetComponentInParent<NavMeshAgent>();

        // Events
        if (onTakeDamage == null)
            onTakeDamage = new UnityEvent();
        if (onDeath == null)
            onDeath = new UnityEvent();
    }

    public void TakeDamage(int amount)
    {
        // Don't do anything if dead
        if (isDead)
            return;

        // Reduce health by amount of damage
        health -= amount;

        // Is dead?
        if (health <= 0)
        {
            isDead = true;

            // Instatiate death particles
            if (deathEffect) Instantiate(deathEffect, transform.position, transform.rotation);

            // Play death animation
            if (animator) animator.SetTrigger("dead");

            // Play death sound
            if (deathSound) audioSource.PlayOneShot(deathSound);

            // Stop updating navmesh for objects with pathfinding
            if (nav) nav.isStopped = true;

            onDeath.Invoke();
        }
        // Got hit
        else
        {
            // Play hurt sound
            if (hurtSound) audioSource.PlayOneShot(hurtSound);

            // Play hit animation
            if (animator) animator.SetTrigger("hit");

            onTakeDamage.Invoke();
        }
    }

    // Call this on pick up event
    public void AddHealth(int hp)
    {
        health = (int)Mathf.Clamp(health += hp, 0, maxHealth);
    }

    // Call this on last frame of death animation
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
