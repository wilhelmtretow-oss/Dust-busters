using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public float speed = 4f;
    public int currentDamage;
    public float currentDefence;
    private float finalSpeed;
    public AudioSource footSteps;
    private Vector2 moveDir; // used for WASD movement
    private Vector2 movePos; // used for mouse click movement
    [HideInInspector] public Vector2 lastDir;
    private Rigidbody2D rb;
    private Health health;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        // Get components
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastDir = Vector2.down; // Set to players starting direction
    }

    private void Start()
    {
        float speedBonus = ModuleInventoryManager.Instance.GetTotalBonus("Speed");
        float atkBonus = ModuleInventoryManager.Instance.GetTotalBonus("Damage");

        finalSpeed = speed + speedBonus;
        currentDamage = 20 + Mathf.RoundToInt(atkBonus);
        currentDefence = ModuleInventoryManager.Instance.GetTotalBonus("Defence");

        Debug.Log($"Speed: {finalSpeed} | Damage: {currentDamage} | Defence: {currentDefence}");
    }

    void Update()
    {
        if (health.isDead)
            return;

        // Get movement by WASD
        moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // Normalize Vector
        if (moveDir.magnitude > 1f)
            moveDir.Normalize();

        /*
        // Read movement by right mouse click
        if (Input.GetMouseButtonDown(1))
            movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        */

        // Set animation params
        if (moveDir.sqrMagnitude > 0.01f)
        {
            lastDir = moveDir;
        }
        else
        { 
        }
    }

    void FixedUpdate()
    {
        if (health.isDead)
            return;

        // Move with WASD
        rb.MovePosition(rb.position + moveDir * finalSpeed * Time.fixedDeltaTime);

        // Move with mouse position
        //rb.MovePosition(Vector2.MoveTowards(rb.position, movePos, speed * Time.fixedDeltaTime));

        
        // Set rotation based on moving vector
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        if(moveDir.magnitude > 0f)
            rb.rotation = angle;
        

        /*
        // Set rotation based on mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mousePos - transform.position;
        rb.rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        */
    }
}
