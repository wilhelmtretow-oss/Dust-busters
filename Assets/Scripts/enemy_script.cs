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

    private float lastAttackTime;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        if (!nav) Debug.LogWarning("No NavMeshAgent found on this GameObject.");

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb) rb.isKinematic = true; // säkerställ 2D-fysik inte stör agent
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

        // Line of sight check
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position, obstacleLayerMasks);

        if (hit) // hinder blockerar sikt
        {
            nav.isStopped = true;
        }
        else if (distance <= attackRange) // attackera
        {
            nav.isStopped = true;
            TryAttack();
        }
        else if (distance <= viewDistance) // jaga
        {
            nav.isStopped = false;
            nav.destination = player.transform.position;
        }
        else
        {
            nav.isStopped = true; // utanför viewDistance
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