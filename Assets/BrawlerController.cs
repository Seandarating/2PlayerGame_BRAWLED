using UnityEngine;

public class BrawlerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 7f;
    public float jumpForce = 12f;

    [Header("Input Mapping")]
    public string horizontalAxis = "Horizontal";
    public KeyCode jumpKey = KeyCode.W;
    public KeyCode attackKey = KeyCode.Space; // New Attack Key

    [Header("Combat Settings")]
    public Transform attackPoint; // Where the punch happens
    public float attackRange = 0.5f;
    public LayerMask enemyLayers; // To define who we can hit
    public int attackDamage = 20;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movement
        float moveInput = Input.GetAxisRaw(horizontalAxis);
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Jumping
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Attacking
        if (Input.GetKeyDown(attackKey))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Detect enemies in range of the attack point
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            // Make sure we don't hit ourselves
            if (enemy.gameObject != gameObject)
            {
                enemy.GetComponent<BrawlerHealth>().TakeDamage(attackDamage);
            }
        }
    }

    // Ground Detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = false;
    }

    // This draws a red circle in the Unity Editor so you can see your attack range
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}