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
    public AudioClip footSteps;
    private Vector2 moveDir;
    private Vector2 movePos;
    [HideInInspector] public Vector2 lastDir;
    private Rigidbody2D rb;
    private Health health;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastDir = Vector2.down;

        if (health == null)
            Debug.LogError("Health component not found on " + gameObject.name);
        if (rb == null)
            Debug.LogError("Rigidbody2D not found on " + gameObject.name);
    }

    private void Start()
    {
        if (ModuleInventoryManager.Instance != null)
        {
            float speedBonus = ModuleInventoryManager.Instance.GetTotalBonus("Speed");
            float atkBonus = ModuleInventoryManager.Instance.GetTotalBonus("Damage");
            finalSpeed = speed + speedBonus;
            currentDamage = 20 + Mathf.RoundToInt(atkBonus);
            currentDefence = ModuleInventoryManager.Instance.GetTotalBonus("Defence");
        }
        else
        {
            finalSpeed = speed;
            currentDamage = 20;
            currentDefence = 0;
            Debug.LogWarning("ModuleInventoryManager not found! Using default values.");
        }

        Debug.Log($"Speed: {finalSpeed} | Damage: {currentDamage} | Defence: {currentDefence}");
    }

    void Update()
    {
        if (health == null || health.isDead)
            return;

        moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (moveDir.magnitude > 1f)
            moveDir.Normalize();

        if (moveDir.sqrMagnitude > 0.01f)
            lastDir = moveDir;
    }

    void FixedUpdate()
    {
        if (health == null || health.isDead)
            return;

        rb.MovePosition(rb.position + moveDir * finalSpeed * Time.fixedDeltaTime);

        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        if (moveDir.magnitude > 0f)
            rb.rotation = angle;
    }
}