using UnityEngine;

public class meleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    
    [SerializeField] private float range;
    [SerializeField] private float colldierDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D BoxCollider; 
    [SerializeField] private LayerMask Playerlayer; 
    private float cooldownTimer = Mathf.Infinity;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        //Attack only when player in sight
        if (PlayerInSight())
        {
             if (cooldownTimer >= attackCoolDown)
        {
            //Attack
        }
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
}
