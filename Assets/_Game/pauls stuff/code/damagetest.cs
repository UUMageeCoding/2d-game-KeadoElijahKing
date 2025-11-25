using UnityEngine;

public class damagetest : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private bool movingDown;
    private float bottomEdge;
    private float topEdge;

    private void Awake()
    {
        bottomEdge = transform.position.y - movementDistance;
        bottomEdge = transform.position.y + movementDistance;
    }
    private void Update()
    {
        if (movingDown)
        {
            if (transform.position.y > bottomEdge)
            {
                transform.position = new Vector3(transform.position.y - speed * Time.deltaTime, transform.position.x, transform.position.z);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
        
    }
}
