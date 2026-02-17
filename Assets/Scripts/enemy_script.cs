using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent nav;
    private Animator animator;
    private GameObject player;
    private bool isAttacking = false;

    public LayerMask obstacleLayerMasks;
    public float viewDistance = 5f;
    public float attackRange = 2f;
    public int damage = 10;


    private void Awake()
    {
        // Get component references
        nav = GetComponentInParent<NavMeshAgent>();
        if (!nav) Debug.Log("No NavMeshAgent component found in Enemy parent object");
        animator = GetComponent<Animator>();
        if (!animator) Debug.Log("No Animator component was found on Enemy");
    }


    void Start()
    {
        // Uncomment if you don't need to manualy overwrite rotation
        nav.updateRotation = false;
        nav.updateUpAxis = false;

        // Get player
        player = GameObject.Find("Player");
        if (!player) Debug.Log("No gameObject tagged 'Player' was found for Enemy.");
    }


    void Update()
    {
        // Send data to Animator
        if (animator) animator.SetFloat("move", nav.velocity.magnitude);

        // Stop updating if Player is missing or dead
        if (!player || player.GetComponent<Health>().isDead) return;

        // Create a Linecast between this enemy and player
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position, obstacleLayerMasks);

        // Linecast to target was succesful (did not hit anything on obstacleLayerMasks)
        if (!hit)
        {
            // Play Attack animation if within attack range
            if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
            {
                // Is not already attacking
                if (!isAttacking)
                {
                    // Set attack animation
                    if (animator) animator.SetTrigger("attack");
                    isAttacking = true;
                }
                // Draw a debug line from enemy to player 
                Debug.DrawLine(transform.position, player.transform.position, Color.red);
            }
            // Move to target position if within view distance
            else if (Vector2.Distance(transform.position, player.transform.position) < viewDistance)
            {
                nav.destination = player.transform.position;
                Debug.DrawLine(transform.position, player.transform.position);
            }
        }
        // Obstacle in the way
        else
        {
            // Draw a debug line from enemy to obstacle
            Debug.DrawLine(transform.position, hit.point);
        }
    }


    // todo: Call this method on a suitable frame in attack animation
    public void Attack()
    {
        if (!player) return;

        // Still within attack range and player is still alive?
        if (Vector2.Distance(transform.position, player.transform.position) < attackRange
            && !player.GetComponent<Health>().isDead)
        {
            // Player takes damage
            player.GetComponent<Health>().TakeDamage(damage);
        }

        // is not longer attacking
        isAttacking = false;
    }
}
