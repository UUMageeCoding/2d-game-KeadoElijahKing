using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    public float lifeTime;
    private float originalLifeTime; // Store original lifetime for reset
    private Animator anim;

    private CapsuleCollider2D boxCollider;
    public PlatformerController pc; 
    private bool fireBallDirection; 
    SpriteRenderer sr;
    
    [Header("Proximity Detection")]
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private LayerMask enemyLayer;
    private bool hasDetectedEnemy = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();

        hit = false;
        direction = 1;
        
        // Store original lifetime if not already set
        if (originalLifeTime == 0)
            originalLifeTime = lifeTime;
    }
    
    private void OnEnable()
    {
        // Reset fireball state when reactivated
        ResetFireball();
    }
    
    private void Start()
    {
        // Initialize direction from player controller if available
        if (pc != null)
        {
            fireBallDirection = pc.isMovingRight;
            SetDirection(fireBallDirection);
        }
    }
    private void Update()
    {
        if (hit) return;
        
        // Check for nearby enemies for proximity-based activation
        if (!hasDetectedEnemy)
        {
            CheckForNearbyEnemies();
        }
        
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) 
        {
            Deactivate();
        }
    }
    
    private void CheckForNearbyEnemies()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
        if (nearbyEnemies.Length > 0)
        {
            hasDetectedEnemy = true;
            // Fireball is already active, just mark that we detected an enemy
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit) return; // Prevent multiple hits
        
        if(collision.CompareTag("Enemy"))
        {
            hit = true;
            boxCollider.enabled = false;
            anim.SetTrigger("explode");
            
            // Damage enemy and trigger glow effect
            Health enemyHealth = collision.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1);
                // Trigger red glow effect on enemy
                StartCoroutine(FlashEnemyRed(collision.gameObject));
            }
            
            // Deactivate after a short delay to allow explosion animation
            Invoke("Deactivate", 0.1f);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hit) return; // Prevent multiple hits
        
        if (collision.gameObject.CompareTag("Wall"))
        {
            hit = true;
            boxCollider.enabled = false;
            anim.SetTrigger("explode");
            Invoke("Deactivate", 0.1f);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hit = true;
            boxCollider.enabled = false;
            anim.SetTrigger("explode");
            
            // Damage enemy and trigger glow effect
            Health enemyHealth = collision.gameObject.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1);
                // Trigger red glow effect on enemy
                StartCoroutine(FlashEnemyRed(collision.gameObject));
            }
            
            Invoke("Deactivate", 0.1f);
        }
    }

    public void SetFireballActive()
    {
        gameObject.SetActive(true);
        ResetFireball();
    }
    
    private void ResetFireball()
    {
        hit = false;
        hasDetectedEnemy = false;
        boxCollider.enabled = true;
        
        // Reset lifetime to original value
        if (originalLifeTime > 0)
            lifeTime = originalLifeTime;
        
        // Reset animator if needed
        if (anim != null)
        {
            anim.ResetTrigger("explode");
            anim.Play("idle"); // Or your default fireball animation state
        }
    }
    
    public void SetDirection(bool _direction)
    {
        hit = false;
        boxCollider.enabled = true;

        if (_direction)
        {
            sr.flipX = false;
            direction = 1;
        } 
        else
        {
            sr.flipX = true;   
            direction = -1; 
        }
    }
    
    private IEnumerator FlashEnemyRed(GameObject enemy)
    {
        SpriteRenderer enemySprite = enemy.GetComponent<SpriteRenderer>();
        if (enemySprite != null)
        {
            Color originalColor = enemySprite.color;
            // Flash red
            enemySprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            // Return to original color (or white if no original)
            enemySprite.color = originalColor;
        }
    }
    
    private void Deactivate()
    {
        gameObject.SetActive(false); 
    }
    
    private void OnDrawGizmosSelected()
    {
        // Visualize detection radius in editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}