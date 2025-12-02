using UnityEngine;

/*public class RangeAttack : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCoolDown;
    [SerializeField] private int damage;
    [SerializeField] private float range;

      [Header("Ranged Attack")]
      [SerializeField] private Transform firepoint;
      [SerializeField] private GameObject[] fireballs;

      [Header("Collider Parameters")]
    [SerializeField] private float colldierDistance;
    
    [SerializeField] private BoxCollider2D BoxCollider; 
      [Header("Player Layer")]
    [SerializeField] private LayerMask Playerlayer; 
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;

    private enemyPatrol enemypatrol;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemypatrol = GetComponentInParent<enemyPatrol>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        //Attack only when player in sight
        if (PlayerInSight())
        {
             if (cooldownTimer >= attackCoolDown)
        {
                cooldownTimer = 0;
                anim.SetTrigger("rangedAttack");
        }
             if (enemypatrol != null) 
                enemypatrol.enabled = !PlayerInSight();
     } 
    }

    private void RangedAttack()
    {
        cooldownTimer = 0;
        fireballs[FindFireball()].transform.position = firepoint.position;
        fireballs[FindFireball()].GetComponent<EnemyProjectle>().ActivateProjectile(); 
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i; 
        }
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(BoxCollider.bounds.center + transform.right * range * transform.localScale.x * colldierDistance, new Vector3(BoxCollider.bounds.size.x * range, BoxCollider.bounds.size.y, BoxCollider.bounds.size.z), 0, Vector2.left, 0, Playerlayer);

        return hit.collider !=null; 


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(BoxCollider.bounds.center + transform.right * range * transform.localScale.x * colldierDistance, new Vector3(BoxCollider.bounds.size.x * range, BoxCollider.bounds.size.y, BoxCollider.bounds.size.z));
    }

}*/
