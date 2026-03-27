using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent nav;
    private GameObject player;
    private Health playerHealth;
    public LayerMask obstacleLayerMasks;
    public float viewDistance = 5f;
    public float attackRange = 2f;
    public int damage = 10;
    public float attackCooldown = 1f;
    public bool rotation = false;
    public float fixedRotation = 0f;
    private float lastAttackTime;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        if (!nav) Debug.LogWarning("No NavMeshAgent found on this GameObject.");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb) rb.bodyType = RigidbodyType2D.Kinematic; // fixed - replaces isKinematic
    }

    void Start()
    {
        nav.updateRotation = false;
        nav.updateUpAxis = false;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
            if (!playerHealth)
                Debug.LogWarning("Player does not have a Health component!");
        }
    }

    void Update()
    {
        if (!player || playerHealth == null || playerHealth.isDead)
            return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position, obstacleLayerMasks);
        if (hit)
        {
            nav.isStopped = true;
        }
        else if (distance <= attackRange)
        {
            nav.isStopped = true;
            TryAttack();
        }
        else if (distance <= viewDistance)
        {
            nav.isStopped = false;
            nav.destination = player.transform.position;
            if (rotation)
            {
                Vector2 direction = player.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle + fixedRotation);
            }
        }
        else
        {
            nav.isStopped = true;
        }
    }

    private void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            playerHealth.TakeDamage(damage);
            lastAttackTime = Time.time;
            Debug.Log("Enemy dealt damage!");
        }
    }
}