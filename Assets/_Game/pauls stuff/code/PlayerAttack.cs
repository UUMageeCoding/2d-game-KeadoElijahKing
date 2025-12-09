using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float attackDelay = 0.5f; 
     [SerializeField] public Transform firePoint;
   // [SerializeField] public Transform firePointRight;
  //   [SerializeField] public Transform firePointLeft;
    [SerializeField] public GameObject[] fireballs;
    private Animator anim;
    private PlatformerController playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private int lastFireballIndex = -1; // Track last used fireball for round-robin

    PlatformerController platformerController;
    public SpriteRenderer playerSprite;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlatformerController>();

        playerSprite = GetComponent <SpriteRenderer>();
        
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCoolDown && playerMovement.canAttack())
            Attack();
            //Debug.Log("isFlipping  " + playerSprite.flipX); 

        cooldownTimer += Time.deltaTime;

       // firePointLeft.transform.position = new Vector2(transform.position.x, firePointLeft.transform.position.y);
        //firePoint.transform.position = new Vector2(transform.position.x, firePoint.transform.position.y);
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

       /* if (playerSprite.flipX)
        {
            firePoint.position = firePoint.position;
        }
        else
        {
            firePoint.position = firePoint.position; 
        }
*/
        int fireballIndex = FindFireball();
        if (fireballIndex >= 0)
        {
            GameObject fireball = fireballs[fireballIndex];
            fireball.transform.position = firePoint.position;
            fireball.GetComponent<projectile>().SetFireballActive();
            fireball.GetComponent<projectile>().SetDirection(playerMovement.isMovingRight);
        }
    }
    public int FindFireball()
    {
        // First, try to find an inactive fireball
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                lastFireballIndex = i;
                return i;
            }
        }
        
        // If all fireballs are active, use round-robin to cycle through them
        // This ensures we can always fire even if all are active
        lastFireballIndex = (lastFireballIndex + 1) % fireballs.Length;
        return lastFireballIndex;
    }
}

