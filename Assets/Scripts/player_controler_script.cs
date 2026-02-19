using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public float speed = 4f;
    public AudioSource footSteps;
    private Vector2 moveDir; // used for WASD movement
    private Vector2 movePos; // used for mouse click movement
    [HideInInspector] public Vector2 lastDir;
    private Rigidbody2D rb;
    private Health health;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        // Get components
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastDir = Vector2.down; // Set to players starting direction
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
            animator.SetFloat("xMove", moveDir.x);
            animator.SetFloat("yMove", moveDir.y);
            spriteRenderer.flipX = moveDir.x > 0f ? true : false; // Remove if right faced animations is present
            if (footSteps != null) footSteps.UnPause();
        }
        else
        {
            footSteps.Pause();
        }
        animator.SetBool("moving", moveDir.sqrMagnitude > 0.01f ? true : false);
    }

    void FixedUpdate()
    {
        if (health.isDead)
            return;

        // Move with WASD
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);

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
