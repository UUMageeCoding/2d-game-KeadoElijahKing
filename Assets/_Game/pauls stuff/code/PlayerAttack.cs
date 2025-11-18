using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
     [SerializeField] public Transform firePoint;
    [SerializeField] public Transform firePointRight;
     [SerializeField] public Transform firePointLeft;
    [SerializeField] public GameObject[] fireballs;
    private Animator anim;
    private PlatformerController playerMovement;
    private float cooldownTimer = Mathf.Infinity;

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

        firePointLeft.transform.position = new Vector2(transform.position.x, firePointLeft.transform.position.y);
        firePointRight.transform.position = new Vector2(transform.position.x, firePointRight.transform.position.y);
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        if (playerSprite.flipX)
        {
            firePoint.position = firePointLeft.position;
        }
        else
        {
            firePoint.position = firePointRight.position; 
        }

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    public int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            return i;
        }
        return 0;
    }
}

